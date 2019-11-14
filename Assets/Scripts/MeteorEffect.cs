using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planet
{
    public class MeteorEffect
    {
        public static readonly IReadOnlyList<MeteorEffect> Effects = new List<MeteorEffect>();
        static readonly Dictionary<Player, Dictionary<MeteorEffect, int>> ActiveEffects =
            new Dictionary<Player, Dictionary<MeteorEffect, int>>();

        #region static effects

        public static MeteorEffect None = new MeteorEffect(Color.white,
            (player) => (player as IHasGameManager).GameManager.GameOver());

        public static MeteorEffect Slowdown = new MeteorEffect(Color.red, 5f,
            (player) =>
            {
                player.Speed -= .5f;
            },
            (player) =>
            {
                player.Speed += .5f;
            });

        public static MeteorEffect Speedup = new MeteorEffect(Color.green, 5f,
            (player) =>
            {
                player.Speed += .5f;
            },
            (player) =>
            {
                player.Speed -= .5f;
            });

        #endregion

        public readonly Color Color;
        public readonly Action<Player> OnCollideWithPlayer = delegate { };

        protected MeteorEffect(Color color)
        {
            Color = color;
            (Effects as List<MeteorEffect>).Add(this);
        }
        protected MeteorEffect(Color color, Action<Player> onCollideWithPlayer) : this(color)
        {
            OnCollideWithPlayer = onCollideWithPlayer;
        }
        protected MeteorEffect(Color color, float effectTime, Action<Player> onEffectStart, Action<Player> onEffectStop) : this(color)
        {
            OnCollideWithPlayer = (player) => player.StartCoroutine(PlayerEffectCoroutine(player, effectTime, onEffectStart, onEffectStop));
        }

        IEnumerator PlayerEffectCoroutine(Player player, float effectTime, Action<Player> onEffectStart, Action<Player> onEffectStop)
        {
            if (!ActiveEffects.ContainsKey(player))
                ActiveEffects.Add(player, new Dictionary<MeteorEffect, int>());

            var activeEffects = ActiveEffects[player];

            if (activeEffects.ContainsKey(this))
            {
                activeEffects[this]++;
                yield break;
            }

            activeEffects.Add(this, 1);

            onEffectStart(player);

            while (activeEffects[this] != 0)
                yield return new WaitForSeconds(effectTime);

            onEffectStop(player);
            ActiveEffects[player].Remove(this);
        }
    }
}