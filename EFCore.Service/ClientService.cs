using EFCore.DAL;
using EFCore.Model;
using EFCore.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace EFCore.Service;
public class ClientService : IClientService
{
    private readonly DataContext context;

    public ClientService(DataContext context)
    {
        this.context = context;
    }

    public List<Client> GetClientsByLastName(string lastName)
        => this.context.Client.Where(c => c.LastName.ToLower().Contains(lastName.ToLower())).ToList();

    public Client? GetClientById(int id) => this.context.Client.Find(id);

    public bool IsEmailInUse(string email) => this.context.Client.Where(c => c.Email == email).Any();
    public Client? Add(Client client)
    {
        Client? added;
        try
        {
            added = this.context.Client.Add(client).Entity;
            this.context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            added = null;
        }
        return added;
    }

    public void Delete(Func<Client, bool> filter, bool loadRalatedData = false)
    {
        var clientsToDelete = (loadRalatedData) ? this.context.Client.Include(c => c.Orders).Where(filter).ToList() : this.context.Client.Where(filter).ToList();
        if (clientsToDelete != null)
        {
            this.context.Client.RemoveRange(clientsToDelete);
            this.context.SaveChanges();
        }
    }

    public int LoadOrders(Client client)
    {
        this.context.Client.Entry(client).Collection(c => c.Orders).Load();
        return client.Orders.Count;
    }

    public Client? GetClientByPhoneNumber(string phone)
        => this.context.Client.FirstOrDefault(c => c.Phone == phone);

    public Client? GetClientByEmail(string email)
        => this.context.Client.FirstOrDefault(c => c.Email == email);

    public void Edit(int clientIdToChange, string clientFirstName, string clientLastName, string clientEmail, string clientPhone)
    {
        if (string.IsNullOrWhiteSpace(clientFirstName) || string.IsNullOrWhiteSpace(clientLastName) ||
            string.IsNullOrWhiteSpace(clientEmail) || string.IsNullOrWhiteSpace(clientPhone))
            return;

        var clientToChange = GetClientById(clientIdToChange);
        if (clientToChange != null)
        {
            clientToChange.FirstName = clientFirstName;
            clientToChange.LastName = clientLastName;
            clientToChange.Email = clientEmail;
            clientToChange.Phone = clientPhone;
            context.SaveChanges();
        }
    }
}