
using EFCore.DAL;
using EFCore.Model;
using EFCore.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCore.Service;
public class ProductService : IProductService
{
    private readonly DataContext context;

    public ProductService(DataContext context)
    {
        this.context = context;
    }

    public List<Product> GetProductsByName(string productName)
        => this.context.Product.Where(p => p.Name.ToLower().Contains(productName.ToLower())).ToList();

    public Product? GetProductById(int id) => this.context.Product.Find(id);

    public Product Add(Product product)
    {
        this.context.Product.Add(product);
        this.context.SaveChanges();
        return product;
    }

    public int LoadCategories(Product product)
    {
        this.context.Product.Entry(product).Collection(p => p.Categories).Load();
        return product.Categories.Count;
    }

    public List<Product> Search(Func<Product, bool> filter, bool loadRalatedData = false)
        => (loadRalatedData) ? this.context.Product.Include(p => p.Categories).Where(filter).ToList() : this.context.Product.Where(filter).ToList();

    public void Delete(Func<Product, bool> filter, bool loadRalatedData = false)
    {
        var productsToDelete = (loadRalatedData) ? this.context.Product.Include(p => p.Categories).Where(filter).ToList() : this.context.Product.Where(filter).ToList();
        if (productsToDelete != null)
        {
            this.context.Product.RemoveRange(productsToDelete);
            this.context.SaveChanges();
        }
    }

    public void Edit(int productIdToChange, string productName, string productDescription, decimal productPrice)
    {
        if (string.IsNullOrWhiteSpace(productName) || string.IsNullOrWhiteSpace(productName) || productPrice < 0)
            return;

        var productToChange = GetProductById(productIdToChange);
        if (productToChange != null)
        {
            productToChange.Name = productName;
            productToChange.Description = productName;
            productToChange.Price = productPrice;
            context.SaveChanges();
        }
    }
}