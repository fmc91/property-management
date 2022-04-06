using Microsoft.Win32;
using PropertyManagementService.Model;
using PropertyManagementUi.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for EditPropertyPage.xaml
    /// </summary>
    public partial class EditPropertyPage : Page, INotifyPropertyChanged
    {
        private bool _addingOwner;

        private AppController _appController;

        public EditPropertyPage(AppController appController, EditPropertyViewModel viewModel)
        {
            _appController = appController;
            ViewModel = viewModel;

            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private EditPropertyViewModel ViewModel
        {
            get { return (EditPropertyViewModel)DataContext; }

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

        private void LoadImageButtonClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files|*.png;*.jpeg;*.jpg;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
                _appController.AddImage(openFileDialog.FileName, ViewModel);
        }

        private void ClearImageLinkClick(object sender, RoutedEventArgs e)
        {
            _appController.ClearImage(ViewModel);
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(ViewModel.StreetAddress1) ||
                String.IsNullOrEmpty(ViewModel.City) ||
                String.IsNullOrEmpty(ViewModel.Country) ||
                String.IsNullOrEmpty(ViewModel.Postcode) ||
                ViewModel.Owner == null)
                return;

            _appController.EditProperty(ViewModel);
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Are you sure you want to cancel? All unsaved data will be lost.", "Are you sure?", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
                _appController.PropertyDetailsPage(ViewModel.PropertyId);
        }

        private void AddPurchaseCostButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.PurchaseCosts.Add(new PurchaseCost());
        }

        private void RemovePurchaseCostButtonClick(object sender, RoutedEventArgs e)
        {
            var purchaseCost = (PurchaseCost)((Button)sender).Tag;
            ViewModel.PurchaseCosts.Remove(purchaseCost);
        }

        private void NewOwnerButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewOwner = new Owner();
            AddingOwner = true;
        }

        private void NewOwnerOkClick(object sender, RoutedEventArgs e)
        {
            //NEED TO CHANGE VALIDATION
            if (String.IsNullOrWhiteSpace(ViewModel.NewOwner.Name)) return;

            ViewModel.Owners.Add(ViewModel.NewOwner);

            ViewModel.Owner = ViewModel.NewOwner;

            AddingOwner = false;

            ViewModel.NewOwner = null;
        }

        private void NewOwnerCancelClick(object sender, RoutedEventArgs e)
        {
            AddingOwner = false;

            ViewModel.NewOwner = null;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
