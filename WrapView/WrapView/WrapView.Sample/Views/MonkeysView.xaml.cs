using WrapView;
using WrapViewSample.ViewModels;
using Xamarin.Forms;

namespace WrapViewSample.Views
{
    public partial class MonkeysView : ContentPage
    {
        private object Parameter { get; set; }

        public MonkeysView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = App.Locator.MonkeysViewModel;
        }

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as MonkeysViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as MonkeysViewModel;
            if (viewModel != null) viewModel.OnDisappearing();
        }
    }
}