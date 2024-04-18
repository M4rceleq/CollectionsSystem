using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionsSystem.Models;

namespace CollectionsSystem.ViewModels
{
    internal partial class ItemFormViewModel : ObservableObject
    {
        public string CollectionName;

        public Item? EditedItem;

        public List<string> StatusOptions => Item.GetStatusOptions();

        public ItemFormViewModel(string collectionName, Item? item = null)
        {
            CollectionName = collectionName;
            EditedItem = item;
        }

        [RelayCommand]
        public async Task SaveItem(Item newItem)
        {
            if (EditedItem == null && App.Repository.CheckItemDuplicates(CollectionName, newItem.Name))
            {
                bool addSameItem = await Shell.Current.DisplayAlert("Błąd", "Czy na pewno chcesz dodać drugi taki sam przedmiot?", "TAK", "NIE");
                if (addSameItem == false) return;
            }

            if (EditedItem == null)
            {
                App.Repository.AddItem(CollectionName, newItem);
            }
            else
            {
                App.Repository.EditItem(CollectionName, EditedItem, newItem);
            }

            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}
