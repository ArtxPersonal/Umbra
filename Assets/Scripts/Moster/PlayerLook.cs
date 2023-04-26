using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float minViewDis = 25.0f;
    public float mouseSensitivity = 100f;
    [SerializeField] private Transform monsterBody;
    private float xRotation = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float _mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float _mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= _mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, minViewDis);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        monsterBody.Rotate(Vector3.up * _mouseX);
    }
}
