﻿using System.ComponentModel.DataAnnotations;

namespace StockTrackingMVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
		[Required]
		public string? CategoryName { get; set; }
    }
}
