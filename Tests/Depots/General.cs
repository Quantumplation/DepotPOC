using DepotPOC;
using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Depots
{
    public static class General
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden
        public static T RandomElement<T>(this IEnumerable<T> list)
        {
            return RandomElementUsing(list, random);
        }
        public static T RandomElementUsing<T>(this IEnumerable<T> list, Random rand)
        {
            if (!list.Any()) return default(T);
            var index = rand.Next(0, list.Count());
            return list.ElementAt(index);
        }

        public static string RandomString(int size)
        {
            return RandomStringUsing(size, random);
        }
        public static string RandomStringUsing(int size, Random rand)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(random.Next(65, 91));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
