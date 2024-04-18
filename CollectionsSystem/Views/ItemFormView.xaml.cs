using CollectionsSystem.ViewModels;
using CollectionsSystem.Models;

namespace CollectionsSystem.Views;

public partial class ItemFormView : ContentPage
{
	public ItemFormView()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        ItemFormViewModel viewModel = (ItemFormViewModel) BindingContext;
        if (viewModel.EditedItem != null)
        {
            name.Text = viewModel.EditedItem.Name;
            status.SelectedItem = viewModel.EditedItem.Status;
            rating.SelectedItem = viewModel.EditedItem.Rating.ToString();
        }
    }

    private void SaveItem_Clicked(object sender, EventArgs e)
    {
        string name = this.name.Text;
        string status = (string) this.status.SelectedItem;
        int rating = Int32.Parse((string) this.rating.SelectedItem);

        Item newItem = new Item()
        {
            Name = name,
            Status = status,
            Rating = rating
        };

        ItemFormViewModel viewModel = (ItemFormViewModel) BindingContext;
        viewModel.SaveItemCommand.Execute(newItem);
    }
}