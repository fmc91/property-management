using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using PropertyManagementCommon;
using PropertyManagementService.Model;

namespace PropertyManagementUi.ViewModels
{
    public class EditTenancyViewModel : INotifyPropertyChanged
    {
        private Agent _newAgent;

        private Agent _agent;

        public event PropertyChangedEventHandler PropertyChanged;

        public int TenancyId { get; set; }

        public int PropertyId { get; set; }
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime FirstPayment { get; set; }

        public PaymentFrequency PaymentFrequency { get; set; }

        public double Deposit { get; set; }

        public ObservableCollection<Tenant> Tenants { get; set; }

        public ObservableCollection<Rate> Rates { get; set; }

        public ObservableCollection<Agent> Agents { get; set; }

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

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
