using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    public TMP_Text dialogueText;
    private string[] dialogueLines;
    private int currentLine = 0;
    private bool isActive = false;
    private bool canAdvance = false;
    private Coroutine autoAdvanceCoroutine;

    public PlayerScript player;
    public BlinkManager blinkManager;

    void Start()
    {
        dialogueUI.SetActive(false);
        Invoke("StartOpeningDialogue", 1f);
    }

    void Update()
    {
        if (isActive && canAdvance && Input.GetKeyDown(KeyCode.R))
        {
            SkipToNextLine();
        }
    }

    public void StartDialogue(string[] lines)
    {
        dialogueLines = lines;
        currentLine = 0;
        isActive = true;

        dialogueUI.SetActive(true);
        dialogueText.text = dialogueLines[currentLine];

        player.enabled = false;
        blinkManager.enabled = false;
        player.walkingfootsteps.volume = 0;
        player.runningfootsteps.volume = 0;

        if (autoAdvanceCoroutine != null)
            StopCoroutine(autoAdvanceCoroutine);
        autoAdvanceCoroutine = StartCoroutine(AutoAdvanceDialogue());
    }

    IEnumerator AutoAdvanceDialogue()
    {
        while (isActive)
        {
            canAdvance = true;
            yield return new WaitForSeconds(10f); // Delay before auto-advancing

            if (isActive) // still valid after delay
                AdvanceDialogue();
        }
    }

    void SkipToNextLine()
    {
        if (isActive)
        {
            AdvanceDialogue();
        }
    }

    void AdvanceDialogue()
    {
        canAdvance = false;
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
        isActive = false;
        canAdvance = false;
        dialogueUI.SetActive(false);

        player.enabled = true;
        blinkManager.enabled = true;
    }

    public bool IsDialogueActive()
    {
        return isActive;
    }

    void StartOpeningDialogue()
    {
        StartDialogue(new string[] {
            "This house feels so familiar... I haven't been here in years."
        });
    }
}
