using PropertyManagementCommon.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PropertyManagementUi
{
    /// <summary>
    /// Interaction logic for AddPropertyPage.xaml
    /// </summary>
    public partial class AddPropertyPage : Page, INotifyPropertyChanged
    {
        private AppController _appController;

        private bool _addingOwner;

        public AddPropertyPage(AppController appController, AddPropertyViewModel viewModel)
        {
            _appController = appController;
            ViewModel = viewModel;

            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private AddPropertyViewModel ViewModel
        {
            get { return (AddPropertyViewModel)DataContext; }

            set { DataContext = value; }
        }

        public bool AddingOwner
        {
            get { return _addingOwner; }

            set
            {
                if (_addingOwner != value)
                {
                    _addingOwner = value;
                    OnPropertyChanged();
                }    
            }
        }

        public void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            _appController.AddProperty(ViewModel);
        }

        public void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Are you sure you want to cancel? All unsaved data will be lost.", "Are you sure?", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
                _appController.IndexPage();
        }

        public void AddPurchaseCostButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.PurchaseCosts.Add(new PurchaseCost());
        }

        public void RemovePurchaseCostButtonClick(object sender, RoutedEventArgs e)
        {
            var purchaseCost = (PurchaseCost)((Button)sender).Tag;
            ViewModel.PurchaseCosts.Remove(purchaseCost);
        }

        public void NewOwnerButtonClick(object sender, RoutedEventArgs e)
        {
            AddingOwner = true;
        }

        public void NewOwnerOkClick(object sender, RoutedEventArgs e)
        {
            //NEED TO CHANGE VALIDATION
            if (String.IsNullOrWhiteSpace(ViewModel.NewOwnerName)) return;

            var newOwner = new Owner { Name = ViewModel.NewOwnerName };
            _appController.AddOwner(newOwner);

            ViewModel.Owners = new ObservableCollection<Owner>(_appController.GetAllOwners());

            ViewModel.Owner = ViewModel.Owners
                .Where(o => o.OwnerId == newOwner.OwnerId)
                .FirstOrDefault();

            AddingOwner = false;

            ViewModel.NewOwnerName = null;
        }

        public void NewOwnerCancelClick(object sender, RoutedEventArgs e)
        {
            AddingOwner = false;

            ViewModel.NewOwnerName = null;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
