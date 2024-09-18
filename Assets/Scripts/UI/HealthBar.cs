using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetPercentage(float value)
    {
        image.fillAmount = value;
    }
}
