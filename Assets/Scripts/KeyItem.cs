using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        KeyController.instance.UnregisterKey(gameObject);
        Destroy(gameObject);
    }
}
