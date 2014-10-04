using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTestDataBuilder;
using DepotPOC;

namespace Tests.Builders
{
    public abstract class PersistableBuilder<TPersistable, TBuilder> : TestDataBuilder<TPersistable, TBuilder>
        where TPersistable : Persistable
        where TBuilder : PersistableBuilder<TPersistable, TBuilder>
    {
        private UnitOfWork _store;

        public TBuilder Persisted(UnitOfWork uow)
        {
            _store = uow;
            return this as TBuilder;
        }

        protected override TPersistable BuildObject()
        {
            var obj = _BuildObject();
            if (_store != null) _store.Save(obj);
            return obj;
        }

        protected abstract TPersistable _BuildObject();
    }
}
