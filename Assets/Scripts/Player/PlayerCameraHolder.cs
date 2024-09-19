using UnityEngine;

public class PlayerCameraHolder : MonoBehaviour
{
    private static PlayerCameraHolder _instance;
    public static PlayerCameraHolder instance => _instance;

    [SerializeField] private Camera _camera;

    private void Awake()
    {
        _instance = this;
    }

    public bool IsVisibleToPlayer(Vector3 pos) 
    {
        Vector3 viewportPoint = _camera.WorldToViewportPoint(pos);

        bool isVisible = viewportPoint.z > 0 &&
                         viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                         viewportPoint.y >= 0 && viewportPoint.y <= 1;

        return isVisible;
    }
}
