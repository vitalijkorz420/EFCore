using EFCore.Model;

namespace EFCore.Shared.Interfaces;
public interface ICategoryService
{
    Category? Add(string name);
    bool Exists(string name);
    List<Category> GetCategoriesByName(string name);
    Category? GetCategoryById(int id);
    Category? GetCategoryByName(string name);
    int LoadProducts(Category category);
}