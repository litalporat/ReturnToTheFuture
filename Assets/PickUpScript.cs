using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onCollisionEnter(Collider other){
        Debug.Log("in TriggerEnter");
        if(Input.GetKeyDown(KeyCode.E)){
            InventoryScript.instance.add(other.gameObject);
        }
    }
}
