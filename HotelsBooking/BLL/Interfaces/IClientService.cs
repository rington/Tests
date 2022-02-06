using System;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IClientService : IDisposable
    {
        ClientDTO GetClientById(int clientId);

        ClientDTO GetClientByName(string clientName);

        ClientDTO GetClientByPassportId(string passportId);

        IEnumerable<ClientDTO> GetAllClients();

        void AddClient(ClientDTO client);

        void UpdateClient(ClientDTO client);

        bool DeleteClient(int clientId);

        void SaveChanges();
    }
}
