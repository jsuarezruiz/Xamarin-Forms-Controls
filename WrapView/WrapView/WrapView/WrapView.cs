using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace WrapView.Controls
{
    public class WrapView : Layout<View>
    {
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceProperty =
          BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(WrapView), default(IEnumerable<object>),
              BindingMode.TwoWay, propertyChanged: ItemsSourceChanged);

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem", typeof(object), typeof(WrapView), default(object),
                BindingMode.TwoWay, null);

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty =
           BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(WrapView),
               default(DataTemplate));

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (WrapView)bindable;
            itemsLayout.SetItemsSource();
        }

        protected virtual void SetItemsSource()
        {
            if (ItemsSource == null)
                return;

            foreach (var item in ItemsSource)
                Children.Add(GetItem(item));
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

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            double rowHeight = 0;
            double yPos = y, xPos = x;

            foreach (var child in Children.Where(c => c.IsVisible))
            {
                var request = child.GetSizeRequest(width, height);

                double childWidth = request.Request.Width;
                double childHeight = request.Request.Height;
                rowHeight = Math.Max(rowHeight, childHeight);

                if (xPos + childWidth > width)
                {
                    xPos = x;
                    yPos += rowHeight;
                    rowHeight = 0;
                }

                var region = new Rectangle(xPos, yPos, childWidth, childHeight);
                LayoutChildIntoBoundingRegion(child, region);
                xPos += region.Width;
            }
        }
    }
}