using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionsSystem.Models;
using CollectionsSystem.Views;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace CollectionsSystem.ViewModels
{
    public partial class CollectionsViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<string> collectionsNames = [];

        [ObservableProperty]
        public string newCollectionName = string.Empty;

        public void LoadCollections()
        {
            CollectionsNames = new ObservableCollection<string>(App.Repository.GetCollectionsNames());
        }

        [RelayCommand]
        private async Task OpenCollection(string selectedCollection)
        {
            await Shell.Current.GoToAsync($"{nameof(CollectionDetailsView)}?name={selectedCollection}");
        }

        [RelayCommand]
        private void CreateCollection()
        {
            if (string.IsNullOrEmpty(NewCollectionName)) return;
            if (App.Repository.CheckCollectionDuplicates(NewCollectionName)) return;

            CollectionsNames.Add(NewCollectionName);

            App.Repository.CreateCollection(NewCollectionName);
            NewCollectionName = string.Empty;

            LoadCollections();
        }
    }
}
