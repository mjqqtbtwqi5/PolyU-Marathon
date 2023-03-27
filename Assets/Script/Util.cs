using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public Vector2 GetCameraPosition(Vector2 vector2)
    {
        return Camera.main.ScreenToWorldPoint(vector2);
    }

    public RectTransform GetRectTransform(GameObject gameObject)
    {
        return (RectTransform)gameObject.transform;
    }
}