using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Net;

namespace SimsimiChatBotApp.Helper
{
    public class HttpDataHandler
    {

        static string stream = null;
        public HttpDataHandler() { }

        public String GetHttpData(string urlString)
        {
            try
            {
                URL url = new URL(urlString);
                HttpURLConnection urlConnection = (HttpURLConnection)url.OpenConnection();
                if(urlConnection.ResponseCode==HttpStatus.Ok)
                {
                    BufferedReader r = new BufferedReader(new InputStreamReader(urlConnection.InputStream));
                    StringBuilder sb = new StringBuilder();
                    String line;
                    while ((line=r.ReadLine())!=null)
                        sb.AppendLine(line);
                    stream = sb.ToString();
                    urlConnection.Disconnect();

                }
                return stream;

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}