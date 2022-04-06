using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PropertyManagementUi.ViewModels
{
    public class IndexViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PropertySummaryViewModel> _properties;

        public event PropertyChangedEventHandler PropertyChanged;

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
