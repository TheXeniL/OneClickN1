using System;
using System.Collections.Generic;

namespace OneClickN1.Model
{
    public class News
    {
        public int loacalId { get; set; }
        public string caption { get; set; }
        public string overview { get; set; }
        public string id { get; set; }
        public string imageURL { get; set; }
        public string imageSource { get; set; }
        public string newsSource { get; set; }
        public string newsTime  { get; set; }
    }
}