using System;
using System.Collections.Generic;
using FFImageLoading.Cache;
using FFImageLoading.Forms;

namespace OneClickN1.Model
{
    public class News
    {
        public int loacalId { get; set; }
        public string caption { get; set; }
        public string overview { get; set; }
        public string id { get; set; }
        public string imageURL { get; set; }
        public string myImageURL { get; set; }
		//{
		//	get
		//	{
		//		return imageURL;
		//	}

		//	set
		//	{
  //              imageURL = value;
  //              CachedImage.InvalidateCache(imageURL, CacheType.All, true);
		//	}
		//}
        public string imageSource { get; set; }
        public string newsSource { get; set; }
        public string newsTime  { get; set; }
    }
}