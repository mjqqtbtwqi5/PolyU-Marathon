using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDCardCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            GameObject.Find("PropsController").GetComponent<PropsController>().hasStudentID = true;
            Destroy(gameObject);
        }
   }
}
