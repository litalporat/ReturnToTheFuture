using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carScript : MonoBehaviour
{
    float speed;
    float knockbackForce = 10f;

    public Vector3 initialPlayerPosition;
    
    void Start(){
        speed = GetComponentInParent<CarSpawnScript>().speed;
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other){
        string name = other.gameObject.name;
        if(name.Equals("left") || name.Equals("right")){
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Player")
        {
            other.transform.position = initialPlayerPosition;
        }
    }
}
