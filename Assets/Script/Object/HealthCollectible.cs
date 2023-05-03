using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            if(player.health < player.maxHealth)
            {
                player.ChangeHealth(1);
            }
            Destroy(gameObject);
        }
   }
}
