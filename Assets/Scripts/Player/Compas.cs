using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compas : MonoBehaviour
{
    [SerializeField] GameObject particles;

    // Update is called once per frame
    void Update()
    {
        var nearest = KeyController.instance.GetNearestKey(transform.position);
        bool any = nearest != null;
        particles.SetActive(any);
        if (any)
        {
            transform.LookAt(nearest.transform);
        }
    }
}
