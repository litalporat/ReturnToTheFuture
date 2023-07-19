using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carScript : MonoBehaviour
{
    float speed;
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
    }
}
