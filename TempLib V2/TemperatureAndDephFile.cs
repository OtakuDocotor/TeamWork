using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TempLib_V2
{
    public class TemperatureAndDephFile : TemperatureFile
    {
        public double AverageDepth, AveragePressure, AverageSeaPressure;
        public List<TDRMesure> ArrayOFMesureTDR = new List<TDRMesure>();

        public TemperatureAndDephFile(FileInfo filename) : base(filename) { }

        public override string ToString()
        {
            return String.Format(MainFile.Name);
        }
        public void CountAverageTDR()
        {
            double sumT = 0, sumPS = 0, sumP = 0, sumD = 0;
            if (ArrayOFMesureTR != null)
            {
                foreach (TDRMesure item in ArrayOFMesureTR)
                {
                    sumT += item._Temperature;
                    sumP += item._Pressure;
                    sumPS += item._SeaPressure;
                    sumD += item._Pressure;
                }
                int n = ArrayOFMesureTR.Count;
                AverageTemperature = sumT / n;
                AveragePressure = sumP / n;
                AverageSeaPressure = sumPS / n;
                AverageDepth = sumD / n;
               
            }
        }
        public void Cutting_TDR_Files()
        {
            List<TDRMesure> Cutted_Mesures = new List<TDRMesure>();
            List<TDRMesure> All_Mesures = new List<TDRMesure>();
            using (StreamReader sr = new StreamReader(MainFile.FullName))
            {
                customCulture.NumberFormat.NumberDecimalSeparator = ",";
                System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

                double Sum = 0;
                string[] S = sr.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < 6; i++)
                {
                    HatOfFile[i] = S[i];
                }
                Match Match = null;
                Regex Nr = new Regex(@"[0-9]+/[0-9]+/[0-9]+\s[0-9]+:[0-9]+:[0-9]+.[0-9]+[;]+[0-9]+.[0-9]+");
                foreach (string q in S)
                {
                    Match = Nr.Match(q);
                    if (Match.Value != "")
                    {
                        string[] SubS = q.Split(new char[] { ' ', ';', '/', ':', '.' }, StringSplitOptions.RemoveEmptyEntries);
                        All_Mesures.Add(new TDRMesure(new DateTime(int.Parse(SubS[2]), int.Parse(SubS[1]), int.Parse(SubS[0]), int.Parse(SubS[3]), int.Parse(SubS[4]), int.Parse(SubS[5])), double.Parse(SubS[7]), double.Parse(SubS[8]), double.Parse(SubS[9]), double.Parse(SubS[10])));
                        Sum += All_Mesures.Last()._Temperature;
                    }
                }

                double average = Sum / All_Mesures.Count;
                for (int i = 0; i < All_Mesures.Count; i++)
                {
                    if (All_Mesures[i]._Temperature < average)
                    {
                        Cutted_Mesures.Add(All_Mesures[i]);
                        StrMesures.Add(S[i + 7]);
                    }
                }
                Cutted_Mesures.RemoveRange(0, 500);
                StrMesures.RemoveRange(0, 500);
                Cutted_Mesures.RemoveRange(Cutted_Mesures.Count() - 1200, 1200);
                StrMesures.RemoveRange(StrMesures.Count() - 1200, 1200);
            }
            ArrayOFMesureTDR = Cutted_Mesures;
        }
    }
}
