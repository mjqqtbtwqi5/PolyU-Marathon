using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    public GameObject idCardCollectibleUI;
    public int probability;
    public string letter;
    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            if (gameObject.tag == "HealthCollectible")
            {
                if(player.health < player.maxHealth)
                {
                    player.ChangeHealth(1);
                    gameObject.SetActive(false);
                }
            } else if (gameObject.tag == "LetterCollectible")
            {
                GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
                
                int index = gameController.letters.IndexOf(letter);
                if (index>=0)
                {
                    Text letterText = GameObject.Find(letter).GetComponent<Text>();
                    Color color = letterText.color;
                    color.a = 1;
                    letterText.color = color;

                    gameController.letters.RemoveAt(index);
                    gameController.letterSprites.RemoveAt(index);

                    if(gameController.letters.Count <= 0)
                    {
                        player.bonus *= player.doubleBonus;
                    }
                }
                gameObject.SetActive(false);
            } else if (gameObject.tag == "IDCardCollectible")
            {
                GameObject.Find("PropsController").GetComponent<PropsController>().hasStudentID = true;
                Destroy(gameObject);
                idCardCollectibleUI.SetActive(true);
            }
        }
   }
}
