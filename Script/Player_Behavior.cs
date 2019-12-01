﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behavior : MonoBehaviour
{
    public float look_sensitivity_x = 10f;
    public float look_sensitivity_y = 10f;
    public float move_speed = 10f;

    private float rotationY = 0F;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * move_speed * Time.deltaTime);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * move_speed * Time.deltaTime);

        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * look_sensitivity_x * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.left, Input.GetAxis("Mouse Y") * look_sensitivity_y * Time.deltaTime, Space.Self);
    }
}
