using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Range(0, int.MaxValue)]
        public int QuantityPurchased { get; set; }

        // Foreign key property
        public int ProductId { get; set; }
    }
}
