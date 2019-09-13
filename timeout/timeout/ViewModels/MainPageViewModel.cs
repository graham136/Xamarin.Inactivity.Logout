using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;
using Xamarin.Forms;

namespace timeout.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        /// <summary>
        /// Main timer to stop and start timer to logout
        /// </summary>
        /// 


        private Timer _timer;
       
        private TimeSpan _totalSeconds = new TimeSpan(0, 0, 0, 20);
        /// <summary>
        /// Total Seconds keep track of the time elapsed since last reset.
        /// </summary>
        public TimeSpan TotalSeconds
        {
            get => _totalSeconds;
            set
            {
                SetProperty(ref _totalSeconds, value);
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Status update string on the timer
        /// </summary>
        string _timerString;
        public string TimerString
        {
            get => _timerString;
            set
            {
                SetProperty(ref _timerString, value);
                RaisePropertyChanged();
                
            }
        }

        /// <summary>
        /// Command binding used to reset the timer
        /// </summary>
        public Command ResetTimer { get; set; }

        /// <summary>
        /// Constructor for the viewmodel
        /// </summary>
        public MainPageViewModel()
        {
            TimerString = "Start";
            ResetTimer = new Command(RestartTimer);
            TotalSeconds = new TimeSpan(0, 0, 20);
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Start();
            _timer.Elapsed += CheckIfLogout;
           
        }

        /// <summary>
        /// Main event handler for the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CheckIfLogout(object sender, System.Timers.ElapsedEventArgs e)
        {
            
            TotalSeconds=TotalSeconds.Subtract(new TimeSpan(0, 0, 1));
            _timer.Interval = 1000;
            TimerString = "Seconds to Logout: " + TotalSeconds.TotalSeconds.ToString();
            RaisePropertyChanged();
            


            if (TotalSeconds.TotalSeconds < 0)
            {
                TimerString = "You have been logged out";
                _timer.Stop();
                
            }
                       
        }

        /// <summary>
        /// Function to restart the timer, or reset it to 20 seconds. This is used to simulate the activity reset.
        /// Please replace this with an event on the ui, for a button click, or a custom component, onfocused event etc.
        /// </summary>
        public void RestartTimer()
        {
            TotalSeconds = new TimeSpan(0, 0, 20);
            TimerString = "Reset";
            _timer.Start();
        }        
    }
}
