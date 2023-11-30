using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace TempLib_V3
{
    public abstract class MesuresFile
    {
        public System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        public FileInfo MainFile;
        public double AverageTemperature;
        public double DepthOfImmersion;
        public List<TRMesure> ArrayOFMesureTR = new List<TRMesure>();
        public string[] HatOfFile = new string[6];
        public List<string> StrMesures = new List<string>();

        public MesuresFile(FileInfo mainFile)
        {
            this.MainFile = mainFile;
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            DepthOfImmersion = Double.Parse(MainFile.Name.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
        }
        public MesuresFile() { }
        public abstract void CountAverage();
        public abstract void Cutting();


        public override string ToString()
        {
            return String.Format(MainFile.Name);
        }
    }
}
