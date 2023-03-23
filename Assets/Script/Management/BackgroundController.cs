using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public List<GameObject> backgrounds;
    public GameObject entryBackground;
    
    private static Util util = new Util();
    private GameObject[] activeBackground = new GameObject[2];
    private Vector2 camLeftPos;
    private Vector2 camRightPos;


    private void Awake()
    {
        camLeftPos = util.GetCameraPosition(new Vector2(0, (Camera.main.pixelHeight / 2)));
        camRightPos = util.GetCameraPosition(new Vector2(Camera.main.pixelWidth, (Camera.main.pixelHeight / 2)));

        activeBackground[0] = entryBackground;
        UpdateBackgourndPosition(activeBackground[0], camLeftPos.x, camLeftPos.y);
    }

    private void UpdateBackgourndPosition(GameObject background, float x, float y)
    {
        background.transform.position = new Vector2(x, y);
    }

    private GameObject GetRandomBackground()
    {
        int randomNum = Random.Range(0, backgrounds.Count);
        GameObject background = backgrounds[randomNum];
        backgrounds.Remove(background);
        return background;
    }

    private void IsBackgroundSlideIn()
    {
        float backgroundWidth;
        float backgroundPosX;

        if (activeBackground[1] == null)
        {
            RectTransform rt = (RectTransform)activeBackground[0].transform;
            backgroundWidth = rt.rect.width;
            backgroundPosX = activeBackground[0].transform.position.x;
        } else
        {
            RectTransform rt = (RectTransform)activeBackground[1].transform;
            backgroundWidth = rt.rect.width;
            backgroundPosX = activeBackground[1].transform.position.x;
        }

        float backgroundRightX = (backgroundWidth - Mathf.Abs(backgroundPosX));
        if (backgroundRightX <= (camRightPos.x + 0.01f))
        {
            GameObject background = GetRandomBackground();
            background.SetActive(true);
            UpdateBackgourndPosition(background, backgroundRightX, camRightPos.y);
            activeBackground[1] = background;
        }
    }

    private void IsBackgroundSlideOut()
    {
        RectTransform rt = (RectTransform)activeBackground[0].transform;
        float backgroundWidth = rt.rect.width;
        float backgroundPosX = activeBackground[0].transform.position.x;
        float backgroundRightX = (backgroundWidth - Mathf.Abs(backgroundPosX));

        if (backgroundRightX <= camLeftPos.x)
        {
            GameObject background = activeBackground[0];
            background.SetActive(false);
            if (background != entryBackground)
            {
                backgrounds.Add(background);
            }
            activeBackground[0] = activeBackground[1];
        }
    }

    void Update()
    {
        IsBackgroundSlideIn();
        IsBackgroundSlideOut();
    }

    void Start()
    {
        
    }
}
