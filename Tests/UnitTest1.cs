using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Builders;
using Tests.Depots;
using FizzWare.NBuilder;
using DepotPOC;
using NTestDataBuilder;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var uow = new UnitOfWork();
            var students = StudentBuilder.CreateListOfSize(20)
                .All()
                    .With(s => s.Persisted(uow))
                .TheFirst(3)
                    .Do(s => s.PerformSomeMutation())
                .Section(6, 10)
                    .Do(s => s.PerformSomeMutation())
                .Random(10)
                    .With(s => s.WithSuitemates(Depot.Student.Random(3).All().With(x => x.Persisted(uow))))
                    .With(s => s.Persisted(uow))
                .Random(2)
                    .With(s => s.WithName("WOA"))
                .TheLast(1)
                    .With(s => s.WithSuitemates(Depot.Student.Random(5).All().With(x => x.Persisted(uow))))
                .BuildList<Student, StudentBuilder>();
            foreach (var student in students)
            {
                Assert.IsNotNull(uow.Get<Student>(student.Id));
                foreach (var suitemate in student.Suitemates)
                {
                    Assert.IsNotNull(suitemate.Name);
                    Assert.IsNotNull(uow.Get<Student>(suitemate.Id));
                    Assert.IsTrue(suitemate.Suitemates.Contains(student));
                }
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var ouTree = Depot.OrgUnits.Tree(10, 10);
            var ous = Builder<OrgUnit>.CreateListOfSize(2500).BuildHierarchy(ouTree);
        }
    }
}
