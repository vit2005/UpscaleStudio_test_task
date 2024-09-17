using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compas : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var nearest = KeyController.instance.GetNearestKey(transform.position);
        if (nearest != null)
        {
            transform.LookAt(nearest.transform);
        }
    }
}
