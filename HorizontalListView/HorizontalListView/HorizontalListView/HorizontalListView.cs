using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HorizontalListView.Controls
{
    public class HorizontalListView : Grid
    {
        protected readonly ScrollView ScrollView;
        protected readonly StackLayout StackLayout;

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly BindableProperty ItemsSourceProperty =
          BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(HorizontalListView), default(IEnumerable<object>),
              BindingMode.TwoWay, propertyChanged: ItemsSourceChanged);

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem", typeof(object), typeof(HorizontalListView), default(object),
                BindingMode.TwoWay, null);

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty =
           BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(HorizontalListView), default(DataTemplate));

        public HorizontalListView()
        {
            ScrollView = new ScrollView
            {
                Orientation = ScrollOrientation.Horizontal
            };

            StackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 6,     
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            ScrollView.Content = StackLayout;
            Children.Add(ScrollView);
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (HorizontalListView)bindable;
            itemsLayout.SetItemsSource();
        }

        protected virtual void SetItemsSource()
        {
            StackLayout.Children.Clear();

            if (ItemsSource == null)
                return;

            foreach (var item in ItemsSource)
                StackLayout.Children.Add(GetItem(item));
        }

        protected virtual View GetItem(object item)
        {
            var content = ItemTemplate.CreateContent();
            var view = content as View;

            var tapEvent = new TapGestureRecognizer();
            tapEvent.Tapped += (c, r) =>
            {
                SelectedItem = item;
            };

            if (view == null)
                return null;

            view.GestureRecognizers.Add(tapEvent);
            view.BindingContext = item;

            return view;
        }
    }
}