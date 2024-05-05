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
using System.Windows.Threading;

namespace presentation_layer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
       public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private SimulationModel _simulationModel;

        private String _amountOfBalls;
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        private bool _isStartEnable;
        private bool _isStopEnable;
        private bool _isTextFieldEnable;

        private DispatcherTimer _timer;
        // hard value of size
        private int _width = 1000;
        private int _height = 500;

        public MainViewModel()
        {
            StartCommand = new RelayCommand(Start, ()=>_isStartEnable);
            StopCommand = new RelayCommand(Stop, () => IsStopEnable);
            _simulationModel = new SimulationModel(_width, _height);
            _amountOfBalls = "47";

            IsStartEnable = true;
            IsStopEnable = false;
            IsTextFieldEnable = true;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(10);
            _timer.Tick += Timer_Tick;
        }

        public void Start()
        {
            IsStartEnable = false;
            IsStopEnable = true;
            IsTextFieldEnable = false;
            _simulationModel.GenerateBalls(int.Parse(AmountOfBalls));
            _timer.Start();
        }
        public void Stop()
        {
            IsStartEnable = true;
            IsStopEnable = false;
            IsTextFieldEnable = true;
            _timer.Stop();
            _simulationModel.ClearAllBalls();
            OnPropertyChanged("Balls");
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            _simulationModel.UpdateBalls();
            OnPropertyChanged("Balls");
        }
        public int Width
        {
            get => _width;
        }
        public int Height
        {
            get => _height;
        }


        public IBall[]? Balls
        {
            get => _simulationModel.GetBalls().ToArray();
        }
        public string AmountOfBalls
        {
            get => _amountOfBalls;
            set
            {
                _amountOfBalls = value;
                if (int.TryParse(value, out int number) && number > 0 && number < 100)
                {
                    IsStartEnable = true;
                }
                else
                {
                    IsStartEnable = false;
                }
                OnPropertyChanged();
            }
        }

        public bool IsStartEnable
        {
            get => _isStartEnable;
            set
            {
                _isStartEnable = value;
                OnPropertyChanged();
            }
        }

        public bool IsStopEnable
        {
            get => _isStopEnable;
            set
            {
                _isStopEnable = value;
                OnPropertyChanged();
            }
        }

        public bool IsTextFieldEnable
        {
            get => _isTextFieldEnable;
            set
            {
                _isTextFieldEnable = value;
                OnPropertyChanged();
            }
        }
    }

    
}
