using System;
using System.Collections.Generic;
using System.Text;

namespace CSVSalesPro.Services.Interfaces
{
    internal interface IRepositoryService<T>
    {
        int Count();

        void Clear();
        void Add(T Entity);

        void Remove(T Entity);

        List<T> GetAll();

        bool Exists(T Entity);

        T GetFirst();

        T GetLast();

        T Find(Predicate<T> predicate);
    }
}
