using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2._1
{
    internal static class Extended
    {
        public static string ToStringEx(this Product prod, List<Tag> tags)
        {
            string result = $"#{prod.Id} {prod.Brand} — {prod.Model} — ${prod.Cost} [";


            var temp = tags.Where(x => x.Id.Equals(prod.Id)).Select(x => result += $"{x.Value}, ").ToArray().ToString();
            result.Concat(temp);

            return result.Remove(result.Length - 2, 2) + "]";
        }

        public static string ToStringEx(this Tag tag, List<Product> prods, List<Tag> tags)
        {
            int i;
            for (i = 0; i < prods.Count; i++) if (tag.Id.Equals(prods[i].Id)) break;

            string result = $"#{prods[i].Id} {prods[i].Brand} — {prods[i].Model} — ${prods[i].Cost} [";


            var temp = tags.Where(x => x.Id.Equals(prods[i].Id)).Select(x => result += $"{x.Value}, ").ToArray().ToString();
            result.Concat(temp);

            return result.Remove(result.Length - 2, 2) + "]";
        }

    }
}
