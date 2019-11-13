using UnityEngine;

namespace Planet
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour, IStartGameCallback, IGameOverCallback
    {
        [SerializeField] GameManager GameManager = null;
        Transform Parent;

        void Start()
        {
            Parent = transform.parent;

            GameManager.AddCallback(this as IStartGameCallback);
            GameManager.AddCallback(this as IGameOverCallback);
        }
        void FixedUpdate()
        {
            if (!GameManager.IsAlive) return;

            Parent.Rotate(1f + Input.GetAxis("Vertical") / 2f, Input.GetAxis("Horizontal") * 4f, 0f);
        }

        void OnCollisionEnter(Collision other) => GameManager.GameOver();

        public void OnStartGame()
        {
            Parent.rotation = Quaternion.identity;
        }
        public void OnGameOver()
        {

        }
    }
}