using System.Web;

namespace EventManager.Web.ViewModels.CrEvent
{

    public class ImageAndTitle
    {
        public ImageAndTitle()
        {

        }

        public ImageAndTitle(HttpPostedFileBase image, string title)
        {
            this.Image = image;
            this.Title = title;
        }

        public HttpPostedFileBase Image { get; set; }

        public string Title { get; set; }
    }
}