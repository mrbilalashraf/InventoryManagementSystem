﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Range(0, int.MaxValue)]
        public int QuantitySold { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
