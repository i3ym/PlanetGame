using UnityEngine;

namespace Planet
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour, IStartGameCallback, IGameOverCallback, IHasGameManager
    {
        [SerializeField] GameManager GameManager = null;
        [SerializeField] public float Speed = 1f;

        Transform Parent;

        GameManager IHasGameManager.GameManager => GameManager;

        void Start()
        {
            Parent = transform.parent;

            GameManager.AddCallback(this as IStartGameCallback);
            GameManager.AddCallback(this as IGameOverCallback);
        }
        void FixedUpdate()
        {
            if (!GameManager.IsAlive) return;

            Parent.Rotate((1f + Input.GetAxis("Vertical") / 2f) * Speed, Input.GetAxis("Horizontal") * 4f, 0f);
        }

        void OnCollisionEnter(Collision other)
        {
            var meteor = other.gameObject.GetComponent<Meteor>();
            if (meteor == null) return;

            meteor.Effect.OnCollideWithPlayer(this);
        }

        public void OnStartGame()
        {
            Parent.rotation = Quaternion.identity;
        }
        public void OnGameOver()
        {

        }
    }
}