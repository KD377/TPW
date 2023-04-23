using Logic;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public abstract class ViewModelAPI
    {
        public abstract ObservableCollection<Ball> Balls { get; set; }
        public abstract ICommand StartCommand { get; }
        public abstract ICommand StopCommand { get; }
        public abstract ICommand CreateBallCommand { get; }

        public abstract void Start();
        public abstract void Stop();
        public abstract void CreateBall();
        public abstract ObservableCollection<Ball> GetBalls();

        public static ViewModelAPI CreateViewModelAPI(int boardWidht,int boardHeight)
        {
            return new ViewModel(ModelAPI.CreateModelAPI(boardWidht, boardHeight));
        }


    }
    internal class ViewModel :ViewModelAPI,INotifyPropertyChanged
    {
        private readonly ModelAPI _model;
        private ObservableCollection<Ball> balls;
        public override ICommand StartCommand { get; }
        public override ICommand StopCommand { get; }
        public override ICommand CreateBallCommand { get; }

        public ViewModel(ModelAPI model)
        {
            _model = model;
            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);
            CreateBallCommand = new RelayCommand(CreateBall);
            Balls = GetBalls();
        } 

        public override ObservableCollection<Ball> Balls
        {
            get => balls;
            set
            {
                balls = value;
                OnPropertyChanged();
            }
        }

        public override void Start()
        {
            _model.Start();
        }

        public override void Stop()
        {
            _model.Stop();
        }

        public override void CreateBall()
        {
            _model.CreateBall();
            Balls = GetBalls();
        }

        public override ObservableCollection<Ball> GetBalls()
        {
            return _model.GetBalls();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}