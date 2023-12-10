using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace TempLib_V2
{
    public class TemperatureFile
    {
        public System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();  
        public FileInfo MainFile;
        public double AverageTemperature;
        public double DepthOfImmersion;
        public List<TRMesure> ArrayOFMesureTR = new List<TRMesure>();
        public string[] HatOfFile = new string[6];
        public List<string> StrMesures = new List<string>();

        public TemperatureFile(FileInfo mainFile)
        {
            this.MainFile = mainFile;
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            DepthOfImmersion = Double.Parse(MainFile.Name.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
        }
        public TemperatureFile() { }
        public void CountAverage()
        {
            double sum = 0;
            if (ArrayOFMesureTR != null)
            {
                foreach (TRMesure item in ArrayOFMesureTR)
                {
                    sum += item._Temperature;
                }
                AverageTemperature = sum / ArrayOFMesureTR.Count;
            }
        }
        public void Cutting_TR_Files()
        {
            List<TRMesure> Cutted_Mesures = new List<TRMesure>();
            List<TRMesure> All_Mesures = new List<TRMesure>();
            using (StreamReader sr =new StreamReader(MainFile.FullName))
            {
                customCulture.NumberFormat.NumberDecimalSeparator = ",";
                System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

                double Sum = 0;
                string[] S = sr.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                for(int i=0;i<6;i++)
                {
                    HatOfFile[i] = S[i];
                }
                MatchCollection Matches = null;
                Regex r = new Regex(@"[0-9]+/[0-9]+/[0-9]+\s[0-9]+:[0-9]+:[0-9]+.[0-9]+[;]+[0-9]+.[0-9]+\b");

                foreach (string q in S)
                {
                    Matches = r.Matches(q);
                    foreach (Match m in Matches)
                    {
                        string[] SubS = m.Value.Split(new char[] { ' ', ';', '/',':','.' }, StringSplitOptions.RemoveEmptyEntries);
                        All_Mesures.Add(new TRMesure(new DateTime(int.Parse(SubS[2]), int.Parse(SubS[1]), int.Parse(SubS[0]), int.Parse(SubS[3]), int.Parse(SubS[4]), int.Parse(SubS[5])),double.Parse(SubS[7])));
                        Sum += All_Mesures.Last()._Temperature;
                    }
                }
                double average= Sum / All_Mesures.Count;
                for(int i=0;i<All_Mesures.Count;i++)
                {
                    if (All_Mesures[i]._Temperature < average)
                    {
                        Cutted_Mesures.Add(All_Mesures[i]);
                        StrMesures.Add(S[i+7]);
                    }
                }
                Cutted_Mesures.RemoveRange(0, 500);
                StrMesures.RemoveRange(0, 500);
                Cutted_Mesures.RemoveRange(Cutted_Mesures.Count() - 1200, 1200);
                StrMesures.RemoveRange(StrMesures.Count() - 1200, 1200);
            }
            ArrayOFMesureTR = Cutted_Mesures;
        }


        public override string ToString()
        {
            return String.Format(MainFile.Name);
        }
    }
}
