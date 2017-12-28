using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StravaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        Client _client;

        public MainViewModel(Client client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public bool IsAuthenticated { get; protected set; }

        public IList<ActivityViewModel> Activities { get; } = new ObservableCollection<ActivityViewModel>();

        public async Task GetActivitiesAsync()
        {
            Status = "requesting Activities...";
            var activities = await _client.Activities.GetAthleteActivities();
            foreach (var activity in activities)
            {
                Activities.Add(new ActivityViewModel(activity));
            }

            if (activities.Any())
            {
                Status = $"retrieved {activities.Count} activities from Strava";
            }
            else
            {
                Status = "Could not retrieve any activities";
            }
        }

        private RelayCommand _getActivitiesCommand;
        public RelayCommand GetActivitiesCommand
        {
            get
            {
                return _getActivitiesCommand ?? (_getActivitiesCommand = new RelayCommand(async () => await GetActivitiesAsync()));
            }
        }

        private string _status = string.Empty;

        /// <summary>
        /// Sets and gets the Status property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Status
        {
            get => _status;
            set
            {
                Set(()=>Status, ref _status, value);
            }
        }
    }
}
