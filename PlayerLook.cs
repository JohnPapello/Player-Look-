using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float inputSmoothing = 0.5f;
    [SerializeField] private float rotationSmoothing = 0.02f;
    
    private float smoothedCurrentX, smoothedCurrentY; 
    private float smoothedMouseX, smoothedMouseY;
    private float velocityX, velocityY;
    private float currentX, currentY;
    private float mouseX, mouseY;

    private Transform holderTransform;

    private void Awake()
    {
        holderTransform = transform.parent;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        smoothedMouseX = Mathf.Lerp(smoothedMouseX, mouseX, inputSmoothing);
        smoothedMouseY = Mathf.Lerp(smoothedMouseY, mouseY, inputSmoothing);

        currentX += smoothedMouseX;
        currentY += smoothedMouseY;

        currentY = Mathf.Clamp(currentY, -90f, 90f);

        smoothedCurrentX = Mathf.SmoothDamp(smoothedCurrentX, currentX, ref velocityX, rotationSmoothing);
        smoothedCurrentY = Mathf.SmoothDamp(smoothedCurrentY, currentY, ref velocityY, rotationSmoothing);

        transform.localRotation = Quaternion.AngleAxis(-smoothedCurrentY, Vector3.right);
        holderTransform.transform.localRotation = Quaternion.AngleAxis(smoothedCurrentX, Vector3.up);
    }
}

