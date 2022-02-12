using PropertyManagementCommon;
using PropertyManagementCommon.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;

namespace PropertyManagementUi
{
    public class PropertyDetailsViewModel : INotifyPropertyChanged
    {
        private InsuranceViewModel _insurance;

        private NewInsuranceViewModel _newInsurance;

        private ElectricalInspectionCertificate _electricalInspectionCertificate;

        private ElectricalInspectionCertificate _newElectricalInspectionCertificate;

        private GasSafetyCertificate _gasSafetyCertificate;

        private GasSafetyCertificate _newGasSafetyCertificate;

        private EnergyPerformanceCertificate _energyPerformanceCertificate;

        private EnergyPerformanceCertificate _newEnergyPerformanceCertificate;

        private ObservableCollection<Expense> _expenses;

        private ObservableCollection<Improvement> _improvements;

        private Expense _newExpense;

        private Improvement _newImprovement;

        private ObservableCollection<Expense> _expensesForEdit;

        private ObservableCollection<Improvement> _improvementsForEdit;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ImageSource ImageSource { get; set; }

        public int PropertyId { get; set; }

        public PropertyType PropertyType { get; set; }

        public string Owner { get; set; }

        public string StreetAddress1 { get; set; }

        public string StreetAddress2 { get; set; }

        public string Locality { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public TenancyViewModel CurrentTenancy { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public double? PurchasePrice { get; set; }

        public List<PurchaseCost> PurchaseCosts { get; set; }

        public InsuranceViewModel Insurance
        {
            get { return _insurance; }

            set
            {
                if (_insurance != value)
                {
                    _insurance = value;
                    OnPropertyChanged();
                }
            }
        }

        public NewInsuranceViewModel NewInsurance
        {
            get { return _newInsurance; }

            set
            {
                if (_newInsurance != value)
                {
                    _newInsurance = value;
                    OnPropertyChanged();
                }
            }
        }

        public ElectricalInspectionCertificate ElectricalInspectionCertificate
        {
            get { return _electricalInspectionCertificate; }

            set
            {
                if (_electricalInspectionCertificate != value)
                {
                    _electricalInspectionCertificate = value;
                    OnPropertyChanged();
                }
            }
        }

        public ElectricalInspectionCertificate NewElectricalInspectionCertificate
        {
            get { return _newElectricalInspectionCertificate; }

            set
            {
                if (_newElectricalInspectionCertificate != value)
                {
                    _newElectricalInspectionCertificate = value;
                    OnPropertyChanged();
                }
            }
        }

        public GasSafetyCertificate GasSafetyCertificate
        {
            get { return _gasSafetyCertificate; }

            set
            {
                if (_gasSafetyCertificate != value)
                {
                    _gasSafetyCertificate = value;
                    OnPropertyChanged();
                }
            }
        }

        public GasSafetyCertificate NewGasSafetyCertificate
        {
            get { return _newGasSafetyCertificate; }

            set
            {
                if (_newGasSafetyCertificate != value)
                {
                    _newGasSafetyCertificate = value;
                    OnPropertyChanged();
                }
            }
        }

        public EnergyPerformanceCertificate EnergyPerformanceCertificate
        {
            get { return _energyPerformanceCertificate; }

            set
            {
                if (_energyPerformanceCertificate != value)
                {
                    _energyPerformanceCertificate = value;
                    OnPropertyChanged();
                }
            }
        }

        public EnergyPerformanceCertificate NewEnergyPerformanceCertificate
        {
            get { return _newEnergyPerformanceCertificate; }

            set
            {
                if (_newEnergyPerformanceCertificate != value)
                {
                    _newEnergyPerformanceCertificate = value;
                    OnPropertyChanged();
                }
            }
        }

        public Expense NewExpense
        {
            get { return _newExpense; }

            set
            {
                if (_newExpense != value)
                {
                    _newExpense = value;
                    OnPropertyChanged();
                }
            }
        }

        public Improvement NewImprovement
        {
            get { return _newImprovement; }

            set
            {
                if (_newImprovement != value)
                {
                    _newImprovement = value;
                    OnPropertyChanged();
                }
            }
        }

        public double TotalExpenses { get; set; }

        public double TotalImprovementCost { get; set; }

        public ObservableCollection<Expense> Expenses
        {
            get { return _expenses; }

            set
            {
                if (_expenses != value)
                {
                    _expenses = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Expense> ExpensesForEdit
        {
            get { return _expensesForEdit; }

            set
            {
                if (_expensesForEdit != value)
                {
                    _expensesForEdit = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Improvement> Improvements
        {
            get { return _improvements; }

            set
            {
                if (_improvements != value)
                {
                    _improvements = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Improvement> ImprovementsForEdit
        {
            get { return _improvementsForEdit; }

            set
            {
                if (_improvementsForEdit != value)
                {
                    _improvementsForEdit = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
