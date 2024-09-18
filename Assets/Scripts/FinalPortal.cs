using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPortal : MonoBehaviour
{
    public Action OnFinalPortal;

    private void OnTriggerEnter(Collider other)
    {
        OnFinalPortal?.Invoke();
    }
}
