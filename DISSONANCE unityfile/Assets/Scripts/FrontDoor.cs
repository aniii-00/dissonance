using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

    

    private bool dialogueTriggered = false;
    public GameObject gameOverUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        promptUI.SetActive(false);
        gameOverUI.SetActive(false);
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
            StartCoroutine(DelayedDemonScream());
            dialogueTriggered = false;
            Debug.Log("triggered");
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

    IEnumerator DelayedDemonScream()
    {
        yield return new WaitForSeconds(2f); 

        gasp.Play();
        scream.Play();
        taskManager.UpdateTask("Investigate the scream.");
        dialogueManager.StartDialogue(new[] {
            "There's nowhere to hide... but I think the scream came from upstairs. I should make sure they're okay... right?"
        });

        yield return new WaitForSeconds(5f);

        gameOverUI.SetActive(true); 

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("main menu");
    }

    void LockedDoor()
    {
        dialogueManager.StartDialogue(new string[] { "Oh my god... the door is locked." }); 
        doorLocked.Play();
        dialogueTriggered = true;
        
    }

}
