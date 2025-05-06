using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private bool hasKey = false;

    public void CollectKey()
    {
        hasKey = true;
        Debug.Log("Collected Key");
    }

    public bool HasKey()
    {
        return hasKey;
    }
}
