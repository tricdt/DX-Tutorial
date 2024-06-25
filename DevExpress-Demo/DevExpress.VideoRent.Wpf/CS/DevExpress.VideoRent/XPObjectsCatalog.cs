using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using DevExpress.Data.Filtering;
using DevExpress.VideoRent.Helpers;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;

#if SL
using PrivateAttribute = System.ObsoleteAttribute;
using DevExpress.Xpf.Drawing;
#endif

namespace DevExpress.VideoRent {
    public class Movie : VideoRentBaseObject, IRentItem, IComparable {
        MovieCategory category;
        string title;
        MovieGenre genre;
        MovieRating rating;
        DateTime? releaseDate;
        TimeSpan? runTime;
        bool isColor;
        Image photo;
        string tagline;
        string plot;
        string awards;
        string webSite;
        string aspectRatio;
        Language language;

        public Movie(Session session) : base(session) { }
        public Movie(Session session, int selfId)
            : this(session) {
            SelfId = selfId;
        }
        public Movie(Session session, int selfId, string title)
            : this(session, selfId) {
            Title = title;
        }
        public Movie(Session session, string title)
            : this(session) {
            Title = title;
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
            IsColor = true;
            Category = MovieCategory.GetDefaultCategory(Session);
        }
        protected override void OnSavingOverride() {
            base.OnSavingOverride();
            if(!IsDeleted) {
                if(Category == null) Category = MovieCategory.GetDefaultCategory(Session);
                if(string.IsNullOrEmpty(Title)) throw new Exception("Title can't be null");
            }
        }
#if SL
        [Indexed(Unique = true)]
        [Persistent("MovieId"), Browsable(false), Private("", true)]
        public int _MovieId {
            get { return GetPropertyValue<int>("_MovieId"); }
            set { SetPropertyValue<int>("_MovieId", value); }
        }
        [PersistentAlias("_MovieId")]
        public int MovieId {
            get { return GetPropertyValue<int>("_MovieId"); }
            protected set { SetPropertyValue<int>("_MovieId", value); }
        }
#else
        [Persistent, Indexed(Unique = true)]
        public int MovieId {
            get { return GetPropertyValue<int>("MovieId"); }
            protected set { SetPropertyValue<int>("MovieId", value); }
        }
#endif
        protected override string GeneratedIdType { get { return "Movie"; } }
        public MovieItem RentItem {
            get {
                XPCollection<MovieItem> items = GetActiveItems();
                if(items.Count == 0) return null;
                return items[0];
            }
        }
        public MovieItem SellItem {
            get {
                XPCollection<MovieItem> items = GetActiveAvailableForSellItems();
                if(items.Count == 0) return null;
                return items[0];
            }
        }
        [Association("Movie-Items"), Aggregated]
        public XPCollection<MovieItem> Items {
            get { return GetCollection<MovieItem>("Items"); }
        }
        [Association("Category-Movies")]
        public MovieCategory Category {
            get { return category; }
            set { SetPropertyValue<MovieCategory>("Category", ref category, value); }
        }
        [Size(SizeAttribute.Unlimited)]
        public string Title {
            get { return title; }
            set { SetPropertyValue<string>("Title", ref title, value); }
        }
        public string MovieTitle { get { return string.Format("{0} ({1})", Title, ReleaseDate.HasValue ? ReleaseDate.Value.Year.ToString() : "?"); } }
        [Size(SizeAttribute.Unlimited)]
        public string Tagline {
            get { return tagline; }
            set { SetPropertyValue<string>("Tagline", ref tagline, value); }
        }
        [Size(SizeAttribute.Unlimited)]
        public string Plot {
            get { return plot; }
            set { SetPropertyValue<string>("Plot", ref plot, value); }
        }
        public string Awards {
            get { return awards; }
            set { SetPropertyValue<string>("Awards", ref awards, value); }
        }
        [Association("Movie-MovieArtist"), Aggregated]
        public XPCollection<MovieArtist> Artists { get { return GetCollection<MovieArtist>("Artists"); } }
        public MovieRating Rating {
            get { return rating; }
            set { SetPropertyValue<MovieRating>("Rating", ref rating, value); }
        }
        public MovieGenre Genre {
            get { return genre; }
            set { SetPropertyValue<MovieGenre>("Genre", ref genre, value); }
        }
        public DateTime? ReleaseDate {
            get { return releaseDate; }
            set { SetPropertyValue<DateTime?>("ReleaseDate", ref releaseDate, value); }
        }
        public TimeSpan? RunTime {
            get { return runTime; }
            set { SetPropertyValue<TimeSpan?>("RunTime", ref runTime, value); }
        }
        public bool IsColor {
            get { return isColor; }
            set { SetPropertyValue<bool>("IsColor", ref isColor, value); }
        }
        [ValueConverter(typeof(ImageValueConverter))]
        public Image Photo {
            get { return photo; }
            set { SetPropertyValue<Image>("Photo", ref photo, value); }
        }
        public Image PhotoExist {
            get {
                if(Photo == null) return ReferenceImages.UnknownMovie;
                return Photo;
            }
        }
        public string WebSite {
            get { return webSite; }
            set { SetPropertyValue<string>("WebSite", ref webSite, value); }
        }
        public string AspectRatio {
            get { return aspectRatio; }
            set { SetPropertyValue<string>("AspectRatio", ref aspectRatio, value); }
        }
        [Association("Movies-Language")]
        public Language Language {
            get { return language; }
            set { SetPropertyValue<Language>("Language", ref language, value); }
        }
        [Association("Movie-MovieCompany"), Aggregated]
        public XPCollection<MovieCompany> Companies {
            get { return GetCollection<MovieCompany>("Companies"); }
        }
        [Association("Movie-MoviePictures"), Aggregated]
        public XPCollection<MoviePicture> Pictures { get { return GetCollection<MoviePicture>("Pictures"); } }
        public MovieArtist Director {
            get {
                MovieArtistLine directorLine = MovieArtistLine.GetDirector(Session);
                if(directorLine == null) return null;
                MovieArtist movieDirector = Session.FindObject<MovieArtist>(CriteriaOperator.Parse("Movie = ? and Line = ?", this, directorLine));
                if(movieDirector == null) return null;
                return movieDirector;
            }
        }
        public string Directors {
            get {
                List<string> ret = new List<string>();
                foreach(MovieArtist director in Artists) {
                    if(director.Artist != null && director.Line != null && director.Line.Name == ReferenceData.DirectorString)
                        ret.Add(director.Artist.FullName);
                }
                return StringHelper.GetStringByArrayList(ret);
            }
        }
        public int PicturesCount { get { return Pictures.Count; } }
        [Association("Movies-Countries")]
        public XPCollection<Country> Countries { get { return GetCollection<Country>("Countries"); } }
        [NonPersistent]
        public string CountriesAsString {
            get {
                XPCollection<Country> countries = new XPCollection<Country>(Countries);
                countries.Sorting.Add(new SortProperty("[Acronym]", SortingDirection.Ascending));
                string ret = string.Empty;
                foreach(Country country in countries) {
                    ret += ", " + country.Acronym;
                }
                if(ret.Length != 0) ret = ret.Substring(2, ret.Length - 2);
                return ret;
            }
            set {
                while(Countries.Count > 0) Countries.Remove(Countries[0]);
                string[] acronyms = value.Split(new char[] { ',', ' ' });
                foreach(string acronym in acronyms) {
                    if(acronym.Length == 0) continue;
                    Country country = Session.FindObject<Country>(CriteriaOperator.Parse("Acronym = ?", acronym));
                    if(country == null) throw new ArgumentException();
                    Countries.Add(country);
                }
            }
        }
        public XPCollection<Rent> Rents {
            get {
                XPCollection<Rent> ret = new XPCollection<Rent>(Session, false);
                foreach(MovieItem item in Items) {
                    foreach(Rent rent in item.Rents) {
                        ret.Add(rent);
                    }
                }
                return ret;
            }
        }
        public MoviePicture AddPicture(Image picture) {
            return new MoviePicture(this, picture);
        }
        public MoviePicture AddPicture(Image picture, string description) {
            return new MoviePicture(this, picture, description);
        }
        public XPCollection<MovieItem> GetActiveItems() {
            return new XPCollection<MovieItem>(Items, CriteriaOperator.Parse("Status = ?", MovieItemStatus.Active));
        }
        public XPCollection<MovieItem> GetActiveAvailableForSellItems() {
            return new XPCollection<MovieItem>(Items, CriteriaOperator.Parse("Status = ? and AvailableForSell = ?", MovieItemStatus.Active, true));
        }
        public MovieItem AddItem() {
            return new MovieItem(this);
        }
        internal MovieItem AddItem(MovieItemFormat format, decimal sellPrice) {
            return new MovieItem(this, format, sellPrice, string.Format(Properties.Resources.ShelfPattern, this.MovieId % ReferenceData.ShelvesCount + 1));
        }
        public bool IsAvailableForRent { get { return GetActiveItems().Count > 0; } }
        public void AddArtist(Artist artist, MovieArtistLine line) {
            AddArtist(artist, line, string.Empty);
        }
        public void AddArtist(Artist artist, MovieArtistLine line, string description) {
            new MovieArtist(this, artist, line, description); 
        }
        public void AddCompany(Company company) {
            AddCompany(company, string.Empty);
        }
        public void AddCompany(Company company, string description) {
            new MovieCompany(this, company, description);
        }
        public override bool AllowDelete {
            get {
                return base.AllowDelete && Session.FindObject<MovieItem>(CriteriaOperator.Parse("Movie = ? and AllowDelete = ?", this, false)) == null;
            }
        }
        public override string ToString() {
            return MovieTitle;
        }
        public string GetMovieInfoHtml() {
            return string.Format("<b>{0}</b>\r\n<i>{2:D}</i>\r\r\n{1}", Title, Plot, ReleaseDate);
        }
        public string GetFullMovieInfoHtml() {
            string ret = string.Format("<Size=+1><b>{0}</b> ({1})<Size=-1>", Title, ReleaseDate.HasValue ? ReleaseDate.Value.Year.ToString() : "?");
            ret += string.Format("\r\n{1}: <b>{0}</b>", Genre, Properties.Resources.Genre);
            if(!string.IsNullOrEmpty(Directors)) ret += string.Format("\r\n{1}: <b>{0}</b>", Directors, Properties.Resources.Directors);
            if(Language != null) ret += string.Format("\r\n{1}: <b>{0}</b>", Language.Name, Properties.Resources.Language);
            if(RunTime != null) ret += string.Format("\r\n{1}: <b>{0}</b>", RunTime, Properties.Resources.RunTime);
            if(!string.IsNullOrEmpty(AspectRatio)) ret += string.Format("\r\n{1}: <b>{0}</b>", AspectRatio, Properties.Resources.AspectRatio);
            return ret;
        }
        #region IComparable Members
        int IComparable.CompareTo(object obj) {
            return string.Compare(MovieTitle, string.Format("{0}", obj));
        }
        #endregion
    }
    public class Company : VideoRentBaseObject {
        string name;
        CompanyType type;
        string webSite;
        Country country;

        public Company(Session session) : base(session) { }
        public Company(Session session, int selfId)
            : this(session) {
            SelfId = selfId;
        }
        public Company(Session session, string name)
            : this(session) {
            Name = name;
        }
        public Company(Session session, int selfId, string name)
            : this(session, selfId) {
            Name = name;
        }
        public Company(Session session, string name, Country country, CompanyType type, string webSite)
            : this(session, name) {
            Country = country;
            Type = type;
            WebSite = webSite;
        }
        public Company(Session session, int selfId, string name, Country country, CompanyType type, string webSite)
            : this(session, selfId, name) {
            Country = country;
            Type = type;
            WebSite = webSite;
        }
#if SL
        [Indexed(Unique = true)]
        [Persistent("CompanyId"), Browsable(false), Private("", true)]
        public int _CompanyId {
            get { return GetPropertyValue<int>("_CompanyId"); }
            set { SetPropertyValue<int>("_CompanyId", value); }
        }
        [PersistentAlias("_CompanyId")]
        public int CompanyId {
            get { return GetPropertyValue<int>("_CompanyId"); }
            protected set { SetPropertyValue<int>("_CompanyId", value); }
        }
#else
        [Persistent, Indexed(Unique = true)]
        public int CompanyId {
            get { return GetPropertyValue<int>("CompanyId"); }
            protected set { SetPropertyValue<int>("CompanyId", value); }
        }
#endif
        protected override string GeneratedIdType { get { return "Company"; } }
        public string Name {
            get { return name; }
            set { SetPropertyValue<string>("Name", ref name, value); }
        }
        public CompanyType Type {
            get { return type; }
            set { SetPropertyValue<CompanyType>("Type", ref type, value); }
        }
        public string WebSite {
            get { return webSite; }
            set { SetPropertyValue<string>("WebSite", ref webSite, value); }
        }
        [Association("Companies-Country")]
        public Country Country {
            get { return country; }
            set { SetPropertyValue<Country>("Country", ref country, value); }
        }
        [Association("Company-MovieCompany"), Aggregated]
        public XPCollection<MovieCompany> Movies { get { return GetCollection<MovieCompany>("Movies"); } }
        public int MoviesCount { get { return Movies.Count; } }
        public string TypeName {
            get {
                if(Type != null) return Type.Name;
                return string.Empty;
            } 
        }
        public override string ToString() { return Name; }
    }
    public class MovieCompany : VideoRentBaseObject {
        Company company;
        Movie movie;
        string description;
        
        public MovieCompany(Session session) : base(session) { }
        public MovieCompany(Movie movie, Company company, string description)
            : this(movie.Session) {
            if(company == null) throw new ArgumentNullException("company");
            Movie = movie;
            Company = company;
            Description = description;
        }
        public MovieCompany(Movie movie)
            : this(movie.Session) {
            Movie = movie;
        }
        public MovieCompany(Company company)
            : this(company.Session) {
            Company = company; ;
        }
        [Association("Movie-MovieCompany"), DeleteIfNull]
        public Movie Movie {
            get { return movie; }
            set { SetPropertyValue<Movie>("Movie", ref movie, value); }
        }
        [Association("Company-MovieCompany"), DeleteIfNull]
        public Company Company {
            get { return company; }
            set { SetPropertyValue<Company>("Company", ref company, value); }
        }
        [Size(SizeAttribute.Unlimited)]
        public string Description {
            get { return description; }
            set { SetPropertyValue<string>("Description", ref description, value); }
        }
    }
    public class MovieArtistLine : VideoRentBaseObject {
        string name;

        public MovieArtistLine(Session session) : base(session) { }
        public MovieArtistLine(Session session, string name) : this(session) {
            Name = name;
        }
        [Indexed(Unique = true)]
        public string Name {
            get { return name; }
            set { SetPropertyValue<string>("Name", ref name, value); }
        }
        public static MovieArtistLine GetDirector(Session session) { return FromName(session, ReferenceData.DirectorString); }
        public static MovieArtistLine FromName(Session session, string name) {
            return session.FindObject<MovieArtistLine>(CriteriaOperator.Parse("Name = ?", name));
        }
        public override string ToString() {
            return Name;
        }
    }
    public class CompanyType : VideoRentBaseObject {
        string name;

        public CompanyType(Session session) : base(session) { }
        public CompanyType(Session session, string name) : this(session) {
            Name = name;
        }
        [Indexed(Unique = true)]
        public string Name {
            get { return name; }
            set { SetPropertyValue<string>("Name", ref name, value); }
        }
        public override string ToString() {
            return string.Format("{0}", Name);
        }
    }
    public class Artist : Person {
        string birthName;
        Country birthCountry;
        string birthLocation;
        string biography;
        string nickName;
        string link;

        public Artist(Session session) : base(session) { }
        public Artist(Session session, int selfId)
            : this(session) {
            SelfId = selfId;
        }
        public Artist(Session session, string firstName, string lastName)
            : this(session) {
            FirstName = firstName;
            LastName = lastName;
        }
        public Artist(Session session, int selfId, string firstName, string lastName)
            : this(session, selfId) {
            FirstName = firstName;
            LastName = lastName;
        }
#if SL
        [Indexed(Unique = true)]
        [Persistent("ArtistId"), Browsable(false), Private("", true)]
        public int _ArtistId {
            get { return GetPropertyValue<int>("_ArtistId"); }
            set { SetPropertyValue<int>("_ArtistId", value); }
        }
        [PersistentAlias("_ArtistId")]
        public int ArtistId {
            get { return GetPropertyValue<int>("_ArtistId"); }
            protected set { SetPropertyValue<int>("_ArtistId", value); }
        }
#else
        [Persistent, Indexed(Unique = true)]
        public int ArtistId {
            get { return GetPropertyValue<int>("ArtistId"); }
            protected set { SetPropertyValue<int>("ArtistId", value); }
        }
#endif
        protected override string GeneratedIdType { get { return "Artist"; } }
        [Association("Artist-MovieArtist"), Aggregated]
        public XPCollection<MovieArtist> Movies { get { return GetCollection<MovieArtist>("Movies"); } }
        public string BirthName {
            get { return birthName; }
            set { SetPropertyValue<string>("BirthName", ref birthName, value); }
        }
        public Country BirthCountry {
            get { return birthCountry; }
            set { SetPropertyValue<Country>("BirthCountry", ref birthCountry, value); }
        }
        public string BirthLocation {
            get { return birthLocation; }
            set { SetPropertyValue<string>("BirthLocation", ref birthLocation, value); }
        }
        [Size(SizeAttribute.Unlimited)]
        public string Biography {
            get { return biography; }
            set { SetPropertyValue<string>("Biography", ref biography, value); }
        }
        public string NickName {
            get { return nickName; }
            set { SetPropertyValue<string>("NickName", ref nickName, value); }
        }
        public string Link {
            get { return link; }
            set { SetPropertyValue<string>("Link", ref link, value); }
        }
        [Association("Artist-ArtistPictures"), Aggregated]
        public XPCollection<ArtistPicture> Pictures { get { return GetCollection<ArtistPicture>("Pictures"); } }
        public override string FullName {
            get {
                string ret = FirstName == null ? string.Empty : FirstName.Trim();
                string nickNameTrim = NickName == null ? string.Empty : NickName.Trim();
                string lastNameTrim = LastName == null ? string.Empty : LastName.Trim();
                if(nickNameTrim.Length != 0) ret += (ret.Length == 0 ? string.Empty : " ") + "\"" + nickNameTrim + "\"";
                if(lastNameTrim.Length != 0) ret += (ret.Length == 0 ? string.Empty : " ") + lastNameTrim;
                return ret;
            }
        }
        public ArtistPicture AddPicture(Image picture) {
            return new ArtistPicture(this, picture);
        }
        public ArtistPicture AddPicture(Image picture, string description) {
            return new ArtistPicture(this, picture, description);
        }
        public override string ToString() { return FullName; }
    }
    public class Country : VideoRentBaseObject {
        string name;
        string acronym;
        Image flag;

        public Country(Session session) : base(session) { }
        public Country(Session session, string acronym, string name, Image flag)
            : this(session) {
            Acronym = acronym;
            Name = name;
            Flag = flag;
        }
        public string Name {
            get { return name; }
            set { SetPropertyValue<string>("Name", ref name, value); }
        }
        [Indexed(Unique = true)]
        public string Acronym {
            get { return acronym; }
            set { SetPropertyValue<string>("Acronym", ref acronym, value); }
        }
        [ValueConverter(typeof(ImageValueConverter))]
        public Image Flag {
            get { return flag; }
            set { SetPropertyValue<Image>("Flag", ref flag, value); }
        }
        [Association("Companies-Country")]
        public XPCollection<Company> Companies { get { return GetCollection<Company>("Companies"); } }
        [Association("Movies-Countries")]
        public XPCollection<Movie> Movies { get { return GetCollection<Movie>("Movies"); } }
        public override string ToString() {
            string ret = string.Format("{0}", Name);
            return ret;
        }
    }
    public class MovieArtist : VideoRentBaseObject {
        Artist artist;
        Movie movie;
        MovieArtistLine line;
        string description;

        public MovieArtist(Session session) : base(session) { }
        public MovieArtist(Artist artist)
            : this(artist.Session) {
            Artist = artist;
        }
        public MovieArtist(Movie movie, Artist artist, MovieArtistLine line, string description)
            : this(movie.Session) {
            if(artist == null) throw new ArgumentNullException("artist");
            Movie = movie;
            Artist = artist;
            Line = line;
            Description = description;
        }
        public MovieArtist(Movie movie)
            : this(movie.Session) {
            Movie = movie;
        }
        [Association("Movie-MovieArtist"), DeleteIfNull]
        public Movie Movie {
            get { return movie; }
            set { SetPropertyValue<Movie>("Movie", ref movie, value); }
        }
        [Association("Artist-MovieArtist"), DeleteIfNull]
        public Artist Artist {
            get { return artist; }
            set { SetPropertyValue<Artist>("Artist", ref artist, value); }
        }
        [Size(SizeAttribute.Unlimited)]
        public string Description {
            get { return description; }
            set { SetPropertyValue<string>("Description", ref description, value); }
        }
        public MovieArtistLine Line {
            get { return line; }
            set { SetPropertyValue<MovieArtistLine>("Line", ref line, value); }
        }
    }
    public class Picture : VideoRentBaseObject {
        Image image;
        string description;

        public Picture(Session session) : base(session) { }
        [ValueConverter(typeof(ImageValueConverter))]
        public Image Image {
            get { return image; }
            set { SetPropertyValue<Image>("Image", ref image, value); }
        }
        [Size(200)]
        public string Description {
            get { return description; }
            set { SetPropertyValue<string>("Description", ref description, value); }
        }
    }
    public class ArtistPicture : Picture {
        Artist artist;

        public ArtistPicture(Session session) : base(session) { }
        public ArtistPicture(Artist artist, Image picture)
            : this(artist.Session) {
            Artist = artist;
            Image = picture;
        }
        public ArtistPicture(Artist artist, Image picture, string description)
            : this(artist, picture) {
            Description = description;
        }
        [Association("Artist-ArtistPictures"), DeleteIfNull]
        public Artist Artist {
            get { return artist; }
            set { SetPropertyValue<Artist>("Artist", ref artist, value); }
        }
    }
    public class MoviePicture : Picture {
        Movie movie;

        public MoviePicture(Session session) : base(session) { }
        public MoviePicture(Movie movie, Image picture)
            : this(movie.Session) {
            Movie = movie;
            Image = picture;
        }
        public MoviePicture(Movie movie, Image picture, string description)
            : this(movie, picture) {
            Description = description;
        }
        [Association("Movie-MoviePictures"), DeleteIfNull]
        public Movie Movie {
            get { return movie; }
            set { SetPropertyValue<Movie>("Movie", ref movie, value); }
        }
    }
    public class Language : VideoRentBaseObject {
        string name;
        public Language(Session session) : base(session) { }
        public Language(Session session, string name) : this(session) {
            Name = name;
        }
        [Indexed(Unique = true)]
        public string Name {
            get { return name; }
            set { SetPropertyValue<string>("Name", ref name, value); }
        }
        [Association("Movies-Language")]
        public XPCollection<Movie> Movies { get { return GetCollection<Movie>("Movies"); } }
    }
}
