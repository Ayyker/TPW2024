using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation_layer.Models
{
    
    public class poolBall
    {
        public poolBall()
        {
                _ballCounter = 0;
        }
        private int _ballCounter;

        public void AddBall()
        {
            if (_ballCounter < 16) 
            {
                _ballCounter++;
            }
            
        }

        public int getBallCounter() { return _ballCounter;}

        public void deleteBall()
        {
            if (_ballCounter > 0)
            {
                _ballCounter--;
            }
        }


    }
}
