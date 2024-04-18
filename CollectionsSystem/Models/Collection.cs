using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsSystem.Models
{
    public class Collection
    {
        public required string Name { get; set; }
        public required List<Item> Items { get; set; }

        public override string ToString()
        {
            string data = string.Empty;
            foreach (Item item in Items)
            {
                data += item.ToString() + "\r\n";
            }
            return data;
        }
        public static Collection FromString(string name, string data)
        {
            string[] lines = data.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            List<Item> items = new List<Item>();
            foreach (string line in lines)
            {
                items.Add(Item.FromString(line));
            }

            return new Collection
            {
                Name = name,
                Items = items
            };
        }
    }
}
