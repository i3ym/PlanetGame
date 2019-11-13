using System;
using System.Collections.Generic;
using System.Linq;

namespace Planet
{
    public class BiasedRandom<T>
    {
        readonly Random Random = new Random();
        readonly List<T> Bias = new List<T>();

        public T Next() => Bias[Random.Next(Bias.Count)];
        public void Add(T number, int count) => Bias.AddRange(Enumerable.Repeat(number, count));
    }
}