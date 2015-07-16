using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebWithIoC.Models
{
    public interface IDataDbContext
    {
        IDbSet<Customer> Customers { get; set; }
        IDbSet<Order> Orders { get; set; }
        IDbSet<Product> Products { get; set; }
    }
    public class MockDataDbContext : IDataDbContext
    {
        public MockDataDbContext()
        {
            
        }
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Product> Products { get; set; }
    }
    public class DataDbContext : DbContext, IDataDbContext
    {
        public DataDbContext()
            : base("DefaultConnection")
        {
        }
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Product> Products { get; set; }
    }
    public class Customer
    {
        public int CustomerId { get; set; }
        public string NameFirst { get; set; }
        public string NameMiddle { get; set; }
        public string NameLast { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
    public class Order
    {
        public int ID { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public DateTime DateOrdered { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
    public class Initializer : System.Data.Entity.CreateDatabaseIfNotExists<DataDbContext>
    {
        protected override void Seed(DataDbContext context)
        {
            var customers = new List<Customer>
            {
                new Customer{ NameFirst = "Adam", NameLast="White", Address = "1 Bruce Street", City = "Salt Lake City", State = "UT", Zip = "10001"},
                new Customer{ NameFirst = "Bruce", NameLast="Red", Address = "2 Adam Way", City = "Draper", State = "UT", Zip = "10002"},
                new Customer{ NameFirst = "Candice", NameLast="Brown", Address = "3 Daniel Rd", City = "Sandy", State = "UT", Zip = "10003"},
                new Customer{ NameFirst = "Daniel", NameLast="Silver", Address = "4 Candice Lane", City = "Logan", State = "UT", Zip = "10004"}
            };
            ((DbSet)context.Customers).AddRange(customers);
            context.SaveChanges();
            var products = new List<Product>
            {
                new Product{ Name="ASUS V1", Price = 699.99M, Stock = 10, Unit = "item"},
                new Product{ Name="HP", Price = 899.99M, Stock = 10, Unit = "item"},
                new Product{ Name="Apple Macbook V1", Price = 1799.99M, Stock = 0, Unit = "item"},
                new Product{ Name="Blueberries", Price = 1.99M, Stock = 100, Unit = "oz"}
            };
            ((DbSet)context.Products).AddRange(products);
            context.SaveChanges();
            var orders = new List<Order>
            {
                new Order{ CustomerId=1, ProductId = 1, Count = 1, DateOrdered = new DateTime(2015,7,15,14,15,10)},
                new Order{ CustomerId=1, ProductId = 1, Count = 3, DateOrdered = new DateTime(2015,7,15,14,16,20)},
                new Order{ CustomerId=2, ProductId = 4, Count = 2, DateOrdered = new DateTime(2015,7,15,14,17,30)},
                new Order{ CustomerId=2, ProductId = 4, Count = 1, DateOrdered = new DateTime(2015,7,15,14,18,40)}
            };
            ((DbSet)context.Orders).AddRange(orders);
            context.SaveChanges();
        }
    }
}