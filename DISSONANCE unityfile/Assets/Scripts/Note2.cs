using UnityEngine;

public class Note2 : MonoBehaviour
{
    public GameObject promptUI;
    private bool playerInRange = false;
    public GameObject note;
    private bool pickedup = false;
    public PlayerScript player;
    public DialogueManager dialogueManager;
    public TaskManager taskManager;
    private bool dialogueTriggered = false;

    public FrontDoor frontDoor;

    int pickUpCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (promptUI)
        {
            promptUI.SetActive(false);
        }
        if (note)
        { 
            note.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && !dialogueManager.IsDialogueActive() && Input.GetKeyDown(KeyCode.E))
        {
            ToggleNote();
            pickUpCount++;
        }

        if (pickedup)
        {
            player.enabled = false;
            player.walkingfootsteps.volume = 0;
            player.runningfootsteps.volume = 0;
        } else if (!dialogueManager.IsDialogueActive())
        {
            player.enabled = true;
        }

         if (pickUpCount == 2 && !dialogueTriggered)
        {
            dialogueTriggered = true; // Ensure dialogue doesn't trigger again
            dialogueManager.StartDialogue(new[] { "... I think I've seen enough. Let's get out of here." });
            taskManager.UpdateTask("Get the hell out of there!");
            frontDoor.leaving = true;
        }
    }

    void ToggleNote()
    {
        pickedup = !pickedup;

        if (note)
        {
            note.SetActive(pickedup);
    
        }
        
        if (promptUI)
        {
            promptUI.SetActive(!pickedup);
        }
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            playerInRange = true;

            if (promptUI)
            {
                promptUI.SetActive(true);
            }
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            promptUI.SetActive(false);
        }
    }

    void TrackPickUp()
    {

    }
}
