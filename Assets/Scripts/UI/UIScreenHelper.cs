using System.Collections.Generic;

public class UIScreenHelper : Singleton<UIScreenHelper>
{
    private Dictionary<Screens, UIScreen> screens = new();

    public void AddScreen(Screens key, UIScreen screen)
    {
        screens.Add(key, screen);
    }

    public UIScreen GetScreen(Screens key)
    {
        return screens[key];
    }
}
