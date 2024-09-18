using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    [SerializeField] Vector3 rotation;

    private Transform thisTransform;

    private void Start()
    {
        thisTransform = transform;
    }

    private void Update()
    {
        thisTransform.Rotate(rotation);
    }
}
