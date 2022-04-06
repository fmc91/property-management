using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using PropertyManagementService.Model;

namespace PropertyManagementUi.ViewModels
{
    public class NewInsurancePolicyViewModel : INotifyPropertyChanged
    {
        private Insurer _insurer;

        private Broker _broker;

        private Insurer _newInsurer;

        private Broker _newBroker;

        private List<Insurer> _insurers;

        private List<Broker> _brokers;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int PropertyId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Insurer Insurer
        {
            get { return _insurer; }

            set
            {
                if (_insurer != value)
                {
                    _insurer = value;
                    OnPropertyChanged();
                }
            }
        }

        public Broker Broker
        {
            get { return _broker; }

            set
            {
                if (_broker != value)
                {
                    _broker = value;
                    OnPropertyChanged();
                }
            }
        }

        public Insurer NewInsurer
        {
            get { return _newInsurer; }

            set
            {
                if (_newInsurer != value)
                {
                    _newInsurer = value;
                    OnPropertyChanged();
                }
            }
        }

        public Broker NewBroker
        {
            get { return _newBroker; }

            set
            {
                if (_newBroker != value)
                {
                    _newBroker = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<Insurer> Insurers
        {
            get { return _insurers; }

            set
            {
                if (_insurers != value)
                {
                    _insurers = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<Broker> Brokers
        {
            get { return _brokers; }

            set
            {
                if (_brokers != value)
                {
                    _brokers = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
