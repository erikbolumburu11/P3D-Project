using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] float sensitivity = 5;
    float xRot, yRot;

    [Header("References")]
    [SerializeField] InputActionReference lookDirInput;
    [SerializeField] Transform playerOrientation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 lookDir = lookDirInput.action.ReadValue<Vector2>();

        float x = lookDir.x * Time.deltaTime * sensitivity;
        float y = lookDir.y * Time.deltaTime * sensitivity;

        yRot += x;

        xRot -= y;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        playerOrientation.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
