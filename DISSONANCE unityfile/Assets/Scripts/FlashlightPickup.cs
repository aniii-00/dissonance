using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    public GameObject promptUI;
    public GameObject flashlightObject; 
    public GameObject TableLight; 
    public DialogueManager dialogueManager;
    public GameObject flashlightUI;
    public FlashlightToggle flashlightToggleScript;
    public BlinkManager blinkManager;

    private bool playerInRange = false;
    private bool hasPickedUpFlashlight = false;
    public TaskManager taskManager;

    public GameObject livingRoomKey; // reference to key so we can enable it

    void Start()
    {
        promptUI.SetActive(false);
        flashlightUI.SetActive(false);
        livingRoomKey.SetActive(false); // hide key at start
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !hasPickedUpFlashlight)
        {
            EquipFlashlight();
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

    void EquipFlashlight()
    {
        hasPickedUpFlashlight = true;

        flashlightObject.SetActive(true);
        TableLight.SetActive(false);
        promptUI.SetActive(false);

        flashlightUI.SetActive(true);
        flashlightToggleScript.enabled = true;

        Color iconColor = flashlightToggleScript.flashlightIcon.color;
        iconColor.a = 0.3f;
        flashlightToggleScript.flashlightIcon.color = iconColor;

        RevealKeyAndStartDialogue();

        Debug.Log("Flashlight equipped.");
    }

    void RevealKeyAndStartDialogue()
    {
        livingRoomKey.SetActive(true); // Key is now visible & pickable
        blinkManager.TriggerBlink();
        taskManager.UpdateTask("What is this key for?");
        dialogueManager.StartDialogue(new string[]
        {
            "Oh... There's a key. I wonder what this is for."
        });
    }
}
