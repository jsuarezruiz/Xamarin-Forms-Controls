using Microsoft.Practices.Unity;

namespace WrapViewSample.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            // ViewModels
            _container.RegisterType<MonkeysViewModel>();
        }

        public MonkeysViewModel MonkeysViewModel
        {
            get { return _container.Resolve<MonkeysViewModel>(); }
        }
    }
}