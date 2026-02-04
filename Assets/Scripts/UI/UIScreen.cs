using System;
using System.Collections.Generic;
using UnityEngine;

public enum Screens { MainMenu, Options, GameModeSelector, Game, GameOptions, EndGame }
public class UIScreen : MonoBehaviour
{
    [SerializeField] Screens thisScreenKey;

    private List<GameObject> elements = new();
    private Dictionary<Screens, UIScreen> screenConections = new();

    [SerializeField] private List<Screens> screenKeys;
    [SerializeField] private List<UIScreen> screens;
    
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
        if (screenKeys.Count != screens.Count) { Debug.LogWarning("Different Amount of ScreenKeys and Screens"); }

        for (int i = 0; i < screenKeys.Count; i++)
        {
            screenConections.Add(screenKeys[i], screens[i]);
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
