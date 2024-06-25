using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Utils;
using System.Drawing;

namespace DevExpress.VideoRent.Resources {
    public class ImagesHelper {
        static ImagesHelper current;
        static ImageCollection diskIcons;
        static ImageCollection personImages;
        static ImageCollection receiptTypeImages;
        static ImageCollection ratingImages;
        static ImageCollection ratingLargeImages;
        static ImageCollection activeRents;
        static ImageCollection columnHeaderImages;
        static ImageCollection columnHeaderSmoothImages;
        static ImageCollection barImages;
        static ImageCollection folderIcons;
        static Image aboutImage;

        public static ImagesHelper Current {
            get {
                if(current == null)
                    current = new ImagesHelper();
                return current;
            }
        }
        public ImageCollection DiskIcons {
            get {
                if(ImagesHelper.diskIcons == null)
                    ImagesHelper.diskIcons = DevExpress.Utils.Controls.ImageHelper.CreateImageCollectionFromResources("DevExpress.VideoRent.Resources.Images.DiskFormats.png", typeof(ImagesHelper).Assembly, new Size(16, 16));
                return ImagesHelper.diskIcons;
            }
        }
        public ImageCollection PersonImages {
            get {
                if(ImagesHelper.personImages == null)
                    ImagesHelper.personImages = DevExpress.Utils.Controls.ImageHelper.CreateImageCollectionFromResources("DevExpress.VideoRent.Resources.Images.Persons.png", typeof(ImagesHelper).Assembly, new Size(16, 16));
                return ImagesHelper.personImages;
            }
        }
        public ImageCollection ReceiptTypeImages {
            get {
                if(ImagesHelper.receiptTypeImages == null)
                    ImagesHelper.receiptTypeImages = DevExpress.Utils.Controls.ImageHelper.CreateImageCollectionFromResources("DevExpress.VideoRent.Resources.Images.ReceiptType.png", typeof(ImagesHelper).Assembly, new Size(16, 16));
                return ImagesHelper.receiptTypeImages;
            }
        }
        public ImageCollection RatingImages {
            get {
                if(ratingImages == null)
                    ratingImages = DevExpress.Utils.Controls.ImageHelper.CreateImageCollectionFromResources("DevExpress.VideoRent.Resources.Images.RatingsSmall.png", typeof(ImagesHelper).Assembly, new Size(38, 16));
                return ratingImages;
            }
        }
        public ImageCollection RatingLargeImages {
            get {
                if(ratingLargeImages == null)
                    ratingLargeImages = DevExpress.Utils.Controls.ImageHelper.CreateImageCollectionFromResources("DevExpress.VideoRent.Resources.Images.RatingsLarge.png", typeof(ImagesHelper).Assembly, new Size(250, 37));
                return ratingLargeImages;
            }
        }
        public ImageCollection ActiveRents {
            get {
                if(activeRents == null)
                    activeRents = DevExpress.Utils.Controls.ImageHelper.CreateImageCollectionFromResources("DevExpress.VideoRent.Resources.Images.ActiveRents.png", typeof(ImagesHelper).Assembly, new Size(16, 16));
                return activeRents;
            }
        }
        public ImageCollection BarImages {
            get {
                if(ImagesHelper.barImages == null)
                    ImagesHelper.barImages = DevExpress.Utils.Controls.ImageHelper.CreateImageCollectionFromResources("DevExpress.VideoRent.Resources.Images.BarImages16x16.png", typeof(DevExpress.VideoRent.Resources.ImagesHelper).Assembly, new System.Drawing.Size(16, 16));
                return ImagesHelper.barImages;
            }
        }
        public ImageCollection ColumnHeaderImages {
            get {
                if(ImagesHelper.columnHeaderImages == null)
                    ImagesHelper.columnHeaderImages = DevExpress.Utils.Controls.ImageHelper.CreateImageCollectionFromResources("DevExpress.VideoRent.Resources.Images.ColumnHeaderImages.png", typeof(DevExpress.VideoRent.Resources.ImagesHelper).Assembly, new System.Drawing.Size(13, 13));
                return ImagesHelper.columnHeaderImages;
            }
        }
        public ImageCollection ColumnHeaderSmoothImages {
            get {
                if(ImagesHelper.columnHeaderSmoothImages == null)
                    ImagesHelper.columnHeaderSmoothImages = DevExpress.Utils.Controls.ImageHelper.CreateImageCollectionFromResources("DevExpress.VideoRent.Resources.Images.ColumnHeaderSmoothImages.png", typeof(DevExpress.VideoRent.Resources.ImagesHelper).Assembly, new System.Drawing.Size(13, 13));
                return ImagesHelper.columnHeaderSmoothImages;
            }
        }
        public ImageCollection FolderIcons {
            get {
                if(folderIcons == null)
                    folderIcons = DevExpress.Utils.Controls.ImageHelper.CreateImageCollectionFromResources("DevExpress.VideoRent.Resources.Images.FolderIcons16x16.png", typeof(DevExpress.VideoRent.Resources.ImagesHelper).Assembly, new System.Drawing.Size(16, 16));
                return folderIcons;
            }
        }
        public Image AboutImage {
            get {
                if(ImagesHelper.aboutImage == null)
                    ImagesHelper.aboutImage = ResourceImageHelper.CreateImageFromResources("DevExpress.VideoRent.Resources.Images.About.png", typeof(DevExpress.VideoRent.Resources.ImagesHelper).Assembly);
                return ImagesHelper.aboutImage;
            }
        }
    }
}
