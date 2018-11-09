using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MaterialCollider : MonoBehaviour {
    [SerializeField] int m_InteractionTime = 100;
    public Text cdinfo;
    public Text score;
    int countdown=0;
    bool isCrafting;

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

    public Image crafting;
    private InventoryC inventoryC;
    public GameObject craftbutton;

    public Image product11;
    public Image product21;
    public Image product31;
    public Image product41;
    public Image product51;
    public Image product61;
    public Image product71;

    public Image winner;
    public GameObject restartbutton;

    private Text t;
    public Text prompt;
    

    [SerializeField] AudioClip takeItem;
    [SerializeField] AudioClip teleport;
    [SerializeField] AudioClip craftingBGM;

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

        if (crafting) crafting.transform.SetAsLastSibling();
        if (craftbutton) craftbutton.transform.SetAsLastSibling();

        if (product11) product11.transform.SetAsLastSibling();
        if (product21) product21.transform.SetAsLastSibling();
        if (product31) product31.transform.SetAsLastSibling();
        if (product41) product41.transform.SetAsLastSibling();
        if (product51) product51.transform.SetAsLastSibling();
        if (product61) product61.transform.SetAsLastSibling();
        if (product71) product71.transform.SetAsLastSibling();

        inventoryC = GetComponent<InventoryC>();
        EnableCrafting(false);

        restartbutton.SetActive(false);
        winner.enabled = false;
        score.enabled = false;
    }

    private void Update(){

        if (isCrafting){
            if (Input.GetKey(KeyCode.Alpha1)){ addToInventoryC(1); }
            if (Input.GetKey(KeyCode.Alpha2)){ addToInventoryC(2); }
            if (Input.GetKey(KeyCode.Alpha3)){ addToInventoryC(3); }
            if (Input.GetKey(KeyCode.Alpha4)){ addToInventoryC(4); }
        }
    }


    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag.Contains("interactable") || col.gameObject.tag.Contains("Store") || col.gameObject.tag.Contains("Computer"))
        {
            //Debug.Log(col.gameObject.name);
            //prompt.text = "Hold E to interact.";
            countdown = m_InteractionTime;
            prompt.enabled = true;
        }
        else if (col.gameObject.tag.Contains("crafting")){
            EnableCrafting(true);
        }
    }
    void OnTriggerStay(Collider col)
    {

        if (col.gameObject.tag.Contains("interactable") || col.gameObject.tag.Contains("Computer"))
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
                if (col.gameObject.tag.Contains("interactable")){
                    addToInventory(col.gameObject);
                    Destroy(col.gameObject);
                    GetComponent<AudioSource>().PlayOneShot(takeItem);
                }
                if (col.gameObject.tag.Contains("Computer")){
                    EndGame();
                }
                Disappear(collect);
                prompt.enabled = false;
            }

        }
        else if (col.gameObject.tag.Contains("Store"))
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
                transform.position = GameObject.Find("CraftingCoords").transform.position;
                GameObject.Find("Canvas").GetComponent<Transition>().putMask();
                prompt.enabled = false;
                Disappear(collect);
                GetComponent<AudioSource>().PlayOneShot(teleport);
                GetComponents<AudioSource>()[1].clip=craftingBGM;
                GetComponents<AudioSource>()[1].PlayOneShot(craftingBGM);
            }
        }

        }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag.Contains("interactable") || col.gameObject.tag.Contains("Store") || col.gameObject.tag.Contains("Computer"))
        {
            Disappear(collect);
            countdown = m_InteractionTime;
            prompt.enabled = false;
        }
        else if (col.gameObject.tag.Contains("crafting")){
            EnableCrafting(false);
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
        //update text
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
        //update text
        for (int i = 0; i < inventory2.slots.Length; i++){
            t = inventory2.slots[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>();
            if (t) t.text = inventory2.quantity[i].ToString();
        }
    }

    void addToInventoryC(int key){
        for (int i = 0; i < inventoryC.slots.Length-1; i++){
            if (inventoryC.filled[i] == 0 && inventory.filled[key-1] != 0){ //empty and inventory is filled
                inventoryC.filled[i] = inventory.filled[key-1];
                inventoryC.quantity[i] = inventory.quantity[key-1];
                if (inventory.filled[key-1] == 1){
                    object1.transform.position = inventoryC.slots[i].transform.position;
                    if (object1) object1.transform.SetAsLastSibling();
                }
                if (inventory.filled[key-1] == 2){
                    object2.transform.position = inventoryC.slots[i].transform.position;
                    if (object2) object2.transform.SetAsLastSibling();
                }
                if (inventory.filled[key-1] == 3){
                    object3.transform.position = inventoryC.slots[i].transform.position;
                    if (object3) object3.transform.SetAsLastSibling();
                }
                if (inventory.filled[key-1] == 4){
                    object4.transform.position = inventoryC.slots[i].transform.position;
                    if (object4) object4.transform.SetAsLastSibling();
                }
                inventory.filled[key-1] = 0;
                inventory.quantity[key-1] = 0;
                break;
            }
        }

        //determine product
        int a = inventoryC.filled[0];
        int b = inventoryC.filled[1];
        int x = inventoryC.quantity[0];
        int y = inventoryC.quantity[1];

        if (a == 4 && b == 1){ //product 3
            inventoryC.filled[2] = 3;
            product31.enabled = true;
            product31.transform.position = inventoryC.slots[2].transform.position;
            inventoryC.quantity[2] = DetermineQuantity(x,y,2,1);
        }
        if (a == 1 && b == 4){ //product 3
            inventoryC.filled[2] = 3;
            product31.enabled = true;
            product31.transform.position = inventoryC.slots[2].transform.position;
            inventoryC.quantity[2] = DetermineQuantity(x,y,1,2);
        }
        if (a == 1 && b == 3){ //product 6
            inventoryC.filled[2] = 6;
            product61.enabled = true;
            product61.transform.position = inventoryC.slots[2].transform.position;
            inventoryC.quantity[2] = DetermineQuantity(x,y,3,1);
        }
        if (a == 3 && b == 1){ //product 6
            inventoryC.filled[2] = 6;
            product61.enabled = true;
            product61.transform.position = inventoryC.slots[2].transform.position;
            inventoryC.quantity[2] = DetermineQuantity(x,y,1,3);
        }
        if (a == 3 && b == 4){ //product 4
            inventoryC.filled[2] = 4;
            product41.enabled = true;
            product41.transform.position = inventoryC.slots[2].transform.position;
            inventoryC.quantity[2] = DetermineQuantity(x,y,2,1);
        }
        if (a == 4 && b == 3){ //product 4
            inventoryC.filled[2] = 4;
            product41.enabled = true;
            product41.transform.position = inventoryC.slots[2].transform.position;
            inventoryC.quantity[2] = DetermineQuantity(x,y,1,2);
        }
        if (a == 2 && b == 3){ //product 5
            inventoryC.filled[2] = 5;
            product51.enabled = true;
            product51.transform.position = inventoryC.slots[2].transform.position;
            inventoryC.quantity[2] = DetermineQuantity(x,y,4,1);
        }
        if (a == 3 && b == 2){ //product 5
            inventoryC.filled[2] = 5;
            product51.enabled = true;
            product51.transform.position = inventoryC.slots[2].transform.position;
            inventoryC.quantity[2] = DetermineQuantity(x,y,1,4);
        }
        if ((a == 2 && b == 4) || (a == 4 && b == 2)){ //product 7
            inventoryC.filled[2] = 7;
            product71.enabled = true;
            product71.transform.position = inventoryC.slots[2].transform.position;
            inventoryC.quantity[2] = DetermineQuantity(x,y,1,1);
        }
        if ((a == 1 && b == 2) || (a == 2 && b == 1)){ //product 2
            inventoryC.filled[2] = 2;
            product21.enabled = true;
            product21.transform.position = inventoryC.slots[2].transform.position;
            inventoryC.quantity[2] = DetermineQuantity(x,y,2,2);
        }

        //update text
        for (int i = 0; i < inventoryC.slots.Length; i++){
            t = inventoryC.slots[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>();
            if (t) t.text = inventoryC.quantity[i].ToString();
        }
        for (int i = 0; i < inventory.slots.Length; i++){
            t = inventory.slots[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>();
            if (t) t.text = inventory.quantity[i].ToString();
        }
    }

    int DetermineQuantity(int x, int y, int rx, int ry){
        int ret = 0;
        while (x >= rx && y >= ry){
            x = x - rx;
            y = y - ry;
            ret++;
        }
        return ret;
    }

    void ResetCrafting(){
        for (int j = 0; j < 2; j++){
            for (int i = 0; i < inventory.slots.Length; i++){
                if (inventory.filled[i] == 0 && inventoryC.filled[j] != 0){ //empty and inventoryC is filled
                    if (inventoryC.filled[j] == 1){
                        inventory.filled[i] = 1;
                        object1.transform.position = inventory.slots[i].transform.position;
                        inventory.quantity[i] = inventoryC.quantity[j];
                    }
                    if (inventoryC.filled[j] == 2){
                        inventory.filled[i] = 2;
                        object2.transform.position = inventory.slots[i].transform.position;
                        inventory.quantity[i] = inventoryC.quantity[j];
                    }
                    if (inventoryC.filled[j] == 3){
                        inventory.filled[i] = 3;
                        object3.transform.position = inventory.slots[i].transform.position;
                        inventory.quantity[i] = inventoryC.quantity[j];
                    }
                    if (inventoryC.filled[j] == 4){
                        inventory.filled[i] = 4;
                        object4.transform.position = inventory.slots[i].transform.position;
                        inventory.quantity[i] = inventoryC.quantity[j];
                    }
                    inventoryC.filled[j] = 0;
                    inventoryC.quantity[j] = 0;
                    break;
                }
            }
        }

        //remove product images
        product11.enabled = false;
        product21.enabled = false;
        product31.enabled = false;
        product41.enabled = false;
        product51.enabled = false;
        product61.enabled = false;
        product71.enabled = false;
        inventoryC.filled[2] = 0;
        inventoryC.quantity[2] = 0;

        //update text
        for (int i = 0; i < inventoryC.slots.Length; i++){
            t = inventoryC.slots[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>();
            if (t) t.text = inventoryC.quantity[i].ToString();
        }
        for (int i = 0; i < inventory.slots.Length; i++){
            t = inventory.slots[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>();
            if (t) t.text = inventory.quantity[i].ToString();
        }
    }

    public void Craft(){
        int a = inventoryC.filled[0];
        int b = inventoryC.filled[1];

        if (a == 4 && b == 1){ //product 3
            if (inventoryC.quantity[0] - 2 < 0 || inventoryC.quantity[1] - 1 < 0) return;
            inventoryC.quantity[0] -= 2;
            inventoryC.quantity[1] -= 1;
            addToInventory2(3);
        }
        if (a == 1 && b == 4){ //product 3
            if (inventoryC.quantity[0] - 1 < 0 || inventoryC.quantity[1] - 2 < 0) return;
            inventoryC.quantity[0] -= 1;
            inventoryC.quantity[1] -= 2;
            addToInventory2(3);
        }
        if (a == 1 && b == 3){ //product 6
            if (inventoryC.quantity[0] - 3 < 0 || inventoryC.quantity[1] - 1 < 0) return;
            inventoryC.quantity[0] -= 3;
            inventoryC.quantity[1] -= 1;
            addToInventory2(6);
        }
        if (a == 3 && b == 1){ //product 6
            if (inventoryC.quantity[0] - 1 < 0 || inventoryC.quantity[1] - 3 < 0) return;
            inventoryC.quantity[0] -= 1;
            inventoryC.quantity[1] -= 3;
            addToInventory2(6);
        }
        if (a == 3 && b == 4){ //product 4
            if (inventoryC.quantity[0] - 2 < 0 || inventoryC.quantity[1] - 1 < 0) return;
            inventoryC.quantity[0] -= 2;
            inventoryC.quantity[1] -= 1;
            addToInventory2(4);
        }
        if (a == 4 && b == 3){ //product 4
            if (inventoryC.quantity[0] - 1 < 0 || inventoryC.quantity[1] - 2 < 0) return;
            inventoryC.quantity[0] -= 1;
            inventoryC.quantity[1] -= 2;
            addToInventory2(4);
        }
        if (a == 2 && b == 3){ //product 5
            if (inventoryC.quantity[0] - 4 < 0 || inventoryC.quantity[1] - 1 < 0) return;
            inventoryC.quantity[0] -= 4;
            inventoryC.quantity[1] -= 1;
            addToInventory2(5);
        }
        if (a == 3 && b == 2){ //product 5
            if (inventoryC.quantity[0] - 1 < 0 || inventoryC.quantity[1] - 4 < 0) return;
            inventoryC.quantity[0] -= 1;
            inventoryC.quantity[1] -= 4;
            addToInventory2(5);
        }
        if ((a == 2 && b == 4) || (a == 4 && b == 2)){ //product 7
            if (inventoryC.quantity[0] - 1 < 0 || inventoryC.quantity[1] - 1 < 0) return;
            inventoryC.quantity[0] -= 1;
            inventoryC.quantity[1] -= 1;
            addToInventory2(7);
        }
        if ((a == 1 && b == 2) || (a == 2 && b == 1)){ //product 2
            if (inventoryC.quantity[0] - 2 < 0 || inventoryC.quantity[1] - 2 < 0) return;
            inventoryC.quantity[0] -= 2;
            inventoryC.quantity[1] -= 2;
            addToInventory2(2);
        }
        if (inventoryC.quantity[2] > 0) inventoryC.quantity[2]--;

        //update text
        for (int i = 0; i < inventoryC.slots.Length; i++){
            t = inventoryC.slots[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>();
            if (t) t.text = inventoryC.quantity[i].ToString();
        }
        for (int i = 0; i < inventory.slots.Length; i++){
            t = inventory.slots[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>();
            if (t) t.text = inventory.quantity[i].ToString();
        }
    }

    void EnableCrafting(bool b){
        if (crafting) crafting.enabled = b;
        for (int i = 0; i < inventoryC.slots.Length; i++){
            inventoryC.slots[i].SetActive(b);
        }
        craftbutton.SetActive(b);
        isCrafting = b;
        Cursor.visible = b;
        if (!b){ ResetCrafting(); }
    }

    void EndGame(){
        winner.enabled = true;
        restartbutton.SetActive(true);
        Cursor.visible = true;
        score.enabled = true;

        //calculate profit
        int profit = 0;
        for (int i = 0; i < inventory2.slots.Length; i++){
            int value = 0;
            int product = inventory2.filled[i];
            if (product == 3){ value = 10; }
            if (product == 6){ value = 20; }
            if (product == 4){ value = 10; }
            if (product == 5){ value = 30; }
            if (product == 7){ value = 5; }
            if (product == 2){ value = 20; }
            profit += value*inventory2.quantity[i];
        }
        score.text = "Score: $" + profit.ToString();

        product1.enabled = false;
        product2.enabled = false;
        product3.enabled = false;
        product4.enabled = false;
        product5.enabled = false;
        product6.enabled = false;
        product7.enabled = false;
        object1.enabled = false;
        object2.enabled = false;
        object3.enabled = false;
        object4.enabled = false;
    }
}
