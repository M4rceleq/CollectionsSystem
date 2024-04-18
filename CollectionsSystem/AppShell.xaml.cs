namespace CollectionsSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.CollectionDetailsView), typeof(Views.CollectionDetailsView));
        }
    }
}
