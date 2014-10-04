using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepotPOC
{
    public class Student : Persistable
    {
        public string Name { get; set; }
        public ICollection<Student> Suitemates { get; set; }
        public ICollection<Class> Classes { get; set; }

        public Student()
        {
            Suitemates = new List<Student>();
            Classes = new List<Class>();
        }
    }
}
