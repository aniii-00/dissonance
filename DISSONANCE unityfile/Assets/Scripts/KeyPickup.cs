using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyName;
    public GameObject PickupUI;
    private bool playerInRange = false;
    private KeyManager keyManager;

    void Start()
    {
        keyManager = GameObject.FindWithTag("Player").GetComponent<KeyManager>();
        PickupUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            keyManager.AddKey(keyName);
            PickupUI.SetActive(false);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            PickupUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            PickupUI.SetActive(false);
        }
    }
}
