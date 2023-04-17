using EFCore.DAL;
using EFCore.Model;
using EFCore.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Service;
public class CategoryService : ICategoryService
{
    private readonly DataContext context;

    public CategoryService(DataContext context)
    {
        this.context = context;
    }

    public bool Exists(string name) => this.context.Category.Where(c => c.Name == name).Any();

    public Category? GetCategoryByName(string name) => this.context.Category.Where(c => c.Name == name).FirstOrDefault();

    public List<Category> GetCategoriesByName(string name)
        => this.context.Category.Where(c => c.Name.ToLower().Contains(name.ToLower())).ToList();

    public Category? GetCategoryById(int id) => this.context.Category.Find(id);

    public Category? Add(string name)
    {
        Category? added;
        try
        {
            added = this.context.Category.Add(new() { Name = name }).Entity;
            this.context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            added = null;
        }
        return added;
    }


    public int LoadProducts(Category category)
    {
        this.context.Entry(category).Collection(c => c.Products).Load();
        return category.Products.Count;
    }

    public void Delete(Func<Category, bool> filter, bool loadRalatedData = false)
    {
        var categoriesToDelete = (loadRalatedData) ? this.context.Category.Include(c => c.Products).Where(filter).ToList() : this.context.Category.Where(filter).ToList();
        if (categoriesToDelete != null)
        {
            this.context.Category.RemoveRange(categoriesToDelete);
            this.context.SaveChanges();
        }
    }

    public List<Category> Search(Func<Category, bool> filter, bool loadRalatedData = false)
        => (loadRalatedData) ? this.context.Category.Include(c => c.Products).Where(filter).ToList() : this.context.Category.Where(filter).ToList();

    public void Edit(int categoryIdToChange, string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
            return;

        var categoryToChange = GetCategoryById(categoryIdToChange);
        if (categoryToChange != null)
        {
            categoryToChange.Name = categoryName;
            context.SaveChanges();
        }
    }
}