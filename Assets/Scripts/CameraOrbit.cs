using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{

    public float turnSpeed = 4.0f;
    public Transform player;
    public Vector3 off;
    public float centerOffset;
    private Vector3 offset;

    void Start()
    {
        offset = player.position + off;
    }

    void LateUpdate()
    {
        if (Cursor.visible) return;
		offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * -turnSpeed, transform.right) * offset;
        Vector3 newPostion= player.position + offset;
        if (newPostion.y >=0.1f)
        {
            transform.position = newPostion;
        }else{
            transform.position = new Vector3(newPostion.x,0.1f,newPostion.z);
        }
        transform.LookAt(player.position+transform.up.normalized* centerOffset);
        //Debug.Log(transform.rotation);
    }
}
