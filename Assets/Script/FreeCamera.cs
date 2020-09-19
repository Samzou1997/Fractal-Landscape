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
    public Vector2 mapLimit; 

    private bool looking = false;
    private float nRotX = 0.0f;
    private float nRotY = 0.0f;
    
    public Vector3 Reset(Vector3 point)
    {
        point.x = mapLimit.x * -0.8f;
        point.y = mapLimit.x;
        point.z = mapLimit.y * -0.8f;

        ResetRotation();
        return point;
    }

    void ResetRotation() 
    {
        //transform.localEulerAngles = new Vector3(45.0f, 0f, 0f);
        nRotX = 45.0f;
        nRotY = 35.0f;
    }

    // Start is called before the first frame update
    void Start(){
        transform.position = Reset(transform.position);
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
            pos = Reset(pos);
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
        pos.y = Mathf.Clamp(pos.y, 2, mapLimit.y * 2);
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collsion Begin");
        print(collision.collider.gameObject.name);
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("In Collsion");
        print(collision.collider.gameObject.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collsion Finished");
        print(collision.collider.gameObject.name);
    }
}
