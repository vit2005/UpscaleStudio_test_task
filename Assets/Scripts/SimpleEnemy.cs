using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{ 
    calm = 0,
    warning = 1,
    hunt = 2,

}


public class SimpleEnemy : MonoBehaviour
{
    [SerializeField] private float warningDistance = 10.0f;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private Light light;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color warningColor = Color.yellow;
    [SerializeField] private Color huntingColor = Color.red;

    private EnemyState currentState = EnemyState.calm;

    // Update is called once per frame
    void Update()
    {
        var playerPosition = PlayerController.instance.transform.position;
        if (Vector3.Distance(playerPosition, transform.position) < warningDistance)
        {
            var direction = playerPosition - transform.position;
            if (Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit, warningDistance) &&
                hit.collider.gameObject == PlayerController.instance.gameObject)
            {
                SetState(EnemyState.hunt);
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
        var main = ps.main;
        switch (state)
        {
            case EnemyState.calm:
                main.startColor = defaultColor;
                light.color = defaultColor;
                break;
            case EnemyState.warning:
                main.startColor = warningColor;
                light.color = warningColor;
                break;
            case EnemyState.hunt:
                main.startColor = huntingColor;
                light.color = huntingColor;
                break;
        }
    }
}
