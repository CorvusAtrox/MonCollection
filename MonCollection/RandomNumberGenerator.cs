//====================================================
//| Downloaded From                                  |
//| Visual C# Kicks - http://vcskicks.com/           |
//| License - http://vcskicks.com/license.html       |
//====================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace TrueRandomGenerator
{
    public class RandomNumberGenerator
    {
        //Returns a single random integer between two numbers, both inclusive        
        public static int GetRandomInt(int min, int max)
        {
            return GetRandomInts(min, max, 1)[0];
        }

        //Returns an array of random integers between two numbers, both inclusive        
        public static int[] GetRandomInts(int min, int max, int trials)
        {
            //Build the url string to www.random.org
            string url = "http://www.random.org/integers/?num=" + trials.ToString(); ;

            url += "&min=" + min.ToString();
            url += "&max=" + max.ToString();
            url += "&col=1&base=10&format=html&rnd=new";

            string data = DownloadData(url);

            if (data != string.Empty)
            {
                //Parse the data
                string startMarker = "<pre class=" + '"' + "data" + '"' + ">"; //<pre class="data">
                int j = data.IndexOf(startMarker);
                if (j != -1)
                {
                    int k = data.IndexOf("</pre>", j);
                    if (k != -1)
                    {
                        string intString = data.Substring(j + startMarker.Length, k - j - startMarker.Length);
                        intString = intString.Trim();

                        //Read each line
                        List<int> integers = new List<int>();
                        StringReader readLines = new StringReader(intString);

                        while (readLines.Peek() != -1)
                        {
                            integers.Add(int.Parse(readLines.ReadLine()));
                        }

                        return integers.ToArray();
                    }
                }
            }

            return new int[] { -1 };
        }

        public static bool HasQuota()
        {
            string url = "http://www.random.org/quota/?format=plain";

            int ret = Convert.ToInt32(DownloadData(url));

            return (ret > 0);
        }

        //Connects to URL to download data
        private static string DownloadData(string url)
        {
            try
            {
                //Get a data stream from the url
                WebRequest req = WebRequest.Create(url);
                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();

                //Download in chuncks
                byte[] buffer = new byte[1024];

                //Get Total Size
                int dataLength = (int)response.ContentLength;

                //Download to memory
                //Note: adjust the streams here to download directly to the hard drive
                MemoryStream memStream = new MemoryStream();
                while (true)
                {
                    //Try to read the data
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;
                    else
                        memStream.Write(buffer, 0, bytesRead);
                }

                //Convert the downloaded stream to a byte array
                string downloadedData = System.Text.ASCIIEncoding.ASCII.GetString(memStream.ToArray());

                //Clean up
                stream.Close();
                memStream.Close();

                return downloadedData;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

    }
}
