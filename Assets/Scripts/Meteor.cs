using UnityEngine;

namespace Planet
{
    public class Meteor : MonoBehaviour
    {
        static readonly BiasedRandom<MeteorEffect> BiasedRandom;

        public MeteorEffect Effect { get; protected set; }

        Renderer Renderer;

        static Meteor()
        {
            BiasedRandom = new BiasedRandom<MeteorEffect>();

            BiasedRandom.Add(MeteorEffect.None, 100);
            BiasedRandom.Add(MeteorEffect.Slowdown, 5);
            BiasedRandom.Add(MeteorEffect.Speedup, 5);
        }
        void Start()
        {
            Renderer = GetComponent<Renderer>();
            Effect = BiasedRandom.Next();

            Renderer.material.color = Effect.Color;
            Renderer.material.SetColor("_EmissionColor", Effect.Color);
        }
    }
}