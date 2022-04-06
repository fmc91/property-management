using PropertyManagementCommon;
using PropertyManagementService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagementUi.ViewModels
{
    public class EditPropertyViewModel : INotifyPropertyChanged
    {
        private string _imagePath;

        private Owner _owner;

        private Owner _newOwner;

        private ObservableCollection<Owner> _owners;

        public event PropertyChangedEventHandler PropertyChanged;

        //public EditPropertyViewModel()
        //{
        //    PurchaseCosts.CollectionChanged += (s, e) => OnPropertyChanged(nameof(PurchaseCosts));
        //}

        public int? ImageId { get; set; }

        public int PropertyId { get; set; }

        public string StreetAddress1 { get; set; }

        public string StreetAddress2 { get; set; }

        public string Locality { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public DateTime PurchaseDate { get; set; }

        public double PurchasePrice { get; set; }

        public PropertyKind PropertyKind { get; set; }

        public ObservableCollection<PurchaseCost> PurchaseCosts { get; set; }

        public string ImagePath
        {
            get { return _imagePath; }

            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    OnPropertyChanged();
                }
            }
        }

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
                    _owner = value;
                {
                    OnPropertyChanged();
                }
            }
        }

        public Owner NewOwner
        {
            get { return _newOwner; }

            set
            {
                if (_newOwner != value)
                {
                    _newOwner = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
