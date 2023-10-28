using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Интерфейсы_и_классы
{
    public class TemperatureFile
    {
        public FileInfo MainFile;
        public double AverageTemperature;
        public double DepthOfImmersion;
        public TemperatureFile(FileInfo mainFile)
        {
            MainFile = mainFile;
        }
        public TemperatureFile() { }
        public void CountAverageTemperature()
        {
            string pattern= @"[0-9]+/[0-9]+/[0-9]+\s[0-9]+:[0-9]+:[0-9]+.[0-9]+\s[0-9]+.[0-9]+\b";
            List<string> MatchesList = new List<string>();
            using (StreamReader sr = new StreamReader(MainFile.FullName))
            {
                MatchesList.Clear();
                MatchCollection Matches = null;
                Regex regex = new Regex(pattern);
                foreach (string SubString in sr.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    Matches = regex.Matches(SubString);
                    foreach (Match m in Matches)
                    {
                        MatchesList.Add(m.Value);
                    }
                }
            }
        }


    }
}
