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
        public TRMesure[] ArrayOFMesureTR;
        public TemperatureFile(FileInfo mainFile)
        {
            MainFile = mainFile;
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            DepthOfImmersion = Double.Parse(MainFile.Name.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
        }
        public TemperatureFile() { }
        //public void CountAverageTemperature()
        //{
        //    string pattern = @"[0-9]+/[0-9]+/[0-9]+\s[0-9]+:[0-9]+:[0-9]+.[0-9]+\s[0-9]+.[0-9]+\b";
        //    List<string> MatchesList = new List<string>();
        //    using (StreamReader sr = new StreamReader(MainFile.FullName))
        //    {
        //        MatchesList.Clear();
        //        MatchCollection Matches = null;
        //        Regex regex = new Regex(pattern);
        //        double sum = 0;
        //        int cnt = 0;
        //        foreach (string SubString in sr.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            Matches = regex.Matches(SubString);
        //            foreach (Match match in Matches)
        //            {
        //                MatchesList.Add(match.Value);
        //                sum += double.Parse(match.Value.Split(new char[] { ' ' })[2]); // попытался максимально сократить если чё приму новые идеи(не факт что работает (* ^ ω ^) )
        //                cnt += 1;
        //            }
        //        }
        //        AverageTemperature = sum / cnt;
        //    }
        //}
        public void CountAverage()
        {
            double sum = 0;
            if(ArrayOFMesureTR!=null)
            {
                foreach(TRMesure item in ArrayOFMesureTR)
                {
                    sum += item._Temperature;
                }
                AverageTemperature = sum / ArrayOFMesureTR.Length;
            }
        }

        public override string ToString()
        {
            return String.Format(MainFile.Name);
        }
    }
}
