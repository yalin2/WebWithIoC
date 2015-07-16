using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWithIoC.Models
{
    public interface IInventoryManagable
    {
        int GetInvetory(int productId);
        int UseInventory(int productId, int count);
        int SetInventory(int productId, int count);
    }
    public class InventoryManagable : IInventoryManagable
    {
        private DataDbContext context = new DataDbContext();
        public int SetInventory(int productId, int count)
        {
            Product product = context.Products.Find(productId);
            if (product != null)
            {
                product.Stock = count;
                context.SaveChanges();
                return product.Stock;
            }
            else
            {
                throw new InvalidOperationException("There's not product with this id in the database.");
            }
        }

        public int GetInvetory(int productId)
        {
            Product product = context.Products.Find(productId);
            if (product != null)
            {
                return product.Stock;
            }
            else
            {
                throw new InvalidOperationException("There's not product with this id in the database.");
            }
        }

        public int UseInventory(int productId, int count)
        {
            Product product = context.Products.Find(productId);
            if (product != null)
            {
                product.Stock = product.Stock - count;
                context.SaveChanges();
                return product.Stock;
            }
            else
            {
                throw new InvalidOperationException("There's not product with this id in the database.");
            }
        }
    }
    public class InventoryManagableMock : IInventoryManagable
    {
        private int inventoryCount = 0;
        public int SetInventory(int productId, int count)
        {
            inventoryCount = count;
            return count;
        }

        public int GetInvetory(int productId)
        {
            return inventoryCount;
        }

        public int UseInventory(int productId, int count)
        {
            if (inventoryCount - count < 0)
            {
                throw new InvalidOperationException("There are only " + inventoryCount + " items in stock.");
            }
            else
            {
                return inventoryCount -= count;
            }
        }
    }
}