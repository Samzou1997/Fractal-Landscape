﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public float movementSpeed = 12f;
    public float fastSpeed = 60f;
    public float freeLookSens = 2.5f;
    public float zoomSens = 10f;
    public float fastZoomSens = 50f;
    private bool looking = false;
    private float nRotX = 0.0f;
    private float nRotY = 0.0f;
    public Vector2 mapLimit; 

    public Vector3 reset(Vector3 point)
    {
        point.x = 1.0f;
        point.y = 50.0f;
        point.z = 1.0f;
        return point;
    }

    // Start is called before the first frame update
    void Start(){
        transform.position = reset(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        var fastMode = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        var movementSpeed = fastMode ? this.fastSpeed : this.movementSpeed;
        Vector3 pos = transform.position;
        if (Input.GetKey(KeyCode.A))
        {
            pos += (-transform.right) * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos += (transform.right) * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            pos += (transform.forward) * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos += (-transform.forward) * movementSpeed * Time.deltaTime;
        }

        // reset the camera postion when the terrian is re-generated
        if (Input.GetKeyUp(KeyCode.Space)) {
            pos = reset(pos);
        }

        if (looking)
        {
            nRotX += Input.GetAxis("Mouse X") * freeLookSens;
            nRotY -= Input.GetAxis("Mouse Y") * freeLookSens;
            transform.localEulerAngles = new Vector3(nRotY, nRotX, 0f);
        }

        float axis = Input.GetAxis("Mouse ScrollWheel");
        if (axis != 0)
        {
            var zoomSensL = fastMode ? this.fastZoomSens : this.zoomSens;
            pos += transform.forward * axis * zoomSensL;
        }

        
        pos.x = Mathf.Clamp(pos.x, -mapLimit.x, mapLimit.x);
        pos.z = Mathf.Clamp(pos.z, -mapLimit.y, mapLimit.y);
        transform.position = pos;
        
        StartLooking();
    }
    
    public void StartLooking()
    {
        looking = true;
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void StopLooking()
    {
        looking = false;
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }
    void OnDisable()
    {
        StopLooking();
    }
}