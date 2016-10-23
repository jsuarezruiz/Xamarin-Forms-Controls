namespace HorizontalListView.Windows
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new HorizontalListView.App());
        }
    }
}
