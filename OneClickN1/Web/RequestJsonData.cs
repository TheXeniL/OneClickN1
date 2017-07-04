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
        public bool JsonParseSucces;
        public bool HttpConnectStatus;



        public async Task<string> MakeGetRequest(string url)
        {
            var request = WebRequest.Create(url);


            try
            {
                HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
					var content = reader.ReadLine();

                    if (content == "false")
                    {
                        JsonParseSucces = false;
                        return null;
                    }

                    else
                    {
                        JsonParseSucces = true;
                        jArray = Newtonsoft.Json.Linq.JArray.Parse(content);
                        return content;
                    }
                }

            }
            catch(Exception e)
            {
                HttpConnectStatus = false;
                return null;
            }
        }


    }
}

