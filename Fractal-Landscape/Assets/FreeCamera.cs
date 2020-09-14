using System.Collections;
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

    // Update is called once per frame
    void Update()
    {
        var fastMode = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        var movementSpeed = fastMode ? this.fastSpeed : this.movementSpeed;

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + (-transform.right) * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + (transform.right) * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + (transform.forward) * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position + (-transform.forward) * movementSpeed * Time.deltaTime;
        }

        if (looking)
        {
            float nRotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * freeLookSens;
            float nRotY = transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * freeLookSens;
            transform.localEulerAngles = new Vector3(nRotY, nRotX, 0f);
        }

        float axis = Input.GetAxis("Mouse ScrollWheel");
        if (axis != 0)
        {
            var zoomSensL = fastMode ? this.fastZoomSens : this.zoomSens;
            transform.position = transform.position + transform.forward * axis * zoomSensL;
        }
        /*
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ///StartLooking();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            StartLooking();
        }
        */
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
