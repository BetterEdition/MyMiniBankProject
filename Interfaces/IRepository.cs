using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IRepository<T, K>
    {
        int Count { get; }

        void Add(T e);
        void Remove(T e);
        T GetById(K id);
        IList<T> GetAll();
    }
}
