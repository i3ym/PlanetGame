using System.Collections.Generic;
using UnityEngine;

namespace Planet
{
    public class GameManager : MonoBehaviour
    {
        [HideInInspector] public bool IsAlive = false;

        List<IStartGameCallback> StartGameCallbacks = new List<IStartGameCallback>();
        List<IGameOverCallback> GameOverCallbacks = new List<IGameOverCallback>();

        public void AddCallback(IStartGameCallback callback) => StartGameCallbacks.Add(callback);
        public void AddCallback(IGameOverCallback callback) => GameOverCallbacks.Add(callback);

        public void StartGame()
        {
            IsAlive = true;

            foreach (var callback in StartGameCallbacks)
                callback.OnStartGame();
        }
        public void GameOver()
        {
            IsAlive = false;

            foreach (var callback in GameOverCallbacks)
                callback.OnGameOver();
        }
    }
}