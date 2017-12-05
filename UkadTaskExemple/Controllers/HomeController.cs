using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskCalculate;
using UkadTask.Models;

namespace UkadTask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        { 
            return View();
        }
 
        [HttpPost]
        public ActionResult Calculate(string url)
        {
            Calculate calc = new Calculate();
            calc.CalcsAll(url);
            List<UrlAndTime> li=new List<UrlAndTime>();
            List<UrlAndTimes> tmpUaT= calc.needUrlAndTimes;

            //List<string> ti = new List<string>();

            foreach (UrlAndTimes uat in tmpUaT)
            {
                UrlAndTime urlAndTime = new UrlAndTime
                {
                    Url = uat.url,
                    Time = uat.time
                };
                li.Add(urlAndTime);
                //ti.Add(uat.url + "                  " + uat.time + " sec");
            }
 
            if (li.Count == 0)
            {
                //ti.Add("Map site is not found");
                UrlAndTime urlAndTime = new UrlAndTime
                {
                    Url = "Map site is not found",
                    Time = 0
                };
                li.Add(urlAndTime);
            }
            ViewBag.Timer = li;
            return View("Index",li);
        }
    }
}