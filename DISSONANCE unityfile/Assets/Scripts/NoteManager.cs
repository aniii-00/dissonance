using UnityEngine;
using UnityEngine.UI;



public class NoteManager : MonoBehaviour
{
    public FlashlightPickup flashlightPickup;
    public KeyPickup keyPickup;
    public GameObject note;
    public GameObject physicalnote;
    public GameObject promptUI;
    private bool playerInRange = false;
    private bool pickedup = false;
    private bool isNoteUnlocked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        note.SetActive(false);
        promptUI.SetActive(false);
        physicalnote.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && isNoteUnlocked)
        {
            ToggleNote();
        }

    
        if (!isNoteUnlocked && keyPickup.hasPickedUpKey && flashlightPickup.hasPickedUpFlashlight)
        {
            physicalnote.SetActive(true);
            isNoteUnlocked = true;
            
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

            if (isNoteUnlocked && promptUI)
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
}
