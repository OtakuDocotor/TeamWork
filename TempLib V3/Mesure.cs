using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempLib_V3
{
    public abstract class Mesure
    {
        public DateTime _DateTime;
        public double _Temperature;
        public Mesure(DateTime date, double temperature)
        {
            this._DateTime = date;
            this._Temperature = temperature;
        }
    }
}
