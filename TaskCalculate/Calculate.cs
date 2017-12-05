using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TaskCalculate
{
    public class Calculate
    {
        public List<UrlAndTimes> needUrlAndTimes;
        public Calculate()
        {
            needUrlAndTimes = new List<UrlAndTimes>();
        }
        private void  CalcTimeUrl(string url)
        {

            Stopwatch timer = Stopwatch.StartNew();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException)
            { }
          
                float ti = (((float)timer.ElapsedTicks) / Stopwatch.Frequency);
            UrlAndTimes uAT;
            uAT.url = url;
            uAT.time = ti;
            needUrlAndTimes.Add(uAT);
        }

        public List<string> MakeRequest(string url= "http://scheduler-net.com/sitemap.xml")
        {
            XmlTextReader reader = new XmlTextReader(url);
            List<string> total = new List<string>();
            try {
                while (reader.Read())
                {

                    if (reader.IsStartElement("loc"))
                    {
                        total.Add(reader.ReadElementContentAsString());
                    }

                }
            } catch (Exception) { };
            return total;
        }
        private   void  CalcAll(string url)
        {
            List<string> listUrls = MakeRequest(url+ "/sitemap.xml");
            if(listUrls==null)
            {
                return;
            }
            Parallel.ForEach(listUrls, CalcTimeUrl);
             
        }
        public List<UrlAndTimes> CalcsAll(string url)
        {
            CalcAll(url);
            return needUrlAndTimes;
        }

    }
    public  struct UrlAndTimes
    {
        public string url;
        public float time;
    }
}

