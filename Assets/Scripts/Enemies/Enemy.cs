using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.CullingGroup;

public enum EnemyState
{ 
    calm = 0,
    warning = 1,
    hunt = 2,

}


public class Enemy : MonoBehaviour
{
    [SerializeField] private float warningDistance = 10.0f;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private Light lightSource;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color warningColor = Color.yellow;
    [SerializeField] private Color huntingColor = Color.red;

    [SerializeField] private float damageValue;

    public Action<EnemyState> OnStateChanged;

    private EnemyState currentState = EnemyState.calm;

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.paused) return;

        var playerPosition = PlayerCameraHolder.instance.transform.position;
        if (Vector3.Distance(playerPosition, transform.position) < warningDistance)
        {
            var direction = playerPosition - transform.position;
            if (Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit, warningDistance) &&
                hit.collider.gameObject == PlayerCameraHolder.instance.gameObject)
            {
                SetState(EnemyState.hunt);
                float speed = PlayerCameraHolder.instance.IsVisibleToPlayer(transform.position) ? 2f : 1f;
                transform.position += (direction.normalized) * speed * 0.01f;
            }
            else
            {
                SetState(EnemyState.warning);
            }
        }
        else
        {
            SetState(EnemyState.calm);
        }
    }

    private void SetState(EnemyState state)
    {
        if (state == currentState) return;

        currentState = state;
        OnStateChanged?.Invoke(currentState);

        var main = ps.main;
        switch (state)
        {
            case EnemyState.calm:
                main.startColor = defaultColor;
                lightSource.color = defaultColor;
                break;
            case EnemyState.warning:
                main.startColor = warningColor;
                lightSource.color = warningColor;
                break;
            case EnemyState.hunt:
                main.startColor = huntingColor;
                lightSource.color = huntingColor;
                break;
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        var healthController = other.gameObject.GetComponent<HealthController>();
        if (healthController == null) return;

        healthController.Damage(damageValue);
    }
}
