using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public ClientService(IUnitOfWork uow)
        {
            _uow = uow;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Client, ClientDTO>();
                cfg.CreateMap<ClientDTO, Client>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        public ClientDTO GetClientById(int clientId)
        {
            var client = _uow.Clients.Get(clientId);
            return _mapper.Map<Client, ClientDTO>(client);
        }

        public IEnumerable<ClientDTO> GetAllClients()
        {
            var clients = _uow.Clients.GetAll();
            return _mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(clients);
        }

        public void AddClient(ClientDTO client)
        {
            _uow.Clients.Create(_mapper.Map<ClientDTO, Client>(client));
        }

        public void UpdateClient(ClientDTO client)
        {
            _uow.Clients.Update(_mapper.Map<ClientDTO, Client>(client));
        }

        public bool DeleteClient(int clientId)
        {
            return _uow.Clients.Delete(clientId);
        }

        public ClientDTO GetClientByName(string clientName)
        {
            var client = _uow.Clients.Find(c => c.Name == clientName).First();
            return _mapper.Map<Client, ClientDTO>(client);
        }

        public ClientDTO GetClientByPassportId(string passportId)
        {
            var client = _uow.Clients.Find(c => c.PassportId == passportId).First();
            return _mapper.Map<Client, ClientDTO>(client);
        }

        public void SaveChanges()
        {
            _uow.Save();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
