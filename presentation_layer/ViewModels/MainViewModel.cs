using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using presentation_layer.Models;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace presentation_layer.ViewModels {
    public class MainViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ISimulationModel _Simulation_Model;
        private String _Amount_Of_Balls;
        private bool _Is_Start_Button_Active;
        private bool _Is_Stop_Button_Active;
        private bool _Is_Text_Field_Active;
        public ICommand Start_Simulation_Command { get; }
        public ICommand Stop_Simulation_Command { get; }

        // hard coded value of Billard Table size
        private int _Billard_Table_Width = 1000;
        private int _Billard_Table_Height = 500;
        public int Billard_Table_Width => _Billard_Table_Width;
        public int Billard_Table_Height => _Billard_Table_Height;

        public ObservableCollection<IBetterBall> BallsWithTimer => _Simulation_Model.GetBalls();

        public MainViewModel() {
            Start_Simulation_Command = new RelayCommand(Start_Simulation, () => _Is_Start_Button_Active);
            Stop_Simulation_Command = new RelayCommand(Stop_Simulation, () => Is_Stop_Button_Enable);
            _Simulation_Model = new SimulationModel(_Billard_Table_Width, _Billard_Table_Height);
            _Amount_Of_Balls = "47";

            Is_Start_Button_Enable = true;
            Is_Stop_Button_Enable = false;
            Is_Text_Field_Enable = true;

        }

        public string Amount_Of_Balls {
            get => _Amount_Of_Balls;
            set {
                _Amount_Of_Balls = value;
                Is_Start_Button_Enable = int.TryParse(value, out int number) && number > 0;
                NotifyPropertyChanged();
                NotifyPropertyChanged("CanStart");
            }
        }

        public void Start_Simulation() {
            Is_Start_Button_Enable = false;
            Is_Stop_Button_Enable = true;
            Is_Text_Field_Enable = false;
            _Simulation_Model.GenerateBalls(int.Parse(Amount_Of_Balls));
            NotifyPropertyChanged("CanStart");
            NotifyPropertyChanged("CanStop");
            NotifyPropertyChanged("CanEdit");
        }
        public void Stop_Simulation() {
            Is_Start_Button_Enable = true;
            Is_Stop_Button_Enable = false;
            Is_Text_Field_Enable = true;
            _Simulation_Model.ClearAllBalls();
            NotifyPropertyChanged("CanStart");
            NotifyPropertyChanged("CanStop");
            NotifyPropertyChanged("CanEdit");
        }

        public bool Is_Start_Button_Enable {
            get => _Is_Start_Button_Active;
            set {
                _Is_Start_Button_Active = value;
                NotifyPropertyChanged();
            }
        }

        public bool Is_Stop_Button_Enable {
            get => _Is_Stop_Button_Active;
            set {
                _Is_Stop_Button_Active = value;
                NotifyPropertyChanged();
            }
        }

        public bool Is_Text_Field_Enable {
            get => _Is_Text_Field_Active;
            set {
                _Is_Text_Field_Active = value;
                NotifyPropertyChanged();
            }
        }
    }


}
