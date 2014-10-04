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
    public class StudentBuilder : PersistableBuilder<Student, StudentBuilder>
    {
        IListBuilder<StudentBuilder> _suitemates;

        public StudentBuilder()
        {
            WithName("John Smith");
        }

        public StudentBuilder WithName(string name)
        {
            Set(x => x.Name, name);
            return this;
        }

        public StudentBuilder WithSuitemates(IListBuilder<StudentBuilder> suitemateSpecs)
        {
            _suitemates = suitemateSpecs;
            return this;
        }

        public void PerformSomeMutation()
        {
        }


        protected override Student _BuildObject()
        {
            ICollection<Student> Suitemates = new List<Student>();
            var student = new Student
            {
                Name = GetOrDefault(x => x.Name),
                Suitemates = GetOrDefault(x => x.Suitemates) ?? new List<Student>(),
                Classes = GetOrDefault(x => x.Classes) ?? new List<Class>()
            };

            // A student and his suitemates must always form a complete graph.  i.e. if you're in my suitemate collection, I'm in your suitemate collection.
            if (_suitemates != null)
            {
                var students = _suitemates.All().With(x => { x.Set(y => y.Suitemates, null); return x; }).BuildList<Student, StudentBuilder>().ToList();
                students.Add(student);
                student.Suitemates = students;
                foreach (var s in students)
                    s.Suitemates = students.ToList();
            }
            return student;
        }
    }
}
