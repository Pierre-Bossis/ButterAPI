using Butter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butter.Repositories.Interfaces
{
    public interface IRepo<M,T,U>
        where T : class
    {
        IEnumerable<M> Get();
        M GetById(U id);

        M Add(M item);
        void Update(M item);
        void Delete(U id);
    }
}
