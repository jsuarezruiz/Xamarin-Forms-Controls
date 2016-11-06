using WrapView;
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
    }
}