using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIScreen))]
public class RevealScreenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        UIScreen miScript = (UIScreen)target;

        if (GUILayout.Button("Toggle Visibility"))
        {
            miScript.ToggleScreenVisibilityEditor();
        }
        if (GUILayout.Button("Show This Screen"))
        {
            GameObject mainMenu = GameObject.Find("UI").gameObject;
            

            for (int i = 0; i < mainMenu.transform.childCount; i++)
            {
                mainMenu.transform.GetChild(i).GetComponent<UIScreen>().HideScreenVisibilityEditor();
            }

            miScript.DisplayScreenVisibilityEditor();
        }
    }
}
