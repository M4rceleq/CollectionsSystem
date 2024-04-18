using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionsSystem.Models;
using CollectionsSystem.Views;

namespace CollectionsSystem.ViewModels
{
    internal partial class CollectionDetailsViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        public string collectionName = string.Empty;

        [ObservableProperty]
        public ObservableCollection<Item> items = [];

        public event EventHandler? ForceReRender;

        public void OnAppearing()
        {
            LoadCollection();
        }

        public void LoadCollection()
        {
            Collection collection = App.Repository.LoadCollection(CollectionName);

            List<Item> otherItems = [];
            List<Item> soldItems = [];

            foreach (Item item in collection.Items)
            {
                if (item.Status == "Sprzedane")
                {
                    soldItems.Add(item);
                }
                else
                {
                    otherItems.Add(item);
                }
            }

            Items = new ObservableCollection<Item>(otherItems.Concat(soldItems));
        }

        [RelayCommand]
        private async Task DeleteCollection()
        {
            App.Repository.DeleteCollection(CollectionName);
            await Shell.Current.Navigation.PopModalAsync();
        }

        [RelayCommand]
        private void ChangeCollectionName(string newCollectionName)
        {
            App.Repository.RenameCollection(CollectionName, newCollectionName);
            CollectionName = newCollectionName;
        }

        [RelayCommand]
        public async Task AddItem()
        {
            await Shell.Current.Navigation.PushAsync(new ItemFormView
            {
                BindingContext = new ItemFormViewModel(CollectionName)
            });
            LoadCollection();
        }

        [RelayCommand]
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
            App.Repository.SaveCollection(new Collection
            {
                Name = CollectionName,
                Items = Items.ToList(),
            });
            ForceReRender.Invoke(this, EventArgs.Empty);
        }

        [RelayCommand]
        public async Task EditItem(Item item)
        {
            await Shell.Current.Navigation.PushAsync(new ItemFormView
            {
                BindingContext = new ItemFormViewModel(CollectionName, item)
            });
            LoadCollection();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
           CollectionName = query["name"].ToString();
        }
    }
}
