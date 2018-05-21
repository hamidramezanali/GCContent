using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication10.Models.Home
{
    public class NGS
    {
        public class Flowcell
        {

            public string _id { get; set; }
            public string _rev { get; set; }
            public string name { get; set; }
            public string family { get; set; }
            public Json_stats json_stats { get; set; }
            public string run_setup { get; set; }
            public RunInfo runinfo { get; set; }          
            //public DemultiplexingConfig demultiplexingconfig { get; set; }         
         
            public RunParameters runparameters { get; set; }
            //public Undetermined undetermined { get; set; }
            //public Illumina illumina { get; set; }
            //public Samplesheet_csv samplesheet_csv { get; set; }




        }

        public class Samplesheet_csv
        {
        }

        public class Run_setup
        {
        }

        public class Illumina
        {
        }

        public class Undetermined
        {
        }

        public class RunParameters
        {
            public string  mcsversion { get; set; }
        }

        public class RunInfo
        {
            public int number { get; set; }
            public string instrument { get; set; }
            public string date { get; set; }
        }

        public class Json_stats
        {
            public int Runnumber { get; set; }
            public ConversionResult[] conversionresults { get; set; }
            public string flowcell { get; set; }
            public string  runid { get; set; }

        }

        public class ConversionResult
        {
            public int yield { get; set; }
            public int lanenumber { get; set; }
            public int totalclusterspf { get; set; }
            public int totalclustersraw { get; set; }
        }


        public class DemultiplexingConfig
        {
        }


    }
}

    

 