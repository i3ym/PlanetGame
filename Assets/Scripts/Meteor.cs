using UnityEngine;

namespace Planet
{
    public class Meteor : MonoBehaviour
    {
        static BiasedRandom<MeteorEffect> BiasedRandom;

        public MeteorEffect Effect { get; protected set; }

        static Meteor()
        {
            BiasedRandom = new BiasedRandom<MeteorEffect>();

            BiasedRandom.Add(MeteorEffect.None, 100);
        }
        void Start()
        {
            Effect = BiasedRandom.Next();
        }
    }
}