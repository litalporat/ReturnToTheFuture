using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnScript : MonoBehaviour
{
    [SerializeField] List<GameObject> cars;
    // public List<GameObject> activeCars;
    [SerializeField] float interval;
    public float speed;
    int rand;
    Vector3 carRotation;
    GameObject currCar;
    [SerializeField] bool directionLtoR;
    Vector3 start;
    GameObject end;



    // Start is called before the first frame update
    void Start()
    {
        if(directionLtoR){
            carRotation = new Vector3(0,90,0);
            start = gameObject.transform.GetChild(0).position;
            end = gameObject.transform.GetChild(1).gameObject;
        }
        else{
            carRotation = new Vector3(0,-90,0);
            start = gameObject.transform.GetChild(1).position;
            end = gameObject.transform.GetChild(0).gameObject;
        }
        end.AddComponent<BoxCollider>();
        InvokeRepeating("newCar", 2, interval);
    }

    void newCar(){
        rand = Random.Range(0, cars.Count);
        Instantiate(cars[rand], start, Quaternion.Euler(carRotation)).transform.parent = transform;
        // currCar.GetComponent<Rigidbody>().velocity = speed;
    }

}
