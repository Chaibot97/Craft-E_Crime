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

    private Inventory2 inventory2;
    public Image product1;
    public Image product2;
    public Image product3;
    public Image product4;
    public Image product5;
    public Image product6;
    public Image product7;

    private Text t;
    public Text prompt;

    private void Start(){
        inventory = GetComponent<Inventory>();
        if (object1) object1.transform.SetAsLastSibling();
        if (object2) object2.transform.SetAsLastSibling();
        if (object3) object3.transform.SetAsLastSibling();
        if (object4) object4.transform.SetAsLastSibling();

        inventory2 = GetComponent<Inventory2>();
        if (product1) product1.transform.SetAsLastSibling();
        if (product2) product2.transform.SetAsLastSibling();
        if (product3) product3.transform.SetAsLastSibling();
        if (product4) product4.transform.SetAsLastSibling();
        if (product5) product5.transform.SetAsLastSibling();
        if (product6) product6.transform.SetAsLastSibling();
        if (product7) product7.transform.SetAsLastSibling();
    }

    private void Update(){
        
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

    void addToInventory2(int product){
        for (int i = 0; i < inventory2.slots.Length; i++)
        {
            if (inventory2.filled[i] == 0){ //empty
                if (product == 1){
                    product1.enabled = true;
                    inventory2.filled[i] = 1;
                    product1.transform.position = inventory2.slots[i].transform.position;
                    inventory2.quantity[i] = 1;
                }
                if (product == 2){
                    product2.enabled = true;
                    inventory2.filled[i] = 2;
                    product2.transform.position = inventory2.slots[i].transform.position;
                    inventory2.quantity[i] = 1;
                }
                if (product == 3){
                    product3.enabled = true;
                    inventory2.filled[i] = 3;
                    product3.transform.position = inventory2.slots[i].transform.position;
                    inventory2.quantity[i] = 1;
                }
                if (product == 4){
                    product4.enabled = true;
                    inventory2.filled[i] = 4;
                    product4.transform.position = inventory2.slots[i].transform.position;
                    inventory2.quantity[i] = 1;
                }
                if (product == 5){
                    product5.enabled = true;
                    inventory2.filled[i] = 5;
                    product5.transform.position = inventory2.slots[i].transform.position;
                    inventory2.quantity[i] = 1;
                }
                if (product == 6){
                    product6.enabled = true;
                    inventory2.filled[i] = 6;
                    product6.transform.position = inventory2.slots[i].transform.position;
                    inventory2.quantity[i] = 1;
                }
                if (product == 7){
                    product7.enabled = true;
                    inventory2.filled[i] = 7;
                    product7.transform.position = inventory2.slots[i].transform.position;
                    inventory2.quantity[i] = 1;
                }
                break;
            }
            else if(inventory2.filled[i] == product){ //same product
                inventory2.quantity[i]++;
                break;
            }
        }
        for (int i = 0; i < inventory2.slots.Length; i++){
            t = inventory2.slots[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>();
            if (t) t.text = inventory2.quantity[i].ToString();
        }
    }
}