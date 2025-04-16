using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public HashSet<string> keys = new HashSet<string>();

    public void AddKey(string keyName)
    {
        keys.Add(keyName);
    }

    public bool HasKey(string keyName)
    {
        return keys.Contains(keyName);
    }
}
