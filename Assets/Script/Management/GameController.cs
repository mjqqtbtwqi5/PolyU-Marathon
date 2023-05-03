using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject UI;
    private float defaultDepth = 10;//10
    public float parallaxDepth = 0;
    public List<string> letters;
    public List<Sprite> letterSprites;

    private Animator entryAnimator;
    
    void Awake()
    {
        entryAnimator = GameObject.Find("Entry").GetComponent<Animator>();
    }

    public void StartGame()
    {
        GameObject.Find("InstructionUI").SetActive(false);
        
        UI.SetActive(true);
        player.SetActive(true);
        parallaxDepth = defaultDepth;
        entryAnimator.SetTrigger("GameStart");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
