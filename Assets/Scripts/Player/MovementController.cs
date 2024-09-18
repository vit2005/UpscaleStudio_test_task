using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    public float acceleration = 5f; // ����������� �������
    public float maxSpeed = 20f; // ����������� ��������
    public float deceleration = 2f; // ����������� ��� ��������� ��������

    private Rigidbody rb;
    private Vector3 _velocity;
    private bool _isMoving;

    public Action<bool> OnMoving;

    public void Init()
    {
        // ��������� ���������� Rigidbody
        rb = GetComponent<Rigidbody>();

        // ��������� ������� � ����� ������
        Cursor.lockState = CursorLockMode.Locked;

        // ��������� ������
        Cursor.visible = false;
    }

    public void Pause(bool value)
    {
        rb.velocity = value ? _velocity : Vector3.zero;
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

        bool isMoving = !Mathf.Approximately(inputVertical, 0f) || !Mathf.Approximately(inputVertical, 0f) || inputJump || inputCrouch;
        if ((isMoving && !_isMoving) || (!isMoving && _isMoving)) OnMoving?.Invoke(isMoving);
        _isMoving = isMoving;

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
        _velocity = rb.velocity;
    }
}
