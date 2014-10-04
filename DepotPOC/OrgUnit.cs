using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepotPOC
{
    public class OrgUnit : Persistable
    {
        public string Name { get; set; }
        public OrgUnit Parent { get; set; }
        public ICollection<OrgUnit> Children { get; set; }

        public OrgUnit()
        {
            Children = new List<OrgUnit>();
        }
    }
}
