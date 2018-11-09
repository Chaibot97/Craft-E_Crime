using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{

    public float turnSpeed = 4.0f;
    public Transform player;
    public Vector3 off;
    private Vector3 offset;

    void Start()
    {
        offset = player.position + off;
    }

    void LateUpdate()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * -turnSpeed, transform.right) * offset;
        Vector3 newPostion= player.position + offset;
        if (newPostion.y >=0.1f)
        {
            transform.position = newPostion;
        }else{
            transform.position = new Vector3(newPostion.x,0.1f,newPostion.z);
        }
        transform.LookAt(player.position+transform.up.normalized*0.8f);
        //Debug.Log(transform.rotation);
    }
}
