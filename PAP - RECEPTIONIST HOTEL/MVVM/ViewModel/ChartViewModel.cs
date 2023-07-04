using LiveCharts;
using System.ComponentModel;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.ViewModel
{
    public class ChartViewModel : INotifyPropertyChanged
    {
        private ChartValues<int> activeRequests;
        public ChartValues<int> ActiveRequests
        {
            get { return activeRequests; }
            set
            {
                if (activeRequests != value)
                {
                    activeRequests = value;
                    OnPropertyChanged(nameof(ActiveRequests));
                }
            }
        }

        private ChartValues<int> finalizedRequests;
        public ChartValues<int> FinalizedRequests
        {
            get { return finalizedRequests; }
            set
            {
                if (finalizedRequests != value)
                {
                    finalizedRequests = value;
                    OnPropertyChanged(nameof(FinalizedRequests));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
