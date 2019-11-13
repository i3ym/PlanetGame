using UnityEngine;
using UnityEngine.UI;

namespace Planet.Menu
{
    public class MainMenu : Menu
    {
        [SerializeField] Button StartGameButton = null;
        [SerializeField] Button QuitGameButton = null;

        void Start()
        {
            StartGameButton.onClick.AddListener(() => StartGame());
            QuitGameButton.onClick.AddListener(() => QuitGame());
        }

        void StartGame()
        {
            (MenuManager as IHasGameManager).GameManager.StartGame();
        }
        void QuitGame() => Application.Quit();
    }
}