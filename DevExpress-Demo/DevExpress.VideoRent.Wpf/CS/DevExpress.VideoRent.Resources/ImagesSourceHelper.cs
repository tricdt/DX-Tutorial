using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Utils;
using System.Drawing;
using DevExpress.Utils.Controls;
using DevExpress.VideoRent.Resources.Helpers;
using System.Windows.Media;

namespace DevExpress.VideoRent.Resources {
    public class ImagesSourceHelper {
        static ImagesSourceHelper current;
        static List<ImageSource> diskIcons;
        static List<ImageSource> personImages;
        static List<ImageSource> receiptTypeImages;
        static List<ImageSource> ratingImages;
        static List<ImageSource> ratingLargeImages;
        static List<ImageSource> activeRents;
        static List<ImageSource> columnHeaderImages;
        static List<ImageSource> columnHeaderSmoothImages;
        static List<ImageSource> barImages;
        static List<ImageSource> folderIcons;

        public static ImagesSourceHelper Current {
            get {
                if(current == null)
                    current = new ImagesSourceHelper();
                return current;
            }
        }
        public List<ImageSource> DiskIcons {
            get {
                if(diskIcons == null)
                    diskIcons = DrawingImageToImageSourceHelper.Convert(ImagesHelper.Current.DiskIcons);
                return ImagesSourceHelper.diskIcons;
            }
        }
        public List<ImageSource> PersonIcons {
            get {
                if(personImages == null)
                    personImages = DrawingImageToImageSourceHelper.Convert(ImagesHelper.Current.PersonImages);
                return personImages;
            }
        }
        public List<ImageSource> ReceiptTypes {
            get {
                if(receiptTypeImages == null)
                    receiptTypeImages = DrawingImageToImageSourceHelper.Convert(ImagesHelper.Current.ReceiptTypeImages);
                return receiptTypeImages;
            }
        }
        public List<ImageSource> Ratings {
            get {
                if(ratingImages == null)
                    ratingImages = DrawingImageToImageSourceHelper.Convert(ImagesHelper.Current.RatingImages);
                return ratingImages;
            }
        }
        public List<ImageSource> RatingsLarge {
            get {
                if(ratingLargeImages == null)
                    ratingLargeImages = DrawingImageToImageSourceHelper.Convert(ImagesHelper.Current.RatingLargeImages);
                return ratingLargeImages;
            }
        }
        public List<ImageSource> RentTypes {
            get {
                if(activeRents == null)
                    activeRents = DrawingImageToImageSourceHelper.Convert(ImagesHelper.Current.ActiveRents);
                return activeRents;
            }
        }
        public List<ImageSource> BarIcons {
            get {
                if(barImages == null)
                    barImages = DrawingImageToImageSourceHelper.Convert(ImagesHelper.Current.BarImages);
                return barImages;
            }
        }
        public List<ImageSource> ColumnHeaderIcons {
            get {
                if(columnHeaderImages == null)
                    columnHeaderImages = DrawingImageToImageSourceHelper.Convert(ImagesHelper.Current.ColumnHeaderImages);
                return columnHeaderImages;
            }
        }
        public List<ImageSource> ColumnHeaderIconsSmooth {
            get {
                if(columnHeaderSmoothImages == null)
                    columnHeaderSmoothImages = DrawingImageToImageSourceHelper.Convert(ImagesHelper.Current.ColumnHeaderSmoothImages);
                return columnHeaderSmoothImages;
            }
        }
        public List<ImageSource> FolderIcons {
            get {
                if(folderIcons == null)
                    folderIcons = DrawingImageToImageSourceHelper.Convert(ImagesHelper.Current.FolderIcons);
                return folderIcons;
            }
        }
    }
}
