using UnityEngine;

namespace Planet.Menu
{
    public class MenuManager : MonoBehaviour, IStartGameCallback, IGameOverCallback
    {
        public GameManager GameManager => _GameManager;
        public Menu MainMenu => _MainMenu;
        public Menu GameOverMenu => _GameOverMenu;

        [SerializeField] GameManager _GameManager = null;
        [SerializeField] MainMenu _MainMenu = null;
        [SerializeField] GameOverMenu _GameOverMenu = null;

        Menu[] Menus = null;

        void Start()
        {
            Menus = new Menu[] { MainMenu, GameOverMenu };

            GameManager.AddCallback(this as IStartGameCallback);
            GameManager.AddCallback(this as IGameOverCallback);

            SetMenu(MainMenu);
        }

        public void SetMenu(Menu menu)
        {
            foreach (var m in Menus)
                m.gameObject.SetActive(false);

            if (menu != null)
                menu.gameObject.SetActive(true);
        }

        public void OnStartGame()
        {
            SetMenu(null);
        }
        public void OnGameOver()
        {
            SetMenu(_GameOverMenu);
        }
    }
}