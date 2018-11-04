using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MaterialCollider : MonoBehaviour {
    [SerializeField] int m_InteractionTime = 100;
    public Text cdinfo;
    int countdown=0;

    [SerializeField] Image collect;




    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "interactable")
        {
            //Debug.Log(col.gameObject.name);
            countdown = m_InteractionTime;
        } 
    }
    void OnTriggerStay(Collider col)
    {

        if (col.gameObject.tag == "interactable")
        {
            if (!(Input.GetKey(KeyCode.E)|| Input.GetKey(KeyCode.Mouse0)))
            {
                collectDisappear();
                countdown = m_InteractionTime;
                if (cdinfo)
                    cdinfo.text = countdown.ToString();
            }
            else if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Mouse0))
            {
                collect.transform.SetPositionAndRotation(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0), Quaternion.identity);
                countdown--;
                cdinfo.text = countdown.ToString();
            }

            //Debug.Log(countdown.ToString());
            if (countdown <= 0)
            {
                collectDisappear();
                Destroy(col.gameObject);
            }

        }
    }
    void OnTriggerExit(Collider col)
    {
        collectDisappear();
        countdown = m_InteractionTime;
    }

    void collectDisappear(){
        collect.transform.SetPositionAndRotation(new Vector3(1000,1000,1000), Quaternion.identity);
    }
}