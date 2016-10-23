using Microsoft.Practices.Unity;
using HorizontalListViewSample.Services.Navigation;

namespace HorizontalListViewSample.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            // ViewModels
            _container.RegisterType<MainViewModel>();
            _container.RegisterType<HorizontalMonkeysViewModel>();
            _container.RegisterType<VerticalMonkeysViewModel>();

            // Services     
            _container.RegisterType<INavigationService, NavigationService>();
        }

        public MainViewModel MainViewModel
        {
            get { return _container.Resolve<MainViewModel>(); }
        }

        public HorizontalMonkeysViewModel HorizontalMonkeysViewModel
        {
            get { return _container.Resolve<HorizontalMonkeysViewModel>(); }
        }

        public VerticalMonkeysViewModel VerticalMonkeysViewModel
        {
            get { return _container.Resolve<VerticalMonkeysViewModel>(); }
        }
    }
}