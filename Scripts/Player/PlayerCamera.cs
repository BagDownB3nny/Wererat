using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    [SerializeField] private Transform orientation;

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * 100.0f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 100.0f;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
