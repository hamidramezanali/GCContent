using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication10.Models.Home
{
    public class MakeAnArray
    {
        public static string countGC(List<string> Sequences)
        {
            int[] GC = new int[100];
            int[] Total = new int[100];
            double[] MeanGC = new double[100];
            string fileResult;

       

            foreach (var Seq in Sequences)
            {
                for (int i = 0; i < Seq.Length; i++)
                {
                    if ((Seq[i] == 'G') || (Seq[i] == 'C'))
                        GC[i] += 1;
                    Total[i] += 1;

                }
            }
            for (int i = 0; i < 100; i++)
                if (Total[i] != 0) MeanGC[i] = Math.Round((double)GC[i] / Total[i],2);

            fileResult = "[";
            for (int i = 0; i < 100; i++)
            {
                if (i < 100 - 1) fileResult += MeanGC[i] + ",";
                else if (i == 100 - 1) fileResult += MeanGC[i] + "]";
            }
            return fileResult;
        }

        public int min { get; set; }
        public int max { get; set; }
        public int nodes { get; set; }
        public int percise { get; set; }
        public string result { get; set; }
       

        public MakeAnArray(int _min, int _max, int _nodes, int _percise)
        {
             min = _min;
            max = _max;
            nodes=_nodes;
            percise = _percise;
        }

        public  string GetResult()
        {
            Random rnd = new Random();
            List<float> tb = new List<float>(nodes);
            int maxf = max * percise;
            int minf = min * percise;
            for (int i = 0; i < tb.Capacity; i++)
            {
                tb.Add((float)rnd.Next(minf, maxf) / (float)percise);

            }
            result = "[";
            for (int i = 0; i < tb.Capacity; i++)
            {
                if (i < tb.Capacity - 1) result += tb[i] + ",";
                else if (i == tb.Capacity - 1) result += tb[i] + "]";
            }
            return result;

        }
    }
}
