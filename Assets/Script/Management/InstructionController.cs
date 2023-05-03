using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionController : MonoBehaviour
{
    public GameObject instructionPanel;
    public Sprite[] images;
    public string[] text;
    private int current = 0;
    private Animator entryAnimator;
    public string animatorInstructionInPara;
    public string animatorInstructionOutPara;

    private AudioSource audioSource;

    void Awake()
    {
        entryAnimator = GameObject.Find("Entry").GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        TutorialPlayer tutorialPlayer = other.GetComponent<TutorialPlayer>();

        if (tutorialPlayer != null)
        {
            instructionPanel.SetActive(true);
            entryAnimator.SetTrigger(animatorInstructionInPara);
            audioSource.Play();
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        TutorialPlayer tutorialPlayer = other.GetComponent<TutorialPlayer>();

        if (tutorialPlayer != null)
        {
            instructionPanel.SetActive(false);
            entryAnimator.SetTrigger(animatorInstructionOutPara);
        }
    }

    public void PreviousItem()
    {
        if (current > 0)
        {
            current -= 1;
            ChangeItem();
        }
    }

    public void NextItem()
    {
        if (current < images.Length - 1)
        {
            current += 1;
            ChangeItem();
        }
    }

    private void ChangeItem()
    {
        Image image = instructionPanel.transform.Find("Image").GetComponent<Image>();
        image.sprite = images[current];
        Text content = instructionPanel.transform.Find("Content").GetComponent<Text>();
        content.text = text[current];
    }
}
