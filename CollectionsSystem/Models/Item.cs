using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsSystem.Models
{
    public class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Status { get; set; }

        public int Rating { get; set; }

        public override string ToString()
        {
            return $"{Id};{Name};{Status};{Rating}";
        }

        public static Item FromString(string line)
        {
            string[] data = line.Split(';');
            return new Item
            {
                Id = Guid.Parse(data[0]),
                Name = data[1],
                Status = data[2],
                Rating = Int32.Parse(data[3])
            };
        }
        public static List<string> GetStatusOptions()
        {
            return new List<string> { "Nowy", "Używany", "Na sprzedaż", "Sprzedane", "Chcę kupić" };
        }
    }
}
