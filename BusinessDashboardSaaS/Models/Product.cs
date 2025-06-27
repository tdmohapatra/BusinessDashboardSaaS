﻿namespace BusinessDashboardSaaS.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string? Category { get; set; }
        public decimal Price { get; set; }
        public int StockQty { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
