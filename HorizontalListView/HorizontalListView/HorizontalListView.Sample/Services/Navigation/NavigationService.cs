using HorizontalListViewSample.ViewModels;
using HorizontalListViewSample.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HorizontalListViewSample.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private IDictionary<Type, Type> viewModelRouting = new Dictionary<Type, Type>()
        {
            { typeof(MainViewModel),  typeof(MainView) },
            { typeof(HorizontalMonkeysViewModel), typeof(HorizontalMonkeysView) },
            { typeof(VerticalMonkeysViewModel), typeof(VerticalMonkeysView) }
        };

        public void NavigateTo<TDestinationViewModel>(object navigationContext = null)
        {
            Type pageType = viewModelRouting[typeof(TDestinationViewModel)];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;

            if (page != null)
                Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void NavigateTo(Type destinationType, object navigationContext = null)
        {
            Type pageType = viewModelRouting[destinationType];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;

            if (page != null)
                Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void NavigateBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}