using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempLib_V2
{
    public class TDRMesure:TRMesure
    {
        public double _Pressure, _SeaPressure, _Depth;
        public TDRMesure(DateTime date, double temperature,double pressure,double seapressure,double depth):base(date,temperature)
        {
            this._Pressure = pressure;
            this._SeaPressure = seapressure;
            this._Depth = depth;
        }
        public override string ToString()
        {
            return String.Format("Timestamp: {0} Temperarure: {1} Pressure: {2} Sea Pressure:{3} Depth: {4} ", this._DateTime, this._Temperature, this._Pressure,this._SeaPressure,this._Depth);
        }
    }
}
