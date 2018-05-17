using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication10.Models.Home
{
    public class MakeAnArray
    {
        public static double AvgGCContent { get; set; }
        public static int  count { get; set; }
        public static string GCATNcount { get; set; }
        public static string countGC(List<string> Sequences)
        {
            int[] GC = new int[100];
            int[] GCATN = new int[5];
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
                    switch (Seq[i])
                    {
                        case 'G':
                            GCATN[0] += 1;
                            break;
                        case 'C':
                            GCATN[1] += 1;
                            break;
                        case 'A':
                            GCATN[2] += 1;
                            break;
                        case 'T':
                            GCATN[3] += 1;
                            break;
                        default:                        
                            GCATN[4] += 1;
                            break;
                    }

                }
            }
            GCATNcount = "[";
            for (int i = 0; i < 5; i++)
            {
                if (i < 4) GCATNcount += GCATN[i] + ",";
                else if (i == 4) GCATNcount += GCATN[i] + "]";
            }


            double total = 0;
            count = 0;
            for (int i = 0; i < 100; i++)
                if (Total[i] != 0)
                {
                    MeanGC[i] = Math.Round((double)GC[i] / Total[i], 2);
                    count++;
                    total += MeanGC[i];
                }
            AvgGCContent = Math.Round(total / count,2);
            fileResult = "[";
            for (int i = 0; i < count; i++)
            {
                if (i < count - 1) fileResult += MeanGC[i] + ",";
                else if (i == count - 1) fileResult += MeanGC[i] + "]";
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
