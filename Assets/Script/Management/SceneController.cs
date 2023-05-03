using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject[] scenes;
    public GameObject entryScene;
    
    private static Util util = new Util();
    public GameObject[] activeScenes = new GameObject[2];
    private Vector2 camLeftPos;
    private Vector2 camRightPos;

    private int currentScene = 0;

    private GameController gameController;

    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        camLeftPos = util.GetCameraPosition(new Vector2(0, (Camera.main.pixelHeight / 2)));
        camRightPos = util.GetCameraPosition(new Vector2(Camera.main.pixelWidth, (Camera.main.pixelHeight / 2)));
        
        activeScenes[0] = entryScene;
        UpdateScenePosition(activeScenes[0], camLeftPos.x, camLeftPos.y);
    }

    private void UpdateScenePosition(GameObject scene, float x, float y)
    {
        scene.transform.position = new Vector2(x, y);
    }

    private GameObject GetNextScene()
    {
        GameObject scene = scenes[currentScene%scenes.Length];
        currentScene++;
        return scene;
    }

    private void SceneSlideIn()
    {
        float sceneWidth;
        float scenePosX;
        GameObject scene;

        if (activeScenes[1] == null)
        {
            scene = activeScenes[0];
        } else
        {
            scene = activeScenes[1];
        }

        RectTransform rt = util.GetRectTransform(scene);
        sceneWidth = rt.rect.width;
        scenePosX = scene.transform.position.x;
        float sceneRightX = (sceneWidth - Mathf.Abs(scenePosX));

        if (sceneRightX <= (camRightPos.x))
        {   /* Enable a next scene */
            GameObject nextScene = GetNextScene();
            nextScene.SetActive(true);
            UpdateScenePosition(nextScene, sceneRightX, camRightPos.y);
            activeScenes[1] = nextScene;
            EnableObstacles(nextScene);
        }
    }

    private void SceneSlideOut()
    {
        RectTransform rt = (RectTransform)activeScenes[0].transform;
        float sceneWidth = rt.rect.width;
        float scenePosX = activeScenes[0].transform.position.x;
        float sceneRightX = (sceneWidth - Mathf.Abs(scenePosX));

        if (sceneRightX <= camLeftPos.x)
        {   /* Disable the scene slided out */
            activeScenes[0].SetActive(false);
            activeScenes[0] = activeScenes[1];
        }
    }

    private void EnableObstacles(GameObject parent)
    {
        foreach(Transform t in parent.transform)
        {
            if(t.gameObject.tag == "ObstacleRespawn")
            {
                for (int i = 0; i < t.childCount; i++)
                {
                    t.GetChild(i).gameObject.SetActive(false);
                }
                int randomObstacle = Random.Range(0, t.childCount);
                t.GetChild(randomObstacle).gameObject.SetActive(true);
            }
            if(t.gameObject.tag == "CollectibleRespawn")
            {
                for (int i = 0; i < t.childCount; i++)
                {
                    t.GetChild(i).gameObject.SetActive(false);
                }
                int randomCollectible = Random.Range(0, t.childCount);
                GameObject child = t.GetChild(randomCollectible).gameObject;
                int probability = child.GetComponent<Collectible>().probability;
                bool active = Random.Range(1, 11) <= probability;
                if(active)
                {
                    if(child.tag == "HealthCollectible")
                    {
                        child.SetActive(true);
                    }else if(child.tag == "LetterCollectible")
                    {
                        if(gameController.letters.Count > 0)
                        {
                            int randomLetter = Random.Range(0, gameController.letters.Count);

                            string letter = gameController.letters[randomLetter];
                            Sprite letterSprite = gameController.letterSprites[randomLetter];

                            // gameController.letters.RemoveAt(randomLetter);
                            // gameController.letterSprites.RemoveAt(randomLetter);

                            child.GetComponent<SpriteRenderer>().sprite = letterSprite;
                            child.GetComponent<Collectible>().letter = letter;

                            child.SetActive(true);
                        }
                    }
                }
            }
            
        }
    }

    void Update()
    {
        SceneSlideIn();
        SceneSlideOut();
    }
}
