using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace OneClickN1
{
    public class RequestJsonData
    {
        public Newtonsoft.Json.Linq.JArray jArray = new Newtonsoft.Json.Linq.JArray();

        public RequestJsonData()
        {

        }

        public async Task<string> MakeGetRequest(string url)
        {
            var request = WebRequest.Create(url);

            HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    
                    var content = reader.ReadLine();
                    jArray = Newtonsoft.Json.Linq.JArray.Parse(content);
                    return content;
                }
            }
            else
            {
                return response.StatusCode.ToString();
            }
        }


    }
}

