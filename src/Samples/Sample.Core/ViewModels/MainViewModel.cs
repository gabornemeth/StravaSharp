using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StravaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly Client _client;

        public MainViewModel(Client client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        private bool _isAuthenticated;

        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            protected set
            {
                if (SetProperty(ref _isAuthenticated, value))
                {
                    GetActivitiesCommand.NotifyCanExecuteChanged();
                }
            }
        }

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
                Status = $"retrieved {activities.Count()} activities from Strava";
            }
            else
            {
                Status = "Could not retrieve any activities";
            }
        }

        private IRelayCommand _getActivitiesCommand;

        public IRelayCommand GetActivitiesCommand
            => _getActivitiesCommand ??= new AsyncRelayCommand(async () => await GetActivitiesAsync(), () => IsAuthenticated);

        private string _status = string.Empty;

        /// <summary>
        /// Sets and gets the Status property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }
    }
}
