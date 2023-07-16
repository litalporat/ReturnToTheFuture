using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<GameObject> listItems;
    public List<GameObject> placement;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        listItems = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void add(GameObject gameObject){
        listItems.Add(gameObject);
        Instantiate(gameObject, placement[listItems.LastIndexOf(gameObject)].transform.position, gameObject.transform.rotation);
    }
}
