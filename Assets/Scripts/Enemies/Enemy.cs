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
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color warningColor;
    [SerializeField] private Color huntingColor;

    [SerializeField] private float damageValue;

    public Action<EnemyState> OnStateChanged;

    private EnemyState currentState = EnemyState.calm;
    private Dictionary<EnemyState, Color> statesColors = new Dictionary<EnemyState, Color>();

    private void Awake()
    {
        statesColors.Add(EnemyState.calm, defaultColor);
        statesColors.Add(EnemyState.warning, warningColor);
        statesColors.Add(EnemyState.hunt, huntingColor);
    }

    private void Update()
    {
        if (GameController.instance.Paused) return;

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
        main.startColor = statesColors[state];
        lightSource.color = statesColors[state];
    }

    protected void OnTriggerStay(Collider other)
    {
        var healthController = other.gameObject.GetComponent<HealthController>();
        if (healthController == null) return;

        healthController.Damage(damageValue);
    }
}
