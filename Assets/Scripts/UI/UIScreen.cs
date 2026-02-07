using System;
using System.Collections.Generic;
using UnityEngine;

public enum Screens { MainMenu, Options, GameModeSelector, Game, GameOptions, EndGame, Achievements }
public class UIScreen : MonoBehaviour
{
    [SerializeField] Screens thisScreenKey;

    private List<GameObject> elements = new();

    [SerializeField] private List<UIScreenInfo> uiScreensInfoList;
    private Dictionary<Screens, UIScreen> screenConections = new();
    
    void Awake()
    {
        UIScreenHelper.Instance.AddScreen(thisScreenKey, this);
        GetAllElements();
        SetScreenConnections();
    }
    private void GetAllElements()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            elements.Add(transform.GetChild(i).gameObject);
        }
    }
    private void SetScreenConnections()
    {
        for (int i = 0; i < uiScreensInfoList.Count; i++)
        {
            screenConections.Add(uiScreensInfoList[i].screenKey, uiScreensInfoList[i].screen);
        }
    }

    public void ChangeScreens(Screens screenKey)
    {
        if (!screenConections.ContainsKey(screenKey)) { Debug.LogError("ScreenKey not in ScreenConnections"); return; }

        screenConections[screenKey].DisplayScreen();
        HideScreen();
    }

    public void DisplayScreen()
    {
        elements.ForEach(e => 
        {
            e.SetActive(true);
        });
        GetComponent<BaseScreen>().OnGameObjectEnabled();
        
    }
    public void HideScreen()
    {
        GetComponent<BaseScreen>().OnGameObjectDisabled();
        elements.ForEach(e => 
        { 
            e.SetActive(false); 
        });
    }

    public void SetScreenVisibility(bool state)
    {
        elements.ForEach(e =>
        {
            e.SetActive(state);
        });
    }

    // Editor
    public void ToggleScreenVisibilityEditor()
    {
        List<GameObject> childElements = new();

        for (int i = 0; i < transform.childCount; i++)
        {
            childElements.Add(transform.GetChild(i).gameObject);
        }

        childElements.ForEach(e => { e.SetActive(!e.activeSelf); });
    }

    public void DisplayScreenVisibilityEditor()
    {
        List<GameObject> childElements = new();

        for (int i = 0; i < transform.childCount; i++)
        {
            childElements.Add(transform.GetChild(i).gameObject);
        }

        childElements.ForEach(e => { e.SetActive(true); });
    }
    public void HideScreenVisibilityEditor()
    {
        List<GameObject> childElements = new();

        for (int i = 0; i < transform.childCount; i++)
        {
            childElements.Add(transform.GetChild(i).gameObject);
        }

        childElements.ForEach(e => { e.SetActive(false); });
    }
}

[Serializable]
public struct UIScreenInfo
{
    public Screens screenKey;
    public UIScreen screen;
}
