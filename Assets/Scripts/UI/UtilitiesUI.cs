using UnityEngine;

public class UtilitiesUI
{
    public static T GetComponentByName<T>(GameObject origin, string componentName)
    {
        return origin.transform.Find(componentName).GetComponent<T>();
    }
}
