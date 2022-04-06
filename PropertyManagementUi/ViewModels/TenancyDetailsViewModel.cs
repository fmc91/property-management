using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using PropertyManagementCommon;
using PropertyManagementService.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PropertyManagementUi.ViewModels
{
    public class TenancyDetailsViewModel : INotifyPropertyChanged
    {
        private AccountEntry _newPayment;

        public event PropertyChangedEventHandler PropertyChanged;

        public TenancyDetailsViewModel()
        {
            AccountEntries = new ObservableCollection<AccountEntry>();

            AccountEntries.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(AccountBalance));
                OnPropertyChanged(nameof(OutstandingBalance));
            };
        }

        public int TenancyId { get; set; }

        public int PropertyId { get; set; }

        public string StreetAddress1 { get; set; }

        public string StreetAddress2 { get; set; }

        public string Locality { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public List<Tenant> Tenants { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public PaymentFrequency PaymentFrequency { get; set; }

        public DateTime FirstPayment { get; set; }

        public DateTime? NextPaymentDate { get; set; }

        public Agent Agent { get; set; }

        public List<Rate> Rates { get; set; }

        public ObservableCollection<AccountEntry> AccountEntries { get; }

        public double Deposit { get; set; }

        public double AccountBalance => AccountEntries.Sum(e => e.Amount);

        public double OutstandingBalance => AccountBalance > 0 ? 0 : AccountBalance * -1;

        public AccountEntry NewPayment
        {
            get { return _newPayment; }

            set
            {
                if (_newPayment != value)
                {
                    _newPayment = value;
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
