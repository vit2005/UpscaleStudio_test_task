using UnityEngine;

/// <summary>
/// A simple FPP (First Person Perspective) camera rotation script.
/// Like those found in most FPS (First Person Shooter) games.
/// </summary>
public class FirstPersonCameraRotation : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    [SerializeField] private float yRotationLimit;

    private Vector2 rotation = Vector2.zero;
    private Quaternion _lastCalculatedRotation = Quaternion.identity;

    private const string xAxis = "Mouse X";
    private const string yAxis = "Mouse Y";


    void Update()
    {
        if (GameController.instance.Paused)
        {
            transform.localRotation = _lastCalculatedRotation;
            return;
        }

        rotation.x += Input.GetAxis(xAxis) * sensitivity;
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);

        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);
        _lastCalculatedRotation = xQuat * yQuat; //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
        transform.localRotation = _lastCalculatedRotation;
    }
}