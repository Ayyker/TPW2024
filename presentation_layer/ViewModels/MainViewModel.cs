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
using System.Diagnostics;

using presentation_layer.Models;
using data_layer;
using logic_layer;
using System.Xml.Linq;

namespace presentation_layer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private BallCounterModel _ballCounterModel;
        private static BallRepository _ballRepository;
        private static SimulationManager _simulationManager;
        private SimulationUI _simulationUI;

        private string _amountOfBalls;
        public string AmountOfBalls
        {
            get { return _amountOfBalls; }
            set
            {
                if (_amountOfBalls != value)
                {
                    _amountOfBalls = value;
                    OnPropertyChanged(nameof(AmountOfBalls));
                }
            }
        }

        

        public MainViewModel()
        {
            AddBallCommand = new RelayCommand(AddBall);
            DeleteBallCommand = new RelayCommand(DeleteBall);
            ExitCommand = new RelayCommand(ExitApplication);

            _ballCounterModel = new BallCounterModel();

            _ballRepository = new BallRepository();
            _simulationManager = new SimulationManager(_ballRepository);
            _simulationUI = new SimulationUI(_simulationManager);
    }
        private void ExitApplication(object parameter)
        {
            Application.Current.Shutdown();
        }
      
        private void AddBall(object obj)
        {
            if (_amountOfBalls.All(char.IsDigit))
            {
                int amount = int.Parse(_amountOfBalls.ToString());
                _ballCounterModel.AddBall(amount);
                _simulationUI.InitializeSimulation(amount, 100, 100);
            }
            
            
      
            Console.Clear();
            foreach (var ball in _ballRepository.GetBalls())
            {
                Console.WriteLine(ball.ToString());
            }
            OnPropertyChanged(nameof(BallCounter));
        }

        private void DeleteBall(object obj)
        {
            _ballCounterModel.DeleteBall();
            OnPropertyChanged(nameof(BallCounter));
        }

        public ICommand AddBallCommand { get; set; }
        public ICommand DeleteBallCommand { get; set; }
        public ICommand ExitCommand { get; private set; }

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
