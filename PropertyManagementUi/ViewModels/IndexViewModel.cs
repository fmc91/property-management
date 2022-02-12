using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PropertyManagementUi
{
    public class IndexViewModel : INotifyPropertyChanged
    {
        private bool _showOccupied;

        private bool _showUnoccupied;

        private ObservableCollection<PropertySummaryViewModel> _properties;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool ShowOccupied
        {
            get { return _showOccupied; }

            set
            {
                if (_showOccupied != value)
                {
                    _showOccupied = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ShowUnoccupied
        {
            get { return _showUnoccupied; }

            set
            {
                if (_showUnoccupied != value)
                {
                    _showUnoccupied = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<PropertySummaryViewModel> Properties
        {
            get { return _properties; }

            set
            {
                if (_properties != value)
                {
                    _properties = value;
                    OnPropertyChanged();
                }
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
