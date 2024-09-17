using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;
    public static PlayerController instance => _instance;

    private void Awake()
    {
        _instance = this;
    }
}
