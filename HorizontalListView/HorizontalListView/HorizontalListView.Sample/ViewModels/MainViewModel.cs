using HorizontalListViewSample.Services.Navigation;
using HorizontalListViewSample.ViewModels.Base;
using System.Windows.Input;

namespace HorizontalListViewSample.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ICommand _verticalCommand;
        private ICommand _horizontalCommand;

        private INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand VerticalCommand
        {
            get { return _verticalCommand = _verticalCommand ?? new DelegateCommand(VerticalCommandExecute); }
        }

        public ICommand HorizontalCommand
        {
            get { return _horizontalCommand = _horizontalCommand ?? new DelegateCommand(HorizontalCommandExecute); }
        }

        private void VerticalCommandExecute()
        {
            _navigationService.NavigateTo<VerticalMonkeysViewModel>();
        }

        private void HorizontalCommandExecute()
        {
            _navigationService.NavigateTo<HorizontalMonkeysViewModel>();
        }
    }
}
