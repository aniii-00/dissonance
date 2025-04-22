using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    public GameObject promptUI;
    public GameObject flashlightObject; //  attached to the player 
    private bool playerInRange = false;
    public GameObject livingRoomKey;
    public GameObject TableLight; // flashlight on table
    public DialogueManager dialogueManager;
    public GameObject flashlightUI;
    public FlashlightToggle flashlightToggleScript;

    void Start()
    {
        promptUI.SetActive(false);
        livingRoomKey.SetActive(false);
        flashlightUI.SetActive(false);

    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Flashlight Sutff
            flashlightObject.SetActive(true); 
            promptUI.SetActive(false);
            Destroy(TableLight);

            //Flashlight Icon Stuff
            flashlightUI.SetActive(true);
            flashlightToggleScript.enabled = true;
            Color iconColor = flashlightToggleScript.flashlightIcon.color;
            iconColor.a = 0.3f; 
            flashlightToggleScript.flashlightIcon.color = iconColor;

            //Key Stuff
            RevealKey();
            dialogueManager.StartDialogue(new string[] 
            {
            "Oh... There's a key. I wonder what this is for."
            });  
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            promptUI.SetActive(true);
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

    void RevealKey()
    {
        if (livingRoomKey != null)
        {
            livingRoomKey.SetActive(true);
        }

    }
}
