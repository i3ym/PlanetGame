using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planet
{
    public class MeteorEffect
    {
        public static IReadOnlyList<MeteorEffect> Effects = new List<MeteorEffect>();

        #region static effects

        public static MeteorEffect None = new MeteorEffect(Color.red,
            (player) => (player as IHasGameManager).GameManager.GameOver());

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
            onEffectStart(player);
            yield return new WaitForSeconds(effectTime);
            onEffectStop(player);
        }
    }
}