using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Sprite gate;
    public bool specialLogic = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            if (!player.isInvincible)
            {
                if (specialLogic)
                {
                    SpecialLogic(player);
                } else {
                    gameObject.SetActive(false);
                    player.ChangeHealth(-1);
                }
            }
        }
    }

    private void SpecialLogic(Player player)
    {
        if (gameObject.name == "Gate")
        {
            if (!GameObject.Find("PropsController").GetComponent<PropsController>().hasStudentID)
            {
                player.ChangeHealth(-1);
                GameObject.Find("SoundController").GetComponent<SoundController>().PlaySoundGateFail();
            } else {
                if (gate!=null)
                {
                    GameObject.Find("IDCardCollectibles").SetActive(false);
                    gameObject.GetComponent<SpriteRenderer>().sprite = gate;
                    GameObject.Find("SoundController").GetComponent<SoundController>().PlaySoundGatePass();
                }
            }
        }
    }
}
