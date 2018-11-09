using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EnemySight : MonoBehaviour {

    [SerializeField] float m_FOV = 110f;
    [SerializeField] int m_DetectedTime = 100;
    private SphereCollider sCol;
    public Text Detectinfo;
    private bool seen = false;
    private int countdown = 0;
    private float speed;

    private void Awake()
    {
        sCol = GetComponent<SphereCollider>();
        countdown = m_DetectedTime;
        speed = GetComponent<FollowPath>().Speed;
    }
    private void Update()
    {
        Vector3 line1 = transform.position + Quaternion.Euler(0, m_FOV*0.5f, 0)*transform.forward*sCol.radius;
        Debug.DrawLine(transform.position, line1, Color.green);
        Vector3 line2 = transform.position + Quaternion.Euler(0, -m_FOV * 0.5f, 0) * transform.forward * sCol.radius;
        Debug.DrawLine(transform.position, line2, Color.green);
        if (!seen && countdown < m_DetectedTime)
        {
            countdown++;
            Detectinfo.text = countdown.ToString();
        }
        if (seen)
        {

            GetComponent<FollowPath>().Speed = 0;

        }else{
            GetComponent<FollowPath>().Speed = speed;
        }

    }
  
    void OnTriggerStay(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            Vector3 direction = col.gameObject.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if(angle<=m_FOV*0.5f){
                RaycastHit hit;
                if(Physics.Raycast(transform.position+transform.up * 0.2f, direction.normalized,out hit, sCol.radius)){
                    if (hit.collider.gameObject.tag == "Player") {
                        seen = true;
                        if(countdown>0)
                            countdown--;

                        //Debug.Log("seen");

                    }
                }else{
                    seen = false;
                }

            }
            else{
                seen = false;
            }
            Detectinfo.text = countdown.ToString();
            if (countdown <= 0)
            {
                SceneManager.LoadScene("GameOver");
                Cursor.visible = true;
            }
            //Debug.Log(seen);


        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            seen = false;
        }
    }


}
