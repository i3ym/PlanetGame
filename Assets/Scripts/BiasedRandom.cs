using System;
using System.Collections.Generic;
using System.Linq;

namespace Planet
{
    public class BiasedRandom
    {
        readonly Random Random = new Random();
        readonly List<int> Bias = new List<int>();

        public int Next() => Bias[Random.Next(Bias.Count)];
        public void AddNumber(int number, int count) => Bias.AddRange(Enumerable.Repeat(number, count));
    }
}