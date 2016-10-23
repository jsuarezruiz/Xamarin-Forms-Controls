using HorizontalListView;
using HorizontalListViewSample.ViewModels;
using Xamarin.Forms;

namespace HorizontalListViewSample.Views
{
    public partial class VerticalMonkeysView : ContentPage
    {
        private object Parameter { get; set; }

        public VerticalMonkeysView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = App.Locator.VerticalMonkeysViewModel;
        }

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as VerticalMonkeysViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as VerticalMonkeysViewModel;
            if (viewModel != null) viewModel.OnDisappearing();
        }
    }
}
