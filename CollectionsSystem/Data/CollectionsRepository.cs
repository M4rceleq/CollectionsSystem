using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionsSystem.Models;

namespace CollectionsSystem.Data
{
    public class CollectionsRepository
    {
        private readonly string directory;

        public CollectionsRepository(string directory)
        {
            this.directory = directory;
        }

        private string CreatePath(string collectionName)
        {
            return Path.Combine(directory, $"{collectionName}.txt");
        }

        public void CreateCollection(string collectionName)
        {
            File.WriteAllText(CreatePath(collectionName), string.Empty);
        }

        public void SaveCollection(Collection collection)
        {
            File.WriteAllText(CreatePath(collection.Name), collection.ToString());
        }

        public void RenameCollection(string collectionName, string newCollectionName)
        {
            File.Move(CreatePath(collectionName), CreatePath(newCollectionName));
        }

        public void DeleteCollection(string collectionName)
        {
            File.Delete(CreatePath(collectionName));
        }

        public Collection LoadCollection(string collectionName)
        {
            return Collection.FromString(collectionName, File.ReadAllText(CreatePath(collectionName)));
        }

        public IEnumerable<string> GetCollectionsNames()
        {
            return Directory
                .EnumerateFiles(directory, "*.txt")
                .Select(Path.GetFileName)
                .Select(fileName => fileName.Split('.')[0]);
        }

        public bool CheckCollectionDuplicates(string collectionToCheck)
        {
            foreach (var collectionName in GetCollectionsNames())
            {
                if (collectionName == collectionToCheck)
                {
                    return true;
                }
            }
            return false;
        }


        public void AddItem(string collectionName, Item item)
        {
            File.AppendAllText(CreatePath(collectionName), item.ToString() + "\r\n");
        }

        public void EditItem(string collectionName, Item oldItem, Item newItem)
        {
            Collection collection = LoadCollection(collectionName);
            collection.Items = collection.Items.Select(item => item.Id == oldItem.Id ? newItem : item).ToList();
            SaveCollection(collection);
        }

        public bool CheckItemDuplicates(string collectionName, string nameToCheck)
        {
            Collection collection = LoadCollection(collectionName);
            foreach (var item in collection.Items)
            {
                if (item.Name == nameToCheck)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
