using EFCore.Model;

namespace EFCore.Shared.Interfaces;
public interface IClientService
{
    Client? Add(Client client);
    Client? GetClientById(int id);
    Client? GetClientByPhoneNumber(string phone);
    Client? GetClientByEmail(string email);
    List<Client> GetClientsByLastName(string lastName);
    bool IsEmailInUse(string email);
    void Delete(Func<Client, bool> filter, bool loadRalatedData = false);
    int LoadOrders(Client client);
    void Edit(int clientIdToChange, string clientFirstName, string clientLastName, string clientEmail, string clientPhone);
}