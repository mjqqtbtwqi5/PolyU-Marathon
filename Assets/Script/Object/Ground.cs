using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float groundHeight;
    private BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (collider.size.y / 2);
    }


}
