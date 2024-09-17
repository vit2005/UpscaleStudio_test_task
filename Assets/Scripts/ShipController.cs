using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    public float acceleration = 5f; // Прискорення корабля
    public float maxSpeed = 20f; // Максимальна швидкість
    public float deceleration = 2f; // Сповільнення при відсутності введення

    private Rigidbody rb; // Компонент Rigidbody
    private Vector3 currentVelocity; // Поточна швидкість корабля

    void Start()
    {
        // Отримання компонента Rigidbody
        rb = GetComponent<Rigidbody>();

        // Локування курсора в центрі екрана
        Cursor.lockState = CursorLockMode.Locked;

        // Приховати курсор
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        // Введення користувача
        float inputVertical = Input.GetAxis("Vertical"); // W,S - вперед, назад
        float inputHorizontal = Input.GetAxis("Horizontal"); // A,D - вліво, вправо
        bool inputJump = Input.GetKey(KeyCode.Space);
        bool inputCrouch = Input.GetKey(KeyCode.LeftControl);
        bool speedUpInput = Input.GetKey(KeyCode.LeftShift);
        float inputUp = inputJump && inputCrouch ? 0 : inputJump ? 1 : inputCrouch ? -1 : 0;
        float speedUp = speedUpInput ? 2f : 1f;

        // Розрахунок напрямку руху
        Vector3 forwardMovement = transform.forward * inputVertical;
        Vector3 strafeMovement = transform.right * inputHorizontal;
        Vector3 verticalMovement = transform.up * inputUp;
        Vector3 movementDirection = (forwardMovement + strafeMovement + verticalMovement).normalized;

        Vector3 desiredVelocity = movementDirection * maxSpeed * speedUp;

        // Застосування швидкості до Rigidbody
        if (movementDirection != Vector3.zero)
        {
            // Розрахунок сили для прискорення
            Vector3 force = (desiredVelocity - rb.velocity).normalized * acceleration;
            rb.AddForce(force, ForceMode.Acceleration);
        }
        else
        {
            // Гальмування
            if (rb.velocity.magnitude > 0)
            {
                Vector3 brakingForceVector = -rb.velocity * deceleration;
                rb.AddForce(brakingForceVector);
            }
        }
    }
}
