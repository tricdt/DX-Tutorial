using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;


namespace GridDemo {
    [ContentProperty("Storyboard")]
    public class StoryboardContainer : Control {
        public static Storyboard CreateStoryboard(Control resourceHolder, string resourceName) {
            ContentControl c = new ContentControl() { Template = (ControlTemplate)resourceHolder.Resources[resourceName] };
            c.ApplyTemplate();
            StoryboardContainer container = (StoryboardContainer)VisualTreeHelper.GetChild(c, 0);
            return container.Storyboard;
        }
        public Storyboard Storyboard { get; set; }
    }
}
