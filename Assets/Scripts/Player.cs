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

            Parent.Rotate(Speed, Input.GetAxis("Horizontal") * 4f, 0f);
        }

        void OnTriggerEnter(Collider other)
        {
            var meteor = other.GetComponent<Meteor>();
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