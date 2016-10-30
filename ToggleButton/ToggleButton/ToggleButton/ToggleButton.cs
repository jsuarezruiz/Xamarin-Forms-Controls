using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ToggleButton.Controls
{
    public class ToggleButton : ContentView
    {
        private ICommand _toggleCommand;
        private Image _toggleImage;

        public ToggleButton()
        {
            Initialize();
        }

        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create("Checked", typeof(bool), typeof(ToggleButton), false, BindingMode.TwoWay,
                null, propertyChanged: OnCheckedChanged);

        public static readonly BindableProperty CheckedImageProperty =
            BindableProperty.Create("CheckedImage", typeof(ImageSource), typeof(ToggleButton), null);

        public static readonly BindableProperty UnCheckedImageProperty =
            BindableProperty.Create("UnCheckedImage", typeof(ImageSource), typeof(ToggleButton), null);

        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        public ImageSource CheckedImage
        {
            get { return (ImageSource)GetValue(CheckedImageProperty); }
            set { SetValue(CheckedImageProperty, value); }
        }

        public ImageSource UnCheckedImage
        {
            get { return (ImageSource)GetValue(UnCheckedImageProperty); }
            set { SetValue(UnCheckedImageProperty, value); }
        }

        public ICommand ToogleCommand
        {
            get
            {
                return _toggleCommand
                       ?? (_toggleCommand = new Command(() =>
                       {
                           if (Checked)
                           {
                               Checked = false;
                           }
                           else
                           {
                               Checked = true;
                           }
                       }));
            }
        }

        private void Initialize()
        {
            _toggleImage = new Image();

            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ToogleCommand
            });

            _toggleImage.Source = UnCheckedImage;
            Content = _toggleImage;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            _toggleImage.Source = UnCheckedImage;
            Content = _toggleImage;
        }

        private static async void OnCheckedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var toggleButton = (ToggleButton)bindable;

            if (Equals(newValue, null) && !Equals(oldValue, null))
                return;

            if (toggleButton.Checked)
            {
                toggleButton._toggleImage.Source = toggleButton.CheckedImage;
            }
            else
            {
                toggleButton._toggleImage.Source = toggleButton.UnCheckedImage;
            }

            toggleButton.Content = toggleButton._toggleImage;
            
            await toggleButton.ScaleTo(0.9, 50, Easing.Linear);
            await Task.Delay(100);
            await toggleButton.ScaleTo(1, 50, Easing.Linear);
        }
    }
}