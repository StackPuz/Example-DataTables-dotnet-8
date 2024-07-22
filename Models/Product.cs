using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public partial class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}