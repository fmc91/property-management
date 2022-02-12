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
    public class EditTenancyViewModel : INotifyPropertyChanged
    {
        private Agent _newAgent;

        //private Agent _agent;
        private int _agentId;

        public event PropertyChangedEventHandler PropertyChanged;

        public EditTenancyViewModel()
        {
            AddedTenants = new List<Tenant>();
            DeletedTenants = new List<Tenant>();
            ModifiedTenants = new List<Tenant>();

            AddedRates = new List<Rate>();
            DeletedRates = new List<Rate>();
            ModifiedRates = new List<Rate>();
        }

        public int TenancyId { get; set; }

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

        public int AgentId
        {
            get { return _agentId; }

            set
            {
                if (_agentId != value)
                {
                    _agentId = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Tenant> Tenants { get; set; }

        public ObservableCollection<Rate> Rates { get; set; }

        public ObservableCollection<Agent> Agents { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime FirstPayment { get; set; }

        public PaymentFrequency PaymentFrequency { get; set; }

        public double DepositAmount { get; set; }

        public List<Tenant> AddedTenants { get; set; }

        public List<Tenant> DeletedTenants { get; set; }

        public List<Tenant> ModifiedTenants { get; set; }

        public List<Rate> AddedRates { get; set; }

        public List<Rate> DeletedRates { get; set; }

        public List<Rate> ModifiedRates { get; set; }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
