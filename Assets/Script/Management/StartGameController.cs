using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        TutorialPlayer tutorialPlayer = other.GetComponent<TutorialPlayer>();

        if (tutorialPlayer != null)
        {
            tutorialPlayer.gameObject.SetActive(false);
            GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
            gameController.StartGame();
        }
    }
}
