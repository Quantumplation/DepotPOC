using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepotPOC
{
    public class UnitOfWork
    {
        private Dictionary<int, Persistable> _data = new Dictionary<int, Persistable>();
        int id;

        public T Get<T>(int id) where T : Persistable { return Get<T>(new[] { id }).SingleOrDefault(); }
        public IEnumerable<T> Get<T>(IEnumerable<int> ids) where T : Persistable
        {
            return from id in ids select _data[id] as T;
        }

        public void Save<T>(T ts) where T : Persistable { Save<T>(new[] { ts }); }
        public void Save<T>(IEnumerable<T> ts) where T : Persistable
        {
            foreach (var t in ts)
            {
                if (t.Id == 0) t.Id = id++;
                _data[t.Id] = t;
            }
        }
    }
}
