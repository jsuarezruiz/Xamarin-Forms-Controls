using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace WrapView.Controls
{
    public class WrapPanel : Layout<View>
    {
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            double rowHeight = 0;
            double yPos = y, xPos = x;

            foreach (var child in Children)
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

            HeightRequest = yPos;
        }  
    }

    public class WrapView : Grid
    {
        protected readonly ScrollView ScrollView;
        protected readonly WrapPanel WrapPanel;

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceProperty =
          BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(WrapView), default(IEnumerable<object>),
              BindingMode.TwoWay, propertyChanged: ItemsSourceChanged);

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty =
           BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(WrapView), default(DataTemplate));

        public WrapView()
        {
            ScrollView = new ScrollView
            {
                Orientation = ScrollOrientation.Vertical
            };

            WrapPanel = new WrapPanel();

            ScrollView.Content = WrapPanel;
            Children.Add(ScrollView);
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (WrapView)bindable;
            itemsLayout.SetItemsSource();
        }

        protected virtual void SetItemsSource()
        {
            WrapPanel.Children.Clear();

            if (ItemsSource == null)
                return;

            foreach (var item in ItemsSource)
                WrapPanel.Children.Add(GetItem(item));
        }

        protected virtual View GetItem(object item)
        {
            var content = ItemTemplate.CreateContent();
            var view = content as View;

            if (view == null)
                return null;

            view.BindingContext = item;

            return view;
        }
    }
}