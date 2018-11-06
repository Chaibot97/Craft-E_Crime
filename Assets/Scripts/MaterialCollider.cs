using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MaterialCollider : MonoBehaviour {
    [SerializeField] int m_InteractionTime = 100;
    public Text cdinfo;
    int countdown=0;

    [SerializeField] Image collect;

    private Inventory inventory;
    public Image object1;
    public Image object2;
    public Image object3;
    public Image object4;

    private Text t;
    public Text prompt;

    private void Start(){
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update(){
        if (object1) object1.transform.SetAsLastSibling();
        if (object2) object2.transform.SetAsLastSibling();
        if (object3) object3.transform.SetAsLastSibling();
        if (object4) object4.transform.SetAsLastSibling();
    }


    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag.Contains("interactable"))
        {
            //Debug.Log(col.gameObject.name);
            countdown = m_InteractionTime;
            //prompt.text = "Hold E to interact.";
            prompt.enabled = true;
        }
    }
    void OnTriggerStay(Collider col)
    {

        if (col.gameObject.tag.Contains("interactable"))
        {
            if (!(Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Mouse0)))
            {
                Disappear(collect);
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
                Disappear(collect);
                addToInventory(col.gameObject);
                Destroy(col.gameObject);
                prompt.enabled = false;
            }

        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag.Contains("interactable"))
        {
            Disappear(collect);
            countdown = m_InteractionTime;
            prompt.enabled = false;
        }
    }
    
    void Disappear(Image img){
        img.transform.SetPositionAndRotation(new Vector3(1000,1000,1000), Quaternion.identity);
    }

    void addToInventory(GameObject item){
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.filled[i] == 0){ //empty
                //Add to slot
                if (item.tag.Contains("1")){
                    object1.enabled = true;
                    inventory.filled[i] = 1;
                    object1.transform.position = inventory.slots[i].transform.position;
                    inventory.quantity[i] = 1;
                }
                if (item.tag.Contains("2")){
                    object2.enabled = true;
                    inventory.filled[i] = 2;
                    object2.transform.position = inventory.slots[i].transform.position;
                    inventory.quantity[i] = 1;
                }
                if (item.tag.Contains("3")){
                    object3.enabled = true;
                    inventory.filled[i] = 3;
                    object3.transform.position = inventory.slots[i].transform.position;
                    inventory.quantity[i] = 1;
                }
                if (item.tag.Contains("4")){
                    object4.enabled = true;
                    inventory.filled[i] = 4;
                    object4.transform.position = inventory.slots[i].transform.position;
                    inventory.quantity[i] = 1;
                }
                break;
            }
            else if(item.tag.Contains(inventory.filled[i].ToString())){ //same material
                inventory.quantity[i]++;
                break;
            }
        }
        for (int i = 0; i < inventory.slots.Length; i++){
            t = inventory.slots[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>();
            if (t) t.text = inventory.quantity[i].ToString();
        }
    }
}