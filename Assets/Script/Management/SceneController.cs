using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject[] scenes;
    public GameObject entryScene;
    
    private static Util util = new Util();
    private GameObject[] activeScenes = new GameObject[2];
    private Vector2 camLeftPos;
    private Vector2 camRightPos;

    private int currentScene = 0;

    private void Awake()
    {
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

        if (sceneRightX <= (camRightPos.x + 0.01f))
        {   /* Enable a next scene */
            GameObject nextScene = GetNextScene();
            nextScene.SetActive(true);
            UpdateScenePosition(nextScene, sceneRightX, camRightPos.y);
            activeScenes[1] = nextScene;
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

    void Update()
    {
        SceneSlideIn();
        SceneSlideOut();
    }

    void Start()
    {
        
    }
}
