using InfiniteScrollingBehavior.Models;
using InfiniteScrollingBehavior.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace InfiniteScrollingBehavior.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Monkey> _monkeys;

        private ICommand _refreshCommand;

        public MainViewModel()
        {
            LoadItems();
        }

        public bool IsBusy { get; set; }

        public int CurrentPage { get; set; }

        public ObservableCollection<Monkey> Monkeys
        {
            get { return _monkeys; }
            set
            {
                _monkeys = value;
                RaisePropertyChanged();
            }
        }

        public ICommand RefreshCommand
        {
            get { return _refreshCommand = _refreshCommand ?? new DelegateCommand<Monkey>(RefreshCommandExecute, RefreshCommandCanExecute); }
        }

        public bool RefreshCommandCanExecute(Monkey monkey)
        {
            return !IsBusy && 
                Monkeys.Count != 0 && 
                Monkeys.Last().MonkeyId == monkey.MonkeyId;
        }

        public void RefreshCommandExecute(Monkey monkey)
        {
            LoadItems();
        }

        private void LoadItems(int pageSize = 10)
        {
            IsBusy = true;

            if(Monkeys == null)
            {
                Monkeys = new ObservableCollection<Monkey>();
            }

            for (int i = CurrentPage; i < CurrentPage + pageSize; i++)
            {
                Monkeys.Add(new Monkey()
                {
                    MonkeyId = i + 1,
                    Name = string.Format("Monkey {0}", i + 1)
                });
            }

            CurrentPage = Monkeys.Count;
            IsBusy = false;
        }
    }
}