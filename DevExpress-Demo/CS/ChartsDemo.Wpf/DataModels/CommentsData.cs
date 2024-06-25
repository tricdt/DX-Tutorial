using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ChartsDemo {

    [XmlRoot("Comments")]
    public class CommentsData : List<CommentsInfo> {
        public static CommentsData Load() {
            XmlSerializer serializer = XmlSerializer.FromTypes(new[] { typeof(CommentsData) })[0]; 
            return (CommentsData)serializer.Deserialize(DataLoader.LoadFromResources("/Data/CommentsData.xml"));
        }
    }


    public class CommentsInfo {
        public string Category { get; set; }
        public List<CommentInfo> Items { get; set; }
    }


    public class CommentInfo {
        public DateTime Date { get; set; }
        public int CommentCount { get; set; }
    }

}
