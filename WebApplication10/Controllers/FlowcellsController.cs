using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoveSeat;
using MyCouch;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using static WebApplication10.Models.Home.NGS;

namespace WebApplication10.Controllers
{
    public class FlowcellsController : Controller

    {
        

        public IActionResult Index()
        {
            WebRequest myRequest = WebRequest.Create("http://10.5.68.185:5984/xten_db/b8c038313b6075ce2bfddca606000255");

            // Return the response. 
            WebResponse myResponse = myRequest.GetResponse();


            Stream dataStream = myResponse.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            // Code to use the WebResponse goes here.
            ViewBag.Response = responseFromServer;
            var result = JsonConvert.DeserializeObject<Flowcell>(responseFromServer);
            // Close the response to free resources.
            myResponse.Close();


            return View(result);
        }
        public class doc1
        {
            public string _id { get; set; }
            public string _rev { get; set; }
            public string name { get; set; }
            public string family { get; set; }
            public Json_stats json_stats { get; set; }
            public string run_setup { get; set; }
            public RunInfo runinfo { get; set; }
        }
        public class Json_stats
        {
            public int Runnumber { get; set; }
            public ConversionResult[]  conversionresults { get; set; }
            public string flowcell { get; set; }
           
        }
        public class ConversionResult
        {
            public int yield { get; set; }
            public int lanenumber { get; set; }
            public int totalclusterspf { get; set; }
            public int totalclustersraw { get; set; }
        }
        public class RunInfo
        {
            public int number { get; set; }
            public string instrument { get; set; }
            public string date { get; set; }

        }

    }
}