using CollectionsSystem.ViewModels;

namespace CollectionsSystem.Views;

public partial class CollectionsView : ContentPage
{
	public CollectionsView()
	{
		InitializeComponent();
	}

    private void OnItemSelectedChanged(object sender, SelectedItemChangedEventArgs e)
    {
        CollectionsViewModel vm = (CollectionsViewModel) BindingContext;
        vm.OpenCollectionCommand.Execute(e.SelectedItem);
    }

    protected override void OnAppearing()
    {
        CollectionsViewModel vm = (CollectionsViewModel) BindingContext;
        vm.LoadCollections();
        base.OnAppearing();
    }
}