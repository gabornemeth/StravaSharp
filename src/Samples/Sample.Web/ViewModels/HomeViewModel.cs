using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sample.ViewModels;

namespace Sample.Web.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel(bool isAuthenticated)
        {
            IsAuthenticated = isAuthenticated;
        }

        public bool IsAuthenticated { get; private set; }

        public IList<ActivityViewModel> Activities { get; } = new ObservableCollection<ActivityViewModel>();
    }
}