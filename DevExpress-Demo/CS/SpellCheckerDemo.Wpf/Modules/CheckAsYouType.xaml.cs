using System.Collections.Generic;
using System.Windows;

namespace SpellCheckerDemo {
    public partial class CheckAsYouType : SpellCheckerDemoModule {
        #region TextEn
        public static readonly string TextEn = @"HANS IN LYCK 

Hans had served his master forseven years, so he said to him, ""Master, my time is up; now I should be glad to to go back home to my mother; give me my wages."" The master answered, ""You have served me faithfully and honesly; as the service was so shall the reward be;"" and he gave Hans a piece of gold as big as his head. Hans pulled his handkerchief out of his pocket, wrapped up the lump init, put it on his shoulder, and set out on the way home. 

As he went on, always puting one foot before the other, he saw a horseman trotting quickly and merrily by on a lively horse. ""Ah!"" said Hans quite loud, ""what a fine thing it is to ride! There you sit as on a chair; you stumble over no stones, you save your shoes, and get on, you don""t know how."" 

The rider, who had heard him, stopped and called out, ""Hollo! Hans, why do you go on foot, then?"" ""I must,"" answered he, ""for I have this lump to carry home; it is true that it is gold, but I cannot hold my head straight for it, and it hurts my shoulder."" 

""I will tell you what,"" said the rider, ""we will exchange: I will give you my horse, and you can give me your lump."" 

""With all my heart,"" said Hans, ""but I can tell you, you will have to crawl along with it."" 

The rider got down, took the gold, and helped Hans up; then gave him the briddle tight in his hands and said, ""If you want to go at a really good pace, you must click your tongue and call out, ""Jup! Jup!"" ";
        #endregion

        #region TextRu
        public static readonly string TextRu = @"Сачстливчик Ганс 

Ганс прослужил семь лет у своего господина и стал говорить ему: ""Срок моей службы, сударь, миновал уже, и я бы очень охотно вернулся теперь к моей матери; а потому пожалуйте мне мое жалованье"". 

Хозяин его отвечал: ""Ты служил мне верно и честно; по твоей службе должна быть тебе и награда"", — и дал ему кусок золото величиной с его голову. Ганс вытащил платочек из кармана, завернул в него слиток золота, положил его на плечо и пустился домой.

Шел он своей дорогой, плелся нога за ногу и увидел всадника, который бодро и весело прорысил мимо него на славной лошади. ""Ах, — сказал Ганс вслух, — что за славная штука эта верховая езда! Сидишь словно на стуле, ни на какой камень не спотыкаешься, обуви не изнашиваешь, и едешь себе вперед да вперед, сам того не замечая"". 

Всадник, услышав это, сдержал коня и крикнул: ""Эй, Ганс, так зачем же ты пешком-то идешь?"" — ""Приходится идти пешком, коли нужно домой снести вот этот слиток: оно, положим, и золото, однако вот голову приходится набок держать, да и плечо оттянуло"". — ""А знаешь ли что? — сказал всадник. — Давай меняться: я тебе отдам своего коня, а ты мне свой слиток"". — ""С удовольствием, — сказал Ганс, — но только предупреждаю, что придется тебе с ним тащиться». 

Всадник спешился, взял у ГАнса слиток золота и помог ему взобраться на седло; потом дал ему поводья в руки и добавил: ""Если хочешь, чтобы конь бежал быстро, тебе надо только языком прищелкнуть да крикнуть: гоп, гоп"". ";
        #endregion

        public CheckAsYouType() {
            InitializeComponent();
        }
        protected override List<FrameworkElement> CheckingElements { get { return new List<FrameworkElement>() { textBox, richTextBox, textEdit }; } }
    }
}
