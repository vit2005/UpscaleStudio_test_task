using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    [SerializeField] private GameObject visuals;
    [SerializeField] private AudioSource audio;
    [SerializeField] private ParticleSystem destroyParticles;
    [SerializeField] private Animation animation;
    [SerializeField] private string keyDisolveAnimationTitle = "KeyDisolve";

    public Action OnKeyFinded;

    private void OnTriggerEnter(Collider other)
    {
        visuals.SetActive(false);
        animation.Play(keyDisolveAnimationTitle);
        audio.Play();
        destroyParticles.Play();
        OnKeyFinded?.Invoke();
        //KeyController.instance.UnregisterKey(gameObject);
        StartCoroutine(DestroyDelay());
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
