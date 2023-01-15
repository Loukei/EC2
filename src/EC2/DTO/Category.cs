﻿namespace EC2.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public IFormFile? Picture { get; set; }
    }
}