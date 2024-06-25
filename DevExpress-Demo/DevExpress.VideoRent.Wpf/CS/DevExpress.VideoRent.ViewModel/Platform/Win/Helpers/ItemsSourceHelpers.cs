using System;
using System.Collections.Generic;
using DevExpress.VideoRent.Resources;

namespace DevExpress.VideoRent.ViewModel {
    public class ItemsSourceHelper : ItemsSourceHelperBase {
        static List<GenderItem> genderList;
        static List<RatingItem> ratingsList;
        static List<FormatItem> formatsList;
        
        public static List<GenderItem> GendersList {
            get {
                if(genderList != null) return genderList;
                genderList = GenerateGendersSource();
                return genderList;
            }
            set { genderList = value; }
        }
        public static List<RatingItem> RatingsList {
            get {
                if(ratingsList != null) return ratingsList;
                ratingsList = GenerateMovieRatingsSource();
                return ratingsList;
            }
            set { ratingsList = value; }
        }
        public static List<FormatItem> FormatsList {
            get {
                if(formatsList != null) return formatsList;
                formatsList = GenerateMovieFormatsSource();
                return formatsList;
            }
            set { formatsList = value; }
        }
        static List<GenderItem> GenerateGendersSource() {
            List<GenderItem> toReturn = new List<GenderItem>();
            toReturn.Add(new GenderItem(DevExpress.VideoRent.Resources.ImagesSourceHelper.Current.PersonIcons[0], PersonGender.Male));
            toReturn.Add(new GenderItem(DevExpress.VideoRent.Resources.ImagesSourceHelper.Current.PersonIcons[1], PersonGender.Female));
            return toReturn;
        }
        static List<RatingItem> GenerateMovieRatingsSource() {
            List<RatingItem> list = new List<RatingItem>();
            MovieRating[] values = (MovieRating[])Enum.GetValues(typeof(MovieRating));
            for(int i = 0; i < values.Length; ++i) {
                list.Add(new RatingItem(DevExpress.VideoRent.Resources.ImagesSourceHelper.Current.Ratings[i], DevExpress.VideoRent.Resources.ImagesSourceHelper.Current.RatingsLarge[i], values[i]));
            }
            return list.Count == 0 ? null : list;
        }
        static List<FormatItem> GenerateMovieFormatsSource() {
            List<FormatItem> list = new List<FormatItem>();
            MovieItemFormat[] formats = (MovieItemFormat[])Enum.GetValues(typeof(MovieItemFormat));
            for(int i = 0; i < formats.Length; ++i) {
                list.Add(new FormatItem(DevExpress.VideoRent.Resources.ImagesSourceHelper.Current.DiskIcons[i], formats[i]));
            }
            return list.Count == 0 ? null : list;
        }
    }
}
