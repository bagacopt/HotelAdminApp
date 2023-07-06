using LiveCharts;
using System.ComponentModel;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.ViewModel
{
    public class ChartViewModel : INotifyPropertyChanged
    {
        private ChartValues<int> pendingRequests;
        public ChartValues<int> PendingRequests
        {
            get { return pendingRequests; }
            set
            {
                if (pendingRequests != value)
                {
                    pendingRequests = value;
                    OnPropertyChanged(nameof(PendingRequests));
                }
            }
        }

        private ChartValues<int> concludedRequests;
        public ChartValues<int> ConcludedRequests
        {
            get { return concludedRequests; }
            set
            {
                if (concludedRequests != value)
                {
                    concludedRequests = value;
                    OnPropertyChanged(nameof(ConcludedRequests));
                }
            }
        }

        private ChartValues<int> canceledRequests;
        public ChartValues<int> CanceledRequests
        {
            get { return canceledRequests; }
            set
            {
                if (canceledRequests != value)
                {
                    canceledRequests = value;
                    OnPropertyChanged(nameof(CanceledRequests));
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
