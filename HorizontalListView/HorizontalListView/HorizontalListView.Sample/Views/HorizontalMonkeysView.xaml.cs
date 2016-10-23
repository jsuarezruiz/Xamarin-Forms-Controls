using HorizontalListView;
using HorizontalListViewSample.ViewModels;
using Xamarin.Forms;

namespace HorizontalListViewSample.Views
{
    public partial class HorizontalMonkeysView : ContentPage
    {
        private object Parameter { get; set; }

        public HorizontalMonkeysView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = App.Locator.HorizontalMonkeysViewModel;
        }

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as HorizontalMonkeysViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as HorizontalMonkeysViewModel;
            if (viewModel != null) viewModel.OnDisappearing();
        }
    }
}
