using PropertyManagementCommon;
using PropertyManagementCommon.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PropertyManagementUi
{
    public class AddPropertyViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Owner> _owners;

        private Owner _owner;
        private string _newOwnerName;

        public AddPropertyViewModel()
        {
            PurchaseCosts = new ObservableCollection<PurchaseCost>();
            PurchaseCosts.CollectionChanged += (s, e) => OnPropertyChanged(nameof(PurchaseCosts));

            PurchaseDate = DateTime.Today;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string StreetAddress1 { get; set; }

        public string StreetAddress2 { get; set; }

        public string Locality { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public ObservableCollection<Owner> Owners
        {
            get { return _owners; }

            set
            {
                if (_owners != value)
                {
                    _owners = value;
                    OnPropertyChanged();
                }
            }
        }

        public Owner Owner
        {
            get { return _owner; }

            set
            {
                if (_owner != value)
                {
                    _owner = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NewOwnerName
        {
            get { return _newOwnerName; }

            set
            {
                if (_newOwnerName != value)
                {
                    _newOwnerName = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime PurchaseDate { get; set; }

        public double PurchasePrice { get; set; }

        public PropertyType PropertyType { get; set; }

        public ObservableCollection<PurchaseCost> PurchaseCosts { get; }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
