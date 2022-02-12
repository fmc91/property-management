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
    public class AddTenancyViewModel : INotifyPropertyChanged
    {
        private Agent _newAgent;

        private Agent _agent;

        public event PropertyChangedEventHandler PropertyChanged;

        public AddTenancyViewModel()
        {
            Tenants = new ObservableCollection<Tenant>();
            Rates = new ObservableCollection<Rate>();

            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            FirstPayment = DateTime.Today;
        }

        public int PropertyId { get; set; }

        public Agent NewAgent
        {
            get { return _newAgent; }

            set
            {
                if (_newAgent != value)
                {
                    _newAgent = value;
                    OnPropertyChanged();
                }
            }
        }

        public Agent Agent
        {
            get { return _agent; }

            set
            {
                if (_agent != value)
                {
                    _agent = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Tenant> Tenants { get; }

        public ObservableCollection<Rate> Rates { get; }

        public ObservableCollection<Agent> Agents { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime FirstPayment { get; set; }

        public PaymentFrequency PaymentFrequency { get; set; }

        public double DepositAmount { get; set; }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
