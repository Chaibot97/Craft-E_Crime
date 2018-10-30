using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float m_TurnSpeed = 360;
    [SerializeField] float m_JumpPower = 12f; 

    [SerializeField] float m_MoveSpeedMultiplier = 3f;
    //[SerializeField] float m_GroundCheckDistance = 3f;
    //[SerializeField] float m_AnimSpeedMultiplier = 1f;
    Rigidbody m_Rigidbody;
    Animator m_Animator;
    float m_ForwardAmount;
    Vector3 m_GroundNormal;
    bool m_IsGrounded;


    // Use this for initialization
    void Start()
    {
        //m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    public void Move(Vector3 move,Quaternion facing,bool jump)
    {
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.2f) + (transform.forward * 3));
        //Debug.Log(move);
       
        var step = m_TurnSpeed * Time.deltaTime;
        m_Rigidbody.transform.rotation = Quaternion.RotateTowards(transform.rotation, facing, step);

        move = move * m_MoveSpeedMultiplier;
        m_ForwardAmount = move.z;


        m_IsGrounded =Physics.Raycast(transform.position + (Vector3.down * 0.4f), Vector3.down, 0.1f);

        if(m_IsGrounded)
        {

            m_Rigidbody.velocity=move;
            if (jump)
                m_Rigidbody.AddForce(Vector3.up * m_JumpPower, ForceMode.Impulse);
        }
        else{
            m_Rigidbody.AddForce(move);
            m_Rigidbody.AddForce(Physics.gravity);
        }
        //Debug.Log(m_Rigidbody.velocity);
        //UpdateAnimator(move);
    }


}
