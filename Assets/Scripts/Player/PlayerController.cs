using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;
    public static PlayerController instance => _instance;

    [SerializeField] private Camera camera;

    private void Awake()
    {
        _instance = this;
    }

    public bool IsVisibleToPlayer(Vector3 pos)
    {
        // Convert the point from world space to viewport space
        Vector3 viewportPoint = camera.WorldToViewportPoint(pos);

        // Check if the point is in front of the camera and within the viewport bounds
        bool isVisible = viewportPoint.z > 0 &&
                         viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                         viewportPoint.y >= 0 && viewportPoint.y <= 1;

        return isVisible;
    }
}
