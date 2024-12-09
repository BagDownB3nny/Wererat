using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    private Interactable lastInteractable;

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

        GameObject lookingAt = GetLookingAt();
        if (lookingAt != null && lookingAt.GetComponent<Interactable>() != null)
        {
            lastInteractable = lookingAt.GetComponent<Interactable>();
            lastInteractable.Highlight();
        }
        else if (lastInteractable != null)
        {
            lastInteractable.Unhighlight();
        }
    }

    private GameObject GetLookingAt()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5.0f))
        {
            return hit.collider.gameObject;
        }
        return null;
    }
}
