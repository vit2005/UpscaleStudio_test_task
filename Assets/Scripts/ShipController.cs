using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    public float acceleration = 5f; // ����������� �������
    public float maxSpeed = 20f; // ����������� ��������
    public float deceleration = 2f; // ����������� ��� ��������� ��������

    private Rigidbody rb; // ��������� Rigidbody
    private Vector3 currentVelocity; // ������� �������� �������

    void Start()
    {
        // ��������� ���������� Rigidbody
        rb = GetComponent<Rigidbody>();

        // ��������� ������� � ����� ������
        Cursor.lockState = CursorLockMode.Locked;

        // ��������� ������
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        // �������� �����������
        float inputVertical = Input.GetAxis("Vertical"); // W,S - ������, �����
        float inputHorizontal = Input.GetAxis("Horizontal"); // A,D - ����, ������
        bool inputJump = Input.GetKey(KeyCode.Space);
        bool inputCrouch = Input.GetKey(KeyCode.LeftControl);
        bool speedUpInput = Input.GetKey(KeyCode.LeftShift);
        float inputUp = inputJump && inputCrouch ? 0 : inputJump ? 1 : inputCrouch ? -1 : 0;
        float speedUp = speedUpInput ? 2f : 1f;

        // ���������� �������� ����
        Vector3 forwardMovement = transform.forward * inputVertical;
        Vector3 strafeMovement = transform.right * inputHorizontal;
        Vector3 verticalMovement = transform.up * inputUp;
        Vector3 movementDirection = (forwardMovement + strafeMovement + verticalMovement).normalized;

        Vector3 desiredVelocity = movementDirection * maxSpeed * speedUp;

        // ������������ �������� �� Rigidbody
        if (movementDirection != Vector3.zero)
        {
            // ���������� ���� ��� �����������
            Vector3 force = (desiredVelocity - rb.velocity).normalized * acceleration;
            rb.AddForce(force, ForceMode.Acceleration);
        }
        else
        {
            // �����������
            if (rb.velocity.magnitude > 0)
            {
                Vector3 brakingForceVector = -rb.velocity * deceleration;
                rb.AddForce(brakingForceVector);
            }
        }
    }
}
