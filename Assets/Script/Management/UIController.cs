using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Player player;
    private Text scoreText;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    void Start()
    {

    }

    void Update()
    {
        int score = Mathf.FloorToInt(player.distance);
        scoreText.text = score/10 + " m";
    }
}
