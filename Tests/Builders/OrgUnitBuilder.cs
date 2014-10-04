using DepotPOC;
using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTestDataBuilder;

namespace Tests.Builders
{
    public class OrgUnitBuilder : PersistableBuilder<OrgUnit, OrgUnitBuilder>
    {
        OrgUnitBuilder parentBuilder;
        OrgUnit parent;
        IListBuilder<OrgUnitBuilder> childrenBuilder;
        IEnumerable<OrgUnit> children;

        List<OrgUnitBuilder> addedChildrenBuilders = new List<OrgUnitBuilder>();
        List<OrgUnit> addedChildren = new List<OrgUnit>();

        public OrgUnitBuilder WithParent(OrgUnit p)
        {
            if (parentBuilder != null)
                throw new InvalidOperationException();
            parent = p;
            return this;
        }
        public OrgUnitBuilder WithParent(OrgUnitBuilder p)
        {
            if (parent != null)
                throw new InvalidOperationException();
            parentBuilder = p;
            return this;
        }

        public OrgUnitBuilder WithChildren(IListBuilder<OrgUnitBuilder> c)
        {
            childrenBuilder = c;
            return this;
        }
        public OrgUnitBuilder WithChildren(IEnumerable<OrgUnit> c)
        {
            children = c;
            return this;
        }

        public OrgUnitBuilder WithChild(OrgUnitBuilder c)
        {
            addedChildrenBuilders.Add(c);
            return this;
        }
        public OrgUnitBuilder WithChild(OrgUnit c)
        {
            addedChildren.Add(c);
            return this;
        }

        protected override OrgUnit _BuildObject()
        {
            var builtParent = parent ?? parentBuilder.Build();
            var ou = new OrgUnit
            {
                Name = GetOrDefault(x => x.Name),
                Parent = parent,
            };
            var builtChildren = (children ?? Enumerable.Empty<OrgUnit>())
                                .Union(childrenBuilder.All()
                                    .With(c => c.WithParent(ou))
                                .BuildList<OrgUnit, OrgUnitBuilder>()).ToList();
            foreach (var child in addedChildrenBuilders)
            {
                builtChildren.Add(child.WithParent(ou).Build());
            }
            foreach (var child in addedChildren)
            {
                child.Parent = ou;
                builtChildren.Add(child);
            }
            ou.Children = builtChildren.ToList();
            return ou;
        }
    }
}
