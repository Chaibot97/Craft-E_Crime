using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Player m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
    private bool m_Sprint;

    [SerializeField] Image shoppinglist;
    [SerializeField] Image tabshop;
    [SerializeField] Text tabtext;

    private Text t;

    private void Start()
    {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<Player>();

        shoppinglist.enabled = false;
        tabshop.enabled = true;
        tabtext.enabled = true;
    }


    private void Update()
    {

    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float z = 0;
        float x = 0;
        if (!Input.GetKey(KeyCode.E)){
            if(Input.GetKey(KeyCode.W)){
                z += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                z -= 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                x -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                x += 1;
            }
        }

        if (Input.GetKey(KeyCode.Tab)){
            shoppinglist.enabled = true;
            tabshop.enabled = false;
            tabtext.enabled = false;
        }
        else{
            shoppinglist.enabled = false;
            tabshop.enabled = true;
            tabtext.enabled = true;
        }
        


        // calculate camera relative direction to move:
        m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
        m_Move = z * m_CamForward + x * m_Cam.right;

       
       

        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Sprint=true;
        if (Input.GetKey(KeyCode.Space)) m_Jump = true;
        // pass all parameters to the character control script
        m_Character.Move(m_Move, new Quaternion(0, m_Cam.transform.rotation.y,0, m_Cam.transform.rotation.w) ,m_Sprint,m_Jump);
        m_Jump = false;
        m_Sprint = false;
    }


}
