using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IRepository
    {
        public void Create(BaseEntity entity);

        public void Update(BaseEntity entity);

        public BaseEntity Get(int id);

        public List<BaseEntity> GetAll();

        public void Delete(BaseEntity entity);
    }
}
