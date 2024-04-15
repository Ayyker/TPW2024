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
using presentation_layer.Models;
using logic_layer;
using System.Collections.ObjectModel;

namespace presentation_layer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly BallCounterModel _ballCounterModel = new BallCounterModel();
        public ICommand ExitCommand { get; private set; }

        public MainViewModel()
        {
            AddBallCommand = new RelayCommand(AddBall);
            DeleteBallCommand = new RelayCommand(DeleteBall);
            ExitCommand = new RelayCommand(ExitApplication);
        }
        private void ExitApplication(object parameter)
        {
            Application.Current.Shutdown();
        }

        private void AddBall()
        {
            _ballCounterModel.AddBall();
            OnPropertyChanged(nameof(BallCounter));
        }

        private void DeleteBall(object obj)
        {
            _ballCounterModel.DeleteBall();
            OnPropertyChanged(nameof(BallCounter));
        }

        public ICommand AddBallCommand { get; set; }
        public ICommand DeleteBallCommand { get; set; }

        public int BallCounter
        {
            get { return _ballCounterModel.BallCount; }
            // OnPropertyChanged wywołuje się teraz w AddBall i DeleteBall
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
