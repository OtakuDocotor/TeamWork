using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempLib_V2
{
    public class TemperatureAndDephFile : TemperatureFile
    {
        public double AverageDepth, AveragePressure, AverageSeaPressure;
        public TDRMesure[] ArrayOFMesureTDR;

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
                int n = ArrayOFMesureTR.Length;
                AverageTemperature = sumT / n;
                AveragePressure = sumP / n;
                AverageSeaPressure = sumPS / n;
                AverageDepth = sumD / n;
            }
        }
    }
}
