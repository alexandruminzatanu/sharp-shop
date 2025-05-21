﻿namespace SharpShop.Models.Base
{
    public class ProductModel(int id, string name, string description, decimal price, int stock)
    {

        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public decimal Price { get; set; } = price;
        public int Stock { get; set; } = stock;

    }
}
