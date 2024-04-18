using CollectionsSystem.ViewModels;
using CollectionsSystem.Models;
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace CollectionsSystem.Views;

public partial class CollectionDetailsView : ContentPage
{
	public CollectionDetailsView()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        CollectionDetailsViewModel viewModel = (CollectionDetailsViewModel) BindingContext;
        viewModel.OnAppearing();
        collection.Content = Grid(viewModel.Items.ToList());
        viewModel.ForceReRender += OnForceReRender;
    }

    private void OnForceReRender(object sender, EventArgs e)
    {
        CollectionDetailsViewModel viewModel = (CollectionDetailsViewModel)BindingContext;
        collection.Content = Grid(viewModel.Items.ToList());
    }

    private async void ChangeCollectionName_Clicked(object sender, EventArgs e)
    {
        string newCollectionName = await DisplayPromptAsync("Zmiana nazwy", "Nowa nazwa kolekcji:");
        if (string.IsNullOrEmpty(newCollectionName))
        {
            await DisplayAlert("Błąd", "Nie wprowadzono nowej nazwy. Spróbuj ponownie.", "OK");
            return;
        }

        if (App.Repository.CheckCollectionDuplicates(newCollectionName))
        {
            await DisplayAlert("Błąd", "Taka nazwa już istnieje.", "OK");
            return;
        }

        CollectionDetailsViewModel viewModel = (CollectionDetailsViewModel)BindingContext;
        viewModel.ChangeCollectionNameCommand.Execute(newCollectionName);
    }

    private Border Border()
    {
        Border border = new Border();
        border.Style = (Style)Resources["border"];
        return border;
    }

    private View Header(string text)
    {
        Label label = new Label();
        label.Text = text;
        label.Style = (Style) Resources["header"];

        Border border = Border();
        border.Content = label;

        return border;
    }

    private View Cell(string text)
    {
        Label label = new Label();
        label.Text = text;
        label.Style = (Style) Resources["cell"];

        Border border = Border();
        border.Content = label;

        return border;
    }


    private Grid Grid(List<Item> items)
    {
        Grid grid = new Grid();
        grid.ColumnDefinitions = new ColumnDefinitionCollection(Enumerable.Repeat(new ColumnDefinition(GridLength.Auto), 3).ToArray());
        grid.RowDefinitions = new RowDefinitionCollection(Enumerable.Repeat(new RowDefinition(GridLength.Auto), items.Count() + 1).ToArray());

        View headerName = Header("Nazwa");
        grid.Add(headerName, 0, 0);

        View headerStatus = Header("Status");
        grid.Add(headerStatus, 1, 0);

        View headerRating = Header("Ocena");
        grid.Add(headerRating, 2, 0);


        for (int i = 0; i < items.Count(); ++i)
        {
            Item item = items[i];
            bool isSold = (item.Status == "Sprzedane");

            View cellName = Cell(item.Name);
            if (isSold) cellName.Opacity = 0.3;
            grid.Add(cellName, 0, i + 1);

            View cellStatus = Cell(item.Status);
            if (isSold) cellStatus.Opacity = 0.3;
            grid.Add(cellStatus, 1, i + 1);

            View cellRating = Cell(item.Rating.ToString());
            if (isSold) cellRating.Opacity = 0.3;
            grid.Add(cellRating, 2, i + 1);

            grid.Add(Buttons(item), 3, i + 1);
        }

        return grid;
    }

    private View Buttons(Item item)
    {
        StackLayout stack = new StackLayout();
        stack.Orientation = StackOrientation.Horizontal;
        stack.VerticalOptions = LayoutOptions.Center;
        stack.Padding = 12;
        stack.Spacing = 12;
        stack.Margin = 12;

        Button editButton = new Button();
        editButton.SetBinding(Button.CommandProperty, "EditItemCommand");
        editButton.SetValue(Button.CommandParameterProperty, item);
        editButton.Text = "🛠";
        editButton.TextColor = new Color(0xff, 0xff, 0xff);

        Button deleteButton = new Button();
        deleteButton.SetBinding(Button.CommandProperty, "DeleteItemCommand");
        deleteButton.SetValue(Button.CommandParameterProperty, item);
        deleteButton.Text = "🗑️";
        deleteButton.TextColor = new Color(0xff, 0xff, 0xff);

        stack.Add(editButton);
        stack.Add(deleteButton);

        Border border = Border();
        border.Content = stack;

        return border;
    }
}