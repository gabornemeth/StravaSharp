using StravaSharp;
using System;

namespace Sample.ViewModels
{
    public class ActivityViewModel
    {
        private readonly ActivitySummary _summary;

        public ActivityViewModel(ActivitySummary summary)
        {
            _summary = summary ?? throw new ArgumentNullException(nameof(summary));
        }

        public string Name => _summary.Name;

        public string Start => _summary.StartDate.ToString("yyyy.MM.dd hh:mm");

        public string Distance
        {
            get
            {
                if (_summary.Distance > 1000)
                    return $"{_summary.Distance / 1000.0f:F2} km";
                else
                    return $"{_summary.Distance} m";
            }
        }
    }
}