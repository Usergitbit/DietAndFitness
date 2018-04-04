using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace DietAndFitness
{
    [Table ("FoodItem")]
    public class FoodItem
    {

        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Calories { get; set; }
        public string Carbohydrates { get; set; }
        public string Proteins { get; set; }
        public string Fats { get; set; }
        [PrimaryKey, AutoIncrement]
        private int ID { get; }
        public FoodItem(string _name, string _proteins, string _carbohydrates, string _fats, string _quantity, string _calories)
        {
            Name = _name;
            Proteins = _proteins;
            Carbohydrates = _carbohydrates;
            Fats = _fats;
            Quantity = _quantity;
            Calories = _calories;
        }
        public FoodItem()
        {

        }
        public override string ToString()
        {
            return Name;
        }



    }
}
