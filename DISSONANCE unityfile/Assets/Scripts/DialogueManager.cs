using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI; 
    public TMP_Text dialogueText; 
    public string[] dialogueLines; 
    private int currentLine = 0;

    public bool isDialogueActive = false;

    public PlayerScript player; 
    public BlinkManager blinkManager; 

    void Start()
    {
        dialogueUI.SetActive(false);
        Invoke("StartOpeningDialogue", 1f);
        
        
    
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.R))
        {
            AdvanceDialogue();
        }
    }

    public void StartDialogue(string[] lines)
    {
        dialogueLines = lines;
        currentLine = 0;
        isDialogueActive = true;

        dialogueUI.SetActive(true);
        dialogueText.text = dialogueLines[currentLine];

        
        player.enabled = false;
        blinkManager.enabled = false;
        player.walkingfootsteps.volume = 0;
        player.runningfootsteps.volume = 0;
    }

    void AdvanceDialogue()
    {
        currentLine++;
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialogueUI.SetActive(false);

        
        player.enabled = true;
        blinkManager.enabled = true;
    }
    void StartOpeningDialogue()
    {
        StartDialogue(new string[] {
            "This house feels so familiar... I haven't been here in years."
        });
    }
}
