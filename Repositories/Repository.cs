using System;
using System.Collections.Generic;
using Interfaces;

namespace Repositories
{
    public class Repository<T, K> : IRepository<T, K>
    {
        //public IList<T> items = new List<T>();
        
        public int Count
        {
            get
            {
                return 0;// items.Count;
            }
        }

        public Repository()
        {
        }

        public void Add(T e)
        {
            throw new NotImplementedException();
        }

        public void Remove(T e)
        {
            throw new NotImplementedException();
        }

        public T GetById(K id)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}