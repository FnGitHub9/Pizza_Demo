using System.ComponentModel.DataAnnotations;

namespace ContosoPizza.Models
{
    public class Drink
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DrinkName { get; set; }
        public float Price { get; set; }

        public float CalculatePaymentAmount(int quantity)
        {
            return Price * quantity;
        }
    }
}
