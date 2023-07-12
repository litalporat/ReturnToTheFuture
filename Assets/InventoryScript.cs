using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public static InventoryScript instance;
    public List<GameObject> listItems;
    public List<GameObject> placement;
    // Start is called before the first frame update
    void Start()
    {
        listItems = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i < listItems.Count; i++){
            Instantiate(listItems[i], placement[i].transform.position, Quaternion.identity);
        }
    }

    public void add(GameObject gameObject){
        listItems.Add(gameObject);
        Destroy(gameObject);
    }
}
