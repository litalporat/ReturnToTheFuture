using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] LayerMask playerLayer;
    float speed = 10;
    Vector3[] directions = new Vector3[2];
    Vector3 target, moveDirection;
    Animator anim;
    int currWaypoint = 0;
    bool stay = true;
    float waitTime = 5;


    // Start is called before the first frame update
    void Start()
    {
        directions[0] = new Vector3(0,90,0);
        directions[1] = new Vector3(0,270,0);
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        target = waypoints[currWaypoint].position;
        moveDirection = target - transform.position;
        if(moveDirection.magnitude < 1 && stay){
            currWaypoint = ++currWaypoint% waypoints.Length;
            StartCoroutine(Stay());
        } 
        GetComponent<Rigidbody>().velocity = moveDirection.normalized * speed;
    }

    IEnumerator Stay(){
        // Debug.Log("stay");
        stay = false;
        yield return new WaitForSeconds(waitTime);
        stay = true;
        transform.rotation = Quaternion.Euler(directions[currWaypoint]);
        // Debug.Log(" not stay");

    }
}
