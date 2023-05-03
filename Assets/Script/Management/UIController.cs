using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Player player;
    private Text scoreText;
    private Text bonusText;
    public Text scoreTextEndPanel;
    private Image heart1;
    private Image heart2;
    private Image heart3;

    public Sprite emptyHeart;
    public Sprite heart;

    private void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        bonusText = GameObject.Find("BonusText").GetComponent<Text>();
        heart1 = GameObject.Find("Heart1").GetComponent<Image>();
        heart2 = GameObject.Find("Heart2").GetComponent<Image>();
        heart3 = GameObject.Find("Heart3").GetComponent<Image>();
    }

    void Update()
    {
        if (player.death)
            return;
        int score = Mathf.FloorToInt(player.distance) * player.bonus;
        bonusText.text = "Bouns X" + player.bonus;
        scoreText.text = score/10 + " m";
        scoreTextEndPanel.text = scoreText.text;
    }

    public void updateHearts()
    {
        switch (player.health)
        {
            case 0:
                heart1.sprite = emptyHeart;
                heart2.sprite = emptyHeart;
                heart3.sprite = emptyHeart;
                break;
            case 1:
                heart1.sprite = heart;
                heart2.sprite = emptyHeart;
                heart3.sprite = emptyHeart;
                break;
            case 2:
                heart1.sprite = heart;
                heart2.sprite = heart;
                heart3.sprite = emptyHeart;
                break;
            case 3:
                heart1.sprite = heart;
                heart2.sprite = heart;
                heart3.sprite = heart;
                break;
        }
    }

}
