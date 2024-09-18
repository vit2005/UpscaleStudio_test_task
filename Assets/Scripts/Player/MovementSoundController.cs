using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSoundController : MonoBehaviour
{
    [SerializeField] private MovementController movementController;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip startMovement;
    [SerializeField] private AudioClip stopMovement;

    public void Awake()
    {
        movementController.OnMoving += OnMoving;
    }

    private void OnMoving(bool value)
    {
        audioSource.PlayOneShot(value ? startMovement : stopMovement);
    }
}
