using System;
using System.Collections;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    [SerializeField] private GameObject visuals;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ParticleSystem destroyParticles;
    [SerializeField] private Animation animationHolder;
    [SerializeField] private string keyDisolveAnimationTitle = "KeyDisolve";
    [SerializeField] private float delayBeforeDestroy;

    public Action OnKeyFinded;

    private void OnTriggerEnter(Collider other)
    {
        visuals.SetActive(false);
        animationHolder.Play(keyDisolveAnimationTitle);
        audioSource.Play();
        destroyParticles.Play();
        OnKeyFinded?.Invoke();
        StartCoroutine(DestroyDelay());
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(delayBeforeDestroy);
        Destroy(gameObject);
    }
}
