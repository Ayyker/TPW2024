using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static presentation_layer.ViewModels.RelayCommand;

namespace presentation_layer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            BallCounter = 0;
            AddBallCommand = new RelayCommand(AddBall);
            DeleteBallCommand = new RelayCommand(DeleteBall);

        }

        private void AddBall(object obj)
        {
            if(BallCounter < 16)
            {
                BallCounter++;
            }
        } 
        private void DeleteBall(object obj)
        {
            if(BallCounter > 0)
            {
                BallCounter--;
            }
        }

        private int _ballCounter;

        public ICommand AddBallCommand { get; set; }
        public ICommand DeleteBallCommand { get; set; }

        public int BallCounter
        {
            get { return _ballCounter; }
            set 
            { _ballCounter = value;
                OnPropertyChanged();
            }
        }

        //this event is called when any property changed
        public event PropertyChangedEventHandler PropertyChanged;

        //this method handles event
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
