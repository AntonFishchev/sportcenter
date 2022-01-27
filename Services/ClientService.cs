using Data;
using Data.Repository;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ClientService
    {
        private readonly ClientRepository _clientRepository;

        public ClientService()
        {
            _clientRepository = new ClientRepository();
        }

        public string RegisterClient(
                string lastName,
                string firstName,
                string middleName,
                string phone,
                string organization
            )
        {
            if(lastName != "" && firstName != "" && phone != "")
            {
                if (_clientRepository.IsAlreadyExistClient(phone))
                {
                    Client client;
                    if (middleName != "" || organization != "")
                    {
                        client = new Client(lastName, firstName, middleName, phone, organization);
                    }
                    else
                    {
                        client = new Client(lastName, firstName, phone);
                    }
                    _clientRepository.Create(client);
                    return $"Клиент {lastName} {firstName} {middleName} добавлен(а)";
                }
                else
                {
                    return $"Клиент с номером {phone} уже существует";
                }
                
            }
            else
            {
                return "Введите обязательные параметры";
            }
        }

        public List<Client> GetAllClients()
        {
            return _clientRepository.GetAll();
        }

        public bool ChangeLastName(Client client, string newValue)
        {
            if (newValue.Length != 0 && newValue != null &&
                newValue.All(char.IsLetter))
            {
                client.LastName = newValue;
                _clientRepository.Update(client);
                return true;
            }
            return false;
        }

        public bool ChangeFirstName(Client client, string newValue)
        {
            if (newValue.Length != 0 && newValue != null &&
                newValue.All(char.IsLetter))
            {
                client.FirstName = newValue;
                _clientRepository.Update(client);
                return true;
            }
            return false;
        }

        public bool ChangeMiddleName(Client client, string newValue)
        {
            if (newValue.Length != 0 && newValue != null &&
                newValue.All(char.IsLetter))
            {
                client.MiddleName = newValue;
                _clientRepository.Update(client);
                return true;
            }
            return false;
        }

        public bool ChangePhone(Client client, string newValue)
        {
            if (newValue.Length != 0 && newValue != null &&
                newValue.All(char.IsDigit))
            {
                client.Phone = newValue;
                _clientRepository.Update(client);
                return true;
            }
            return false;
        }

        public bool ChangeOrganization(Client client, string newValue)
        {
            if (newValue.Length != 0 && newValue != null)
            {
                client.Organization = newValue;
                _clientRepository.Update(client);
                return true;
            }
            return false;
        }
    }
}
