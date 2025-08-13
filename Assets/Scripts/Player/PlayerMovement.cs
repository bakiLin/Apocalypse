using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [Inject]
    private VariableJoystick joystick;

    [SerializeField]
    private float speed;

    private Rigidbody rb;

    private Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        direction = transform.forward * joystick.Direction.y + transform.right * joystick.Direction.x;
        direction.Normalize();
        direction.Set(direction.x * speed, rb.velocity.y, direction.z * speed);
    }

    private void FixedUpdate()
    {
        rb.velocity = direction;
    }
}
