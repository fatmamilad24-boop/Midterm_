using UnityEngine;

public class KeyboardMove : MonoBehaviour
{
    public float speed = 2.5f;
    public float rotSpeed = 80f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * v + transform.right * h;
        transform.position += move * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(0, -rotSpeed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.E))
            transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
    }
}