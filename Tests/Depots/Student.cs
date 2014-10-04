using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Builders;

namespace Tests.Depots
{
    public static partial class Depot
    {
        public static class Student
        {
            public static IListBuilder<StudentBuilder> Random(int n)
            {
                var Names = new HashSet<string>();
                var rand = new Random();
                while (Names.Count < n)
                {
                    Names.Add(General.RandomStringUsing(rand.Next(2, 10), rand) + General.RandomStringUsing(rand.Next(2, 10), rand));
                }
                var nameList = Names.ToList();

                int index = 0;
                return Builder<StudentBuilder>.CreateListOfSize(n).All().With(x => x.WithName(nameList[index++]));
            }
        }
    }
}
