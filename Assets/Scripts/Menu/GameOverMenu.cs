using UnityEngine;
using UnityEngine.UI;

namespace Planet.Menu
{
    public class GameOverMenu : Menu
    {
        [SerializeField] Button RestartGameButton = null;
        [SerializeField] Button ExitToMenuButton = null;

        void Start()
        {
            RestartGameButton.onClick.AddListener(() => RestartGame());
            ExitToMenuButton.onClick.AddListener(() => ExitGame());
        }

        void RestartGame()
        {
            MenuManager.GameManager.StartGame();
        }
        void ExitGame() => MenuManager.SetMenu(MenuManager.MainMenu);
    }
}