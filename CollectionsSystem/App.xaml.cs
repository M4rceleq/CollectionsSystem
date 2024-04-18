using CollectionsSystem.Data;

namespace CollectionsSystem
{
    public partial class App : Application
    {
        public static CollectionsRepository Repository { get; private set; }
        public App(CollectionsRepository repository)
        {
            InitializeComponent();

            MainPage = new AppShell();
            Repository = repository;
        }
    }
}
