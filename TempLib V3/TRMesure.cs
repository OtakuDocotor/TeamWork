using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempLib_V3
{
    public class TRMesure : Mesure
    {
        public TRMesure(DateTime date, double temperature) : base(date, temperature)
        {
        }
        public override string ToString()
        {
            return String.Format("Timestamp: {0} Temperarure: {1}", this._DateTime, this._Temperature);
        }
    }
}
