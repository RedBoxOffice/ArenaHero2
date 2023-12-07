using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaHero.Utils.Other
{
	public static class Extension
	{
		public static T RandomElement<T>(this IEnumerable<T> source,
			Random rng)
		{
			if (source is ICollection<T> collection)
			{
				return source.ElementAt(rng.Next(collection.Count));
			}

			T current = default(T);
			int count = 0;
			
			foreach (T element in source)
			{
				count++;
				if (rng.Next(count) == 0)
				{
					current = element;
				}            
			}
			
			if (count == 0)
			{
				throw new InvalidOperationException("Sequence was empty");
			}
			return current;
		}
	}
}