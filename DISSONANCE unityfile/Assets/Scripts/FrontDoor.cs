using UnityEngine;

public class FrontDoor : MonoBehaviour
{
    public GameObject promptUI; 
    public DialogueManager dialogueManager;
    public AudioSource doorLocked;
    public AudioSource gasp;
    public AudioSource scream;
    private bool inRange = false;

    public bool leaving = false;
    public TaskManager taskManager;

    public GameObject gameover;

    private bool dialogueTriggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        promptUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E) && leaving)
        {
            LockedDoor();
        }

        if (inRange && Input.GetKeyDown(KeyCode.E) && !leaving)
        {
            dialogueManager.StartDialogue(new string[] { "I don't want to leave... Not yet." }); 
        }
        
        if (dialogueTriggered == true)
        {
            DemonScream();
        }
       
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entered");
            inRange = true;
            promptUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            promptUI.SetActive(false);
        }
    }

    void DemonScream()
    {
        scream.Play();
        taskManager.UpdateTask("Investigate the scream.");
        dialogueManager.StartDialogue(new[] {"There's nowhere to hide... but I think the scream came from upstairs. I should make sure they're okay... right? "});
    }

    void LockedDoor()
    {
        dialogueManager.StartDialogue(new string[] { "Oh my god... the door is locked." }); 
        doorLocked.Play();
        dialogueTriggered = true;
        gasp.Play();
    }

}
