using EFCore.Model;

namespace EFCore.Shared.Interfaces;
public interface IClientService
{
    Client? Add(Client client);
    Client? GetClientById(int id);
    List<Client> GetClientsByLastName(string lastName);
    bool IsEmailInUse(string email);
}