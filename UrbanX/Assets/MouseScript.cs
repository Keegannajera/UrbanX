using UnityEngine;


public class MouseScript : MonoBehaviour
{
    public static MouseScript Instance { get { return instance; } }
    private static MouseScript instance;
    public float mouseSensitivity = 200f;

    public Transform playerBody;

    float xRotation = 0f;
    public PlayerMovement playerMovemnt;


    void Start()
    {
        playerMovemnt = playerBody.GetComponent<PlayerMovement>();
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!playerMovemnt.controllable) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerMovemnt.controllable = true;
    }
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        playerMovemnt.controllable = false;
    }

}

