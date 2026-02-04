using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIScreen))]
public class RevealScreenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Dibuja el inspector por defecto
        DrawDefaultInspector();

        // Referencia al script objetivo
        UIScreen miScript = (UIScreen)target;

        // Añadir el botón
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
