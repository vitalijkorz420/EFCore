using System.Collections.ObjectModel;
using Spectre.Console;

namespace EFCore.UI;
internal class Menu
{
    private readonly List<MenuItem> menuItems = new();
    private MenuItem? currentParent = null;
    public Action? Show()
    {        
        string key;
        Action? result = null;
        while (true)
        {
            var elemens = this.menuItems.Where(i => i.Parent == this.currentParent).Select(i => i.Title).ToArray();
            var prompt = new SelectionPrompt<string>().AddChoices<string>(elemens);
            if (currentParent is not null)
                prompt.AddChoice("..");
            AnsiConsole.Clear();
            key = AnsiConsole.Prompt(prompt);
            var item = this.menuItems.Where(i => i.Title == key).FirstOrDefault();
            if (item is null)
            {
                currentParent = currentParent?.Parent;
                continue;
            }
            if (item.Action is null)
            {
                currentParent = item;
                continue;
            }
            result = item.Action;
            break;
        }
        return result;
    }
    public MenuItem? AddItem(string title, Action? action = null, MenuItem? parent = null)
    {
        if (title == ".." || string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Unable to add new menu item. The title provided can not be used");
        this.menuItems.Add(new MenuItem(title, action: action, parent: parent) { Id = this.menuItems.Count });
        return this.menuItems.LastOrDefault();
    }
    public bool RemoveItem() { return false; }
}

internal class MenuItem
{
    public int Id { get; set; }
    public MenuItem? Parent { get; set; }
    public string Title { get; }
    public Action? Action { get; set; }
    public MenuItem(string title, Action? action = null, MenuItem? parent = null)
    {
        this.Parent = parent;
        this.Title = title;
        this.Action = action;
    }
}