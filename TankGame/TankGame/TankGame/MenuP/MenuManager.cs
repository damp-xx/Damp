namespace TankGame.MenuP
{
    class MenuManager : IMenuManager
    {
        public IMenu chosenMenu { get; private set; }

        public void ShowMenu(IMenu nextMenu)
        {
            if (nextMenu != null)
                chosenMenu = nextMenu;
        }
    }
}
