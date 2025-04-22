using UnityEngine;

public class FrontDoor : MonoBehaviour
{
    public GameObject promptUI; 
    public DialogueManager dialogueManager;
    public AudioSource doorLocked;
    private bool inRange = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        promptUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            doorLocked.Play();
            dialogueManager.StartDialogue(new string[] { "I don't want to leave... Not yet." });   
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



}
