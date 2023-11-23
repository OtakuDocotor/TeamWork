using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempLib_V2
{
    public class TRMesure
    {
        public DateTime _DateTime;
        public double _Temperature;
        public TRMesure (DateTime date, double temperature)
        {
            this._DateTime = date;
            this._Temperature = temperature;
        }
        public override string ToString()
        {
            return String.Format("Timestamp: {0} Temperarure: {1}", this._DateTime, this._Temperature);
        }
    }
}
