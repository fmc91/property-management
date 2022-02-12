using PropertyManagementCommon.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for IndexPage.xaml
    /// </summary>
    public partial class IndexPage : Page
    {
        private AppController _appController;

        public IndexPage(AppController navigationController, IndexViewModel viewModel)
        {
            _appController = navigationController;
            ViewModel = viewModel;

            InitializeComponent();
        }

        private IndexViewModel ViewModel
        {
            get { return (IndexViewModel)DataContext; }

            set { DataContext = value; }
        }

        //private void PageLoaded(object sender, RoutedEventArgs e)
        //{
        //    ViewModel.Properties = new ObservableCollection<PropertySummaryViewModel>
        //    {
        //        new PropertySummaryViewModel
        //        {
        //            Address = "34 Didbury Road\nSoutham\nLondon\nUnited Kingdom\nE8 4SU",
        //            Tenants = new List<string> { "Henry Williamson", "Ava Smith" },
        //            OutstandingBalance = 1452635,
        //            NextPaymentDue = new DateTime(2021, 9, 18),
        //            InsuranceEndDate = new DateTime(2022, 5, 14),
        //            ElectricalInspectionExpiry = new DateTime(2022, 7, 8),
        //            GasSafetyExpiry = new DateTime(2022, 3, 21),
        //            EpcExpiry = new DateTime(2021, 12, 15)
        //        },
        //        new PropertySummaryViewModel
        //        {
        //            Address = "67 Pineapple Crescent\nWrexham\nLondon\nUnited Kingdom\nNW5 6ML",
        //            Tenants = new List<string> { "Henry Williamson", "Ava Smith" },
        //            OutstandingBalance = 0,
        //            NextPaymentDue = new DateTime(2021, 9, 12),
        //            InsuranceEndDate = new DateTime(2021, 9, 14),
        //            ElectricalInspectionExpiry = new DateTime(2022, 7, 8),
        //            GasSafetyExpiry = new DateTime(2022, 3, 21),
        //            EpcExpiry = new DateTime(2021, 12, 15)
        //        },
        //        new PropertySummaryViewModel
        //        {
        //            Address = "152a Maxwll Road\nFareham\nLondon\nUnited Kingdom\nW10 7RD",
        //            InsuranceEndDate = new DateTime(2022, 5, 14),
        //            ElectricalInspectionExpiry = new DateTime(2022, 7, 8),
        //            GasSafetyExpiry = new DateTime(2022, 3, 21),
        //            EpcExpiry = new DateTime(2021, 12, 15)
        //        },
        //        new PropertySummaryViewModel
        //        {
        //            Address = "34 Didbury Road\nSoutham\nLondon\nUnited Kingdom\nE8 4SU",
        //            Tenants = new List<string> { "Henry Williamson", "Ava Smith" },
        //            OutstandingBalance = 320,
        //            NextPaymentDue = new DateTime(2021, 9, 18),
        //            ElectricalInspectionExpiry = new DateTime(2021, 10, 13),
        //            GasSafetyExpiry = new DateTime(2022, 3, 21),
        //            EpcExpiry = new DateTime(2021, 12, 15)
        //        },
        //        new PropertySummaryViewModel
        //        {
        //            Address = "34 Didbury Road\nSoutham\nLondon\nUnited Kingdom\nE8 4SU",
        //            Tenants = new List<string> { "Henry Williamson", "Ava Smith" },
        //            OutstandingBalance = 84.5,
        //            NextPaymentDue = new DateTime(2021, 9, 18),
        //            InsuranceEndDate = new DateTime(2022, 5, 14),
        //            ElectricalInspectionExpiry = new DateTime(2022, 7, 8),
        //            EpcExpiry = new DateTime(2021, 12, 15)
        //        }
        //    };
        //}

        public void PropertyPanelClick(object sender, RoutedEventArgs e)
        {
            var propertySummary = (PropertySummaryViewModel)((Control)sender).Tag;

            _appController.PropertyDetailsPage(propertySummary.PropertyId);
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            _appController.AddPropertyPage();
        }

        private void RefreshButtonClick(object sender, RoutedEventArgs e)
        {
            _appController.IndexPage();
        }
    }
}
