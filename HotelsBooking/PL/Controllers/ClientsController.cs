using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using PL.Models;

namespace PL.Controllers
{
    public class ClientsController : ApiController
    {
        private readonly IClientService _clientsService;

        private readonly IMapper _mapper;

        public ClientsController(IClientService service)
        {
            _clientsService = service;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientDTO, ClientView>();
                cfg.CreateMap<ClientView, ClientDTO>();
            });

            _mapper = config.CreateMapper();
        }

        [Route("api/clients/{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var client = _clientsService.GetClientById(id);

            if (client == null)
            {
                return NotFound();
            }
            
            var clientView = _mapper.Map<ClientDTO, ClientView>(client);

            return Ok(clientView);
        }

        [Route("api/clients")]
        [HttpGet]
        public IEnumerable<ClientView> GetAll()
        {
            var clients = _clientsService.GetAllClients();

            return _mapper.Map<IEnumerable<ClientDTO>, IEnumerable<ClientView>>(clients);
        }

        [Route("api/clients")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]ClientView client)
        {
            var clientDTO = _mapper.Map<ClientView, ClientDTO>(client);

            _clientsService.AddClient(clientDTO);

            _clientsService.SaveChanges();

            return Ok();
        }

        [Route("api/clients")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]ClientView client)
        {
            var clientDTO = _mapper.Map<ClientView, ClientDTO>(client);

            _clientsService.UpdateClient(clientDTO);

            _clientsService.SaveChanges();

            return Ok();
        }

        [Route("api/clients/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (_clientsService.DeleteClient(id))
            {
                _clientsService.SaveChanges();

                return Ok();
            }

            return NotFound();
        }

        [Route("api/clients/name/{clientName}")]
        [HttpGet]
        public ClientView GetClientByName(string clientName)
        {
            var client = _clientsService.GetClientByName(clientName);

            return _mapper.Map<ClientDTO, ClientView>(client);
        }

        [Route("api/clients/passportId/{passportId}")]
        [HttpGet]
        public ClientView GetClientByPassportId(string passportId)
        {
            var client = _clientsService.GetClientByPassportId(passportId);

            return _mapper.Map<ClientDTO, ClientView>(client);
        }
    }
}
