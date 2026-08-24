using CSVSalesPro.Services.Exceptions;
using CSVSalesPro.Services.Interfaces;

namespace CSVSalesPro.Services
{
    public class RepositoryService<T> : IRepositoryService<T>
    {
        private List<T> _entities = new List<T>();

        public int Count()
        {
            return _entities.Count;
        }

        public void Clear()
        {
            _entities.Clear();
        }

        public void Add(T Entity)
        {
            if (Exists(Entity))
            {
                throw new RepositoryException($"{Entity} already registred!");
            }
            _entities.Add(Entity);
        }

        public void Remove(T Entity)
        {
            if (!Exists(Entity))
            {
                throw new RepositoryException($"{Entity} not registered, please add!");
            }
            _entities.Remove(Entity);
        }

        public List<T> GetAll()
        {

            if (!ValidateList(_entities))
            {
                throw new RepositoryException("Currently, there are no registered entities!");
            }
            return _entities.ToList();
        }

        public T GetFirst()
        {

            if (!ValidateList(_entities))
            {
                throw new RepositoryException("Currently, there are no registered entites!");
            }
            return _entities.First();
        }

        public T GetLast()
        {

            if (!ValidateList(_entities))
            {
                throw new RepositoryException("Currently, there are no registered entites!");
            }
            return _entities.Last();
        }

        public T? Find(Predicate<T> predicate)
        {
            if (!ValidateList(_entities))
            {
                throw new RepositoryException("Currently, there are no registered entites!");
            }
            return _entities.Find(predicate);
        }

        public bool Exists(T Entity)
        {
            return _entities.Contains(Entity);
        }

        private bool ValidateList(List<T> _entities)
        {
            return _entities != null && _entities.Count > 0;
        }
    }
}
