using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempLib_V2
{
    public class WaterLayer
    {
        public const int Heat_capacity = 4200, Water_Density = 1000;
        public double Thickness, Average_Temp_In_Time;
        public double Heat_Content_Of_The_Water_Column = 0;
        public TemperatureFile file1, file2;

        public WaterLayer(TemperatureFile _file1, TemperatureFile _file2) 
        {
            this.Thickness = Math.Max(_file1.DepthOfImmersion, _file2.DepthOfImmersion) - Math.Min(_file1.DepthOfImmersion, _file2.DepthOfImmersion);
            this.file1 = _file1;
            this.file2 = _file2;
        }

        public void Calculation()
        {
            for(int i = 0; i < Math.Min(file1.ArrayOFMesure.Count, file2.ArrayOFMesure.Count); i++)
            {
                Average_Temp_In_Time = (file1.ArrayOFMesure[i]._Temperature + file2.ArrayOFMesure[i]._Temperature) / 2;
                Heat_Content_Of_The_Water_Column += Average_Temp_In_Time * Thickness * Heat_capacity * Water_Density;
            }
        }

    }
}
