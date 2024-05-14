using data_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation_layer.Models {
    public interface IBetterBall {
        public Ball Ball { get;  set; }
        private void UpdateBall() { }
        public void Stop() { }
    }
}
