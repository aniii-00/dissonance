using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyName;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KeyManager km = other.GetComponent<KeyManager>();
            if (km != null)
            {
                km.AddKey(keyName);
                Destroy(gameObject);
            }
        }
    }
}
