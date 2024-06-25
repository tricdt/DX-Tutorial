using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace DevExpress.Diagram.Demos {
    public class TournamentViewModel {
        public TournamentViewModel() {
            List<Game> originalGames = new List<Game>();
            for(int i = 0; i < TournamentsData.Teams.Count - 1; i += 2) {
                var first = TournamentsData.Teams[i];
                var second = TournamentsData.Teams[i + 1];
                originalGames.Add(gameFactory.Invoke(CreateId(first, second), first, second));
            }
            GenerateTable(originalGames);
        }
        static TournamentViewModel() {
            gameFactory = Mvvm.POCO.ViewModelSource.Factory<int, TeamData, TeamData, Game>((id, first, second) => new Game(id, first, second));
        }
        static Func<int, TeamData, TeamData, Game> gameFactory;
        readonly List<Game> games = new List<Game>();
        readonly List<GamePair> gamePairs = new List<GamePair>();
        readonly List<TournamentRelationInfo> relationships = new List<TournamentRelationInfo>();

        void GenerateTable(List<Game> games) {
            if(!games.Any())
                return;
            this.games.AddRange(games);
            if(games.Count == 1)
                return;

            List<GamePair> newGamePairs= new List<GamePair>();
            var gamePairFactory = Mvvm.POCO.ViewModelSource.Factory<Game, Game, Game, GamePair>((f, s, child) => new GamePair(f, s, child));

            for(int i = 0; i < games.Count - 1; i += 2) {
                var first = games[i];
                var second = games[i + 1];
                var gamePair = gamePairFactory.Invoke(first, second, gameFactory.Invoke(CreateId(first, second), null, null));
                newGamePairs.Add(gamePair);
                relationships.Add(new TournamentRelationInfo(gamePair.Result, first));
                relationships.Add(new TournamentRelationInfo(gamePair.Result, second));
            }
            var newGames = newGamePairs.Select(game => game.Result).ToList();
            gamePairs.AddRange(newGamePairs);
            GenerateTable(newGames);
        }
        static int CreateId(IID first, IID second) {
            return first.Id * 100 + second.Id * 10;
        }
        public IEnumerable<Game> Games { get { return this.games; } }
        public IEnumerable<TournamentRelationInfo> Relationships { get { return this.relationships; } }
    }

    public abstract class DataPair<T> : IID where T : class {
        protected DataPair(int id, T first, T second, T result) {
            this.Id = id;
            this.Result = result;
            this.First = first;
            this.Second = second;
        }
        public int Id { get; set; }
        public virtual T First { get; set; }
        public virtual T Second { get; set; }
        public virtual T Result { get; protected set; }

        protected void OnFirstChanged(T oldValue) {
            if(oldValue != null)
                UnsubscribeEvents(oldValue);
            if(this.First != null)
                SubscribeEvents(this.First);
            UpdatedResult(this.First);
        }
        protected void OnSecondChanged(T oldValue) {
            if(oldValue != null)
                UnsubscribeEvents(oldValue);
            if(this.Second != null)
                SubscribeEvents(this.Second);
            UpdatedResult(this.Second);
        }
        void SubscribeEvents(T data) {
            var notifyObject = data as INotifyPropertyChanged;
            if(notifyObject == null)
                return;
            notifyObject.PropertyChanged += NotifyObject_PropertyChanged; ;
        }
        void UnsubscribeEvents(T data) {
            var notifyObject = data as INotifyPropertyChanged;
            if(notifyObject == null)
                return;
            notifyObject.PropertyChanged -= NotifyObject_PropertyChanged; ;
        }
        void NotifyObject_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            UpdatedResult(sender as T);
        }
        protected virtual void UpdatedResult(T sender) { }
    }
    public class GamePair : DataPair<Game> {
        public GamePair(Game first, Game second, Game child) : base(0, first, second, child) { }
        protected override void UpdatedResult(Game pair) {
            base.UpdatedResult(pair);
            if(pair == null)
                return;
            if(pair == this.First) {
                this.Result.First = pair.Result;
                if(pair.Result == null)
                    this.Result.Team1Score = null;
            }
            else {
                this.Result.Second = pair.Result;
                if(pair.Result == null)
                    this.Result.Team2Score = null;
            }
        }
    }
    public class Game : DataPair<TeamData> {
        public Game(int id, TeamData first, TeamData second) : base(id, first, second, null) {
            this.Team1Score = GetTeamScoreString(first);
            this.Team2Score = GetTeamScoreString(second);
        }
        public virtual string Team1Score { get; set; }
        public virtual string Team2Score { get; set; }
        protected void OnTeam1ScoreChanged() {
            UpdatedResult(this.First);
        }
        protected void OnTeam2ScoreChanged() {
            UpdatedResult(this.Second);
        }

        static string GetTeamScoreString(TeamData data) {
            return data == null ? null : data.Score;
        }
        static int? GetTeamScoreNumericValue(string stringScore) {
            int tempScore = 0;
            return (stringScore == null || !int.TryParse(stringScore, out tempScore)) ? null : new Nullable<int>(tempScore);
        }
        protected override void UpdatedResult(TeamData data) {
            base.UpdatedResult(data);
            var team1Score = GetTeamScoreNumericValue(Team1Score);
            var team2Score = GetTeamScoreNumericValue(Team2Score);
            if((!team1Score.HasValue || !team2Score.HasValue) || (team1Score.Value == team2Score.Value))
                this.Result = null;
            else
                this.Result = team1Score.Value > team2Score.Value ? this.First : this.Second;
        }
    }
    
    public interface IID {
        int Id { get; set; }
    }
    public class TournamentRelationInfo {
        public TournamentRelationInfo(Game source, Game target) {
            Source = source;
            Target = target;
        }
        public Game Source { get; private set; }
        public Game Target { get; private set; }
    }
    public class TeamData : IID {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual string Score { get; set; }
        public byte[] ImageData { get; set; }
    }
    
    [XmlRoot("Teams")]
    public class Teams : List<TeamData> { }
    public static class TournamentsData {
        static TournamentsData() {
            using(var stream = DiagramDemoFileHelper.GetDataStream("TournamentBracket.xml")) {
                var serializer = new XmlSerializer(typeof(Teams));
                Teams = (Teams)serializer.Deserialize(stream);
            }
        }
        public static readonly Teams Teams;
    }
}
