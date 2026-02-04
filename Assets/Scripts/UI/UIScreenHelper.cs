using System.Collections.Generic;
using UnityEngine;

public class UIScreenHelper : Singleton<UIScreenHelper>
{
    Dictionary<Screens, UIScreen> screens = new();

    public void AddScreen(Screens key, UIScreen screen)
    {
        screens.Add(key, screen);
    }

    public UIScreen GetScreen(Screens key)
    {
        return screens[key];
    }

}
