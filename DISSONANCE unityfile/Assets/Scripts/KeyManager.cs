using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> keys = new List<string>();

    public bool HasKey(string keyName)
    {
        return keys.Contains(keyName);
    }

    public void AddKey(string keyName)
    {
        if (!keys.Contains(keyName))
            keys.Add(keyName);
    }
}
