using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] float m_TurnSpeed = 360;
    [SerializeField] float m_JumpPower = 12f; 

    [SerializeField] float m_MoveSpeedMultiplier = 1f;
    [SerializeField] float m_SprintSpeedMultiplier = 2f;
    [SerializeField] int m_MaxStamina = 100;

    [SerializeField] Text staminaInfo;
    [SerializeField] Text speedInfo;
    [SerializeField] private float m_StepInterval;
    [SerializeField] private AudioClip[] m_FootstepSounds;
    //[SerializeField] float m_GroundCheckDistance = 3f;
    //[SerializeField] float m_AnimSpeedMultiplier = 1f;
    Rigidbody m_Rigidbody;
    Animator m_Animator;
    float m_ForwardAmount;
    Vector3 m_GroundNormal;
    bool m_IsGrounded;
    int stamina;
    private float m_StepCycle;
    private float m_NextStep;
    private AudioSource m_AudioSource;



    // Use this for initialization
    void Start()
    {
        //m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        stamina = m_MaxStamina;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        m_AudioSource = GetComponent<AudioSource>();
        m_StepCycle = 0f;
        m_NextStep = m_StepCycle / 2f;
    }

    public void Move(Vector3 move,Quaternion facing,bool sprint,bool jump)
    {
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.2f) + (transform.forward * 3));
       
        var step = m_TurnSpeed * Time.deltaTime;
        m_Rigidbody.transform.rotation = Quaternion.RotateTowards(transform.rotation, facing, step);

        move = move * m_MoveSpeedMultiplier;
        if (move.magnitude > m_MoveSpeedMultiplier){move = move * (m_MoveSpeedMultiplier/move.magnitude);} //adjust diagonal speed

        m_IsGrounded =Physics.Raycast(transform.position + (Vector3.down * 0.4f), Vector3.down, 0.1f);

        if(sprint&&stamina>0){
            stamina--;
            move *= m_SprintSpeedMultiplier;
            //Debug.Log(stamina);
        }else if(!sprint&&stamina < m_MaxStamina)
        {
            stamina++;
            //Debug.Log(stamina);
        }
        m_Rigidbody.velocity = move;
        ProgressStepCycle(move.magnitude);
        //Debug.Log(m_Rigidbody.velocity);
        //if (m_IsGrounded)
        //{

        //    m_Rigidbody.velocity=move;
        //    ProgressStepCycle(move.magnitude);
        //    Debug.Log(m_Rigidbody.velocity);

        //    if (jump)
        //        m_Rigidbody.AddForce(Vector3.up * m_JumpPower, ForceMode.Impulse);
        //}
        //else{
            //m_Rigidbody.AddForce(move);
         m_Rigidbody.AddForce(Physics.gravity);
        //}
        staminaInfo.text = stamina.ToString();
        m_ForwardAmount = move.magnitude;
        speedInfo.text = m_ForwardAmount.ToString();
        //Debug.Log(m_Rigidbody.velocity);
        //UpdateAnimator(move);
    }

    private void ProgressStepCycle(float speed)
    {
        if (speed> 0 )
        {
            m_StepCycle += speed *Time.fixedDeltaTime;
        }

        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        m_NextStep = m_StepCycle + m_StepInterval;

        PlayFootStepAudio();
    }
    private void PlayFootStepAudio()
    {
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, m_FootstepSounds.Length);
        m_AudioSource.clip = m_FootstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;
    }


}
