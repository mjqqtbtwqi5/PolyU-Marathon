using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // public float defaultDepth = 10;
    // public float depth;
    public Player player;
    private GameController gameController;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void FixedUpdate()
    {
        if (gameController.parallaxDepth <= 0)
            return;
        float realVelocity = player.velocity.x / gameController.parallaxDepth;
        Vector2 pos = transform.position;
        pos.x -= realVelocity * Time.fixedDeltaTime;
        transform.position = pos;   
    }
}
