using System.Windows.Input;
using Xamarin.Forms;

namespace InfiniteScrollingBehavior.Behaviors
{
    public class IniniteListScrollingBehavior : Behavior<ListView>
    {
        public static readonly BindableProperty CommandProperty =
        BindableProperty.Create("Command", typeof(ICommand), typeof(IniniteListScrollingBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public ListView AssociatedObject { get; private set; }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);

            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.ItemAppearing += OnItemAppearing;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.ItemAppearing -= OnItemAppearing;
            AssociatedObject = null;
        }

        private void OnBindingContextChanged(object sender, System.EventArgs e)
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

        private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var listview = ((ListView)sender);

            if (listview.IsRefreshing)
                return;

            if (Command == null)
            {
                return;
            }

            if (Command.CanExecute(e.Item))
            {
                Command.Execute(e.Item);
            }
        }
    }
}
