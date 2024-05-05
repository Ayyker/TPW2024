using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_layer
{
    public interface IBall
    {
        public int ID { get; set; }
        public double X_position { get; set; }
        public double Y_position { get; set; }
        public double Velocity_X { get; set; }
        public double Velocity_Y { get; set; }
        public double Radius { get; set; }

    }
}
