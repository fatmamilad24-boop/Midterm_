using UnityEngine;

public class SimpleWASDController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public float gravity = -9.81f;

    public Camera playerCamera;

    private CharacterController controller;
    private float verticalVelocity;
    private float rotationX = 0f;

    private GameObject heldObject;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        Look();
        HandleGrab();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (controller.isGrounded)
            verticalVelocity = -1f;

        verticalVelocity += gravity * Time.deltaTime;

        move.y = verticalVelocity;

        controller.Move(move * speed * Time.deltaTime);
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleGrab()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 3f))
                {
                    if (hit.collider.GetComponent<Rigidbody>() != null)
                    {
                        heldObject = hit.collider.gameObject;
                        heldObject.GetComponent<Rigidbody>().useGravity = false;
                        heldObject.transform.SetParent(playerCamera.transform);
                    }
                }
            }
            else
            {
                heldObject.GetComponent<Rigidbody>().useGravity = true;
                heldObject.transform.SetParent(null);
                heldObject = null;
            }
        }
    }
}