using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ClientRepository : GenericRepository<Client> 
    {
        public bool IsAlreadyExistClient(string newPhone)
        {
            string alreadyPhone = _context.clients
                .Where(s => s.Phone.Equals(newPhone))
                .Select(s => s.Phone)
                .FirstOrDefault();

            return alreadyPhone == null;
        }


    }
}
