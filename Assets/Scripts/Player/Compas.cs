using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compas : MonoBehaviour
{
    [SerializeField] GameObject particles;

    private GameObject _target;
    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    // Update is called once per frame
    void Update()
    {
        var nearest = KeyController.instance.GetNearestKey(transform.position);
        if (nearest == null) nearest = _target;
        bool any = nearest != null;
        particles.SetActive(any);
        if (any)
        {
            transform.LookAt(nearest.transform);
        }
    }
}
