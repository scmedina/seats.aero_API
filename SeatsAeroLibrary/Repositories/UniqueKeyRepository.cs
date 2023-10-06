using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public abstract class UniqueKeyRepository<TKey, TAlternateKey, TItem> : IRepository<TItem, TKey>
        where TKey : struct
        where TAlternateKey : struct
    {
        protected Dictionary<TKey, TItem> PrimaryKeyDictionary { get; } = new Dictionary<TKey, TItem>();
        protected Dictionary<TAlternateKey?, TKey> AlternateKeyDictionary { get; } = new Dictionary<TAlternateKey?, TKey>();

        public abstract TKey GetPrimaryKey(TItem item);
        public abstract TAlternateKey? GetAlternateKey(TItem item);

        public void AddItem(TItem item)
        {
            TKey primaryKey = GetPrimaryKey(item);
            TAlternateKey? alternateKey = GetAlternateKey(item);

            if (!PrimaryKeyDictionary.ContainsKey(primaryKey) && (alternateKey == null || !AlternateKeyDictionary.ContainsKey(alternateKey)))
            {
                PrimaryKeyDictionary[primaryKey] = item;
                if (alternateKey != null)
                {
                    AlternateKeyDictionary[alternateKey] = primaryKey;
                }
            }
            else
            {
                // Handle duplicate key error
                Console.WriteLine("Item with the same key already exists.");
            }
        }

        public TItem GetByPrimaryKey(TKey key)
        {
            if (PrimaryKeyDictionary.TryGetValue(key, out TItem item))
            {
                return item;
            }
            else
            {
                // Handle key not found error
                Console.WriteLine("Item with the specified primary key not found.");
                return default(TItem);
            }
        }

        public TItem GetByAlternateKey(TAlternateKey alternateKey)
        {
            if (AlternateKeyDictionary.TryGetValue(alternateKey, out TKey primaryKey))
            {
                return GetByPrimaryKey(primaryKey);
            }
            else
            {
                // Handle key not found error
                Console.WriteLine("Item with the specified alternate key not found.");
                return default(TItem);
            }
        }

        public abstract void Update(TItem item);
        public abstract void Delete(TKey id);

        public void Initialize(IConfigSettings configSettings)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public TItem GetById(TKey id)
        {
            throw new NotImplementedException();
        }

        public void Add(TItem entity)
        {
            throw new NotImplementedException();
        }

        public TKey GetID(TItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
