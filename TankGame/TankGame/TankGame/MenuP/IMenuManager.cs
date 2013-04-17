namespace TankGame.MenuP
{
    interface IMenuManager
    {
        IMenu chosenMenu { get; }

        void ShowMenu(IMenu nextMenu);
    }
}
