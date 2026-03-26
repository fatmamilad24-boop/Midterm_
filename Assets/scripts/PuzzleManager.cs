using UnityEngine;

public class ControllerFollower : MonoBehaviour
{
    public Transform leftController;
    public Transform rightController;

    // Optional offset if controllers are not aligned perfectly
    public Vector3 leftOffset = Vector3.zero;
    public Vector3 rightOffset = Vector3.zero;

    void LateUpdate()
    {
        if (leftController != null)
            leftController.position = transform.position + leftOffset;

        if (rightController != null)
            rightController.position = transform.position + rightOffset;
    }
}