using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation_layer.Models
{
    public class BallCounterModel
    {
        private int _ballCount;

        public int BallCount
        {
            get { return _ballCount; }
            set { _ballCount = value; }
        }

        public void AddBall()
        {
            if (_ballCount < 16)
            {
                _ballCount++;
            }
        }

        public void DeleteBall()
        {
            if (_ballCount > 0)
            {
                _ballCount--;
            }
        }
    }
}

