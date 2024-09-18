using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private Enemy owner;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip calmSound;
    [SerializeField] private AudioClip warningSound;
    [SerializeField] private AudioClip nuntSound;

    private Dictionary<EnemyState, AudioClip> clips = new Dictionary<EnemyState, AudioClip>();

    public void Awake()
    {
        clips.Add(EnemyState.calm, calmSound);
        clips.Add(EnemyState.warning, warningSound);
        clips.Add(EnemyState.hunt, nuntSound);

        owner.OnStateChanged += PlayNewState;
    }

    private void PlayNewState(EnemyState state)
    {
        audioSource.PlayOneShot(clips[state]);
    }
}
