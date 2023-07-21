using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class fmsScript : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] GameObject FirstCamera; 
    [SerializeField] GameObject ThirdCamera; 
    [ SerializeField] float playerSpeed = 5;
    float mouseSensivity = 3; 
    Vector2 look;
    Animator anim;
    [SerializeField] GameObject player;
    Vector3 velocity; 
    float mass = 1f;
    float jumpSpeed = 5f;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject targetImg;
    [SerializeField] LayerMask layerMaskWeapon;
    [SerializeField] GameObject blueImg;
    [SerializeField] GameObject greenImg;
    [SerializeField] GameObject redImg;
    [SerializeField] GameObject BlueWeapon;
    [SerializeField] GameObject GreenWeapon;
    [SerializeField] GameObject RedWeapon;
    [SerializeField] GameObject GreenLaser;
    [SerializeField] GameObject BlueLaser;
    [SerializeField] GameObject RedLaser;
    Vector3 weaponRotation = new Vector3(90, 0, 0);
    Vector3 laserRotation = new Vector3(100, 0, 0);
    [SerializeField] Transform weaponSpawn;
    float pickUpDistance = 3f;
    float laserDistance = 20f;
    [SerializeField] GameObject ExplodeCube;
    [SerializeField] GameObject Life;
    int lifeCount = 3;
    Vector3 startPosition;
    bool restartPosition = false;
    [SerializeField] GameObject Map;
    GameObject smallMap;
    GameObject bigMap;
    [SerializeField] GameObject ExplodeDoor;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = player.GetComponent<Animator> ();
        Cursor.lockState = CursorLockMode.Locked;
        startPosition = transform.position;

        if(VariableScript.blueWeapon){
            blueImg.SetActive(true);
        }
        if(VariableScript.greenWeapon){
            greenImg.SetActive(true);
        }
        if(VariableScript.redWeapon){
            redImg.SetActive(true);
        }

        if(Map){
            smallMap = Map.transform.GetChild(0).gameObject;
            bigMap = Map.transform.GetChild(1).gameObject;
        }
    }
    void Update()
    {
        UpdateLook(); 
        UpdateGravity();
        
        if(!restartPosition){
            UpdateMovement();
        }else{
            transform.position = startPosition;
            restartPosition = false;
        }

        if(Input.GetKeyDown(KeyCode.C)){
            switchCameras();
        }


        if(Input.GetKeyDown(KeyCode.E)){
            pickUp();
        }


        if(Input.GetKeyDown(KeyCode.Alpha1) ){
            replaceWeapon(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            replaceWeapon(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            replaceWeapon(3);
        }
        

        if(Input.GetMouseButtonDown(0)){
            if(VariableScript.currWeapon){
                anim.SetBool("BShoot", true);
                Invoke("shoot",0.5f);
            }
        }

        if(Map){
            if(Input.GetKeyDown(KeyCode.M)){
                if(smallMap.activeSelf){
                    smallMap.SetActive(false);
                    bigMap.SetActive(true);
                }else{
                    bigMap.SetActive(false);
                    smallMap.SetActive(true);
                }
            }
        }
     }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.name.StartsWith("Car")){
            lifeCount--;
            if(lifeCount > 0){
                switchCameras();
                restartPosition = true;
                Life.transform.GetChild(lifeCount).gameObject.SetActive(false);
                Destroy(other.gameObject);
                text.text = "TRY AGAIN";

            }else{
                text.text = "DEAD";
                Invoke("dead", 1);
            }
        }
    }

    void switchCameras(){
        if(FirstCamera.activeSelf){
            ThirdCamera.SetActive(true);
            FirstCamera.SetActive(false);
        }else{
            FirstCamera.SetActive(true);
            ThirdCamera.SetActive(false);
        }
    }

    void dead(){
        Application.Quit();
    }

    void OnTriggerEnter(Collider other){
        if(other.name.StartsWith("Portal")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
    }

    void shoot(){
        GameObject currLaser;
        if(VariableScript.currWeapon.name.StartsWith("B")){
            currLaser = BlueLaser;
        }else if(VariableScript.currWeapon.name.StartsWith("G")){
            currLaser = GreenLaser;
        }else{
            currLaser = RedLaser;
        }
        
        Destroy(Instantiate(currLaser, targetImg.transform.position, targetImg.transform.rotation),1);

        if(Physics.Raycast(FirstCamera.transform.position, FirstCamera.transform.forward, out RaycastHit hit, laserDistance)){
            Debug.Log(hit.transform.name);
            if(hit.transform.name == "ShootCube"){
                Instantiate(ExplodeCube, hit.transform.position, hit.transform.rotation);
                Destroy(hit.transform.gameObject);
            } else if(hit.transform.name.StartsWith("door")){
                Debug.Log("in if");
                Instantiate(ExplodeDoor, hit.transform.position, hit.transform.rotation);
                Destroy(hit.transform.gameObject);
            }
        }

        anim.SetBool("BShoot", false);
    }

    void pickUp(){
        if(Physics.Raycast(targetImg.transform.position, targetImg.transform.forward, out RaycastHit raycastHit, pickUpDistance, layerMaskWeapon)){
            Destroy(raycastHit.transform.parent.gameObject);
            if(raycastHit.transform.name.StartsWith("Blue")){
                blueImg.SetActive(true);
                VariableScript.blueWeapon = true;
            }else if(raycastHit.transform.name.StartsWith("Green")){
                greenImg.SetActive(true);
                VariableScript.greenWeapon = true;
            }else if(raycastHit.transform.name.StartsWith("Red")){
                redImg.SetActive(true);
                VariableScript.redWeapon = true;
            }
            
        }
    }
     void replaceWeapon(int weapon){
        if(!anim.GetBool("BShoot")){
            if(VariableScript.currWeapon){
                Destroy(VariableScript.currWeapon);
            }
            if(weapon == 1 && VariableScript.blueWeapon){
                VariableScript.currWeapon = Instantiate(BlueWeapon, weaponSpawn.position, Quaternion.Euler(weaponRotation));
            }else if(weapon == 2 && VariableScript.greenWeapon){
                VariableScript.currWeapon = Instantiate(GreenWeapon, weaponSpawn.position, Quaternion.Euler(weaponRotation));
            }else if(weapon == 3 && VariableScript.redWeapon){
                VariableScript.currWeapon = Instantiate(RedWeapon, weaponSpawn.position, Quaternion.Euler(weaponRotation));
            }
            if(VariableScript.currWeapon){
                VariableScript.currWeapon.transform.parent = weaponSpawn;
            }
        }
     }

    void UpdateLook()
    {  
        look.x += Input.GetAxis("Mouse X") * mouseSensivity; 
        look.y += Input.GetAxis("Mouse Y") * mouseSensivity; 
        look.y = Mathf.Clamp(look.y, -90, 90);
        FirstCamera.transform.localRotation = Quaternion.Euler(-look.y, 0, 0); 
        transform.localRotation = Quaternion.Euler(0, look.x, 0); 
     }
    void UpdateMovement()
    { 
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        var isMoving = x != 0 || z!= 0;
        var input = new Vector3();
        input += transform.forward * z;
        input += transform.right * x;
        // input = Vector3.ClampMagnitude(input, 1f);

        if(!text.text.Equals("") && isMoving){
            text.text = "";
            targetImg.SetActive(true);
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y += jumpSpeed;
            anim.SetBool("BJump", true);
        }

        controller.Move((input * playerSpeed + velocity) * Time.deltaTime);
        if(isMoving && !anim.GetBool("BWalk")){
            anim.SetBool("BWalk", true);
        }
        if(!isMoving){
            anim.SetBool("BWalk", false);
        }
    }
    private void UpdateGravity()
    {    
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = controller.isGrounded ? -1 : velocity.y + gravity.y;
        if(anim.GetBool("BJump") && controller.isGrounded){
            anim.SetBool("BJump", false);
        }
    }

}

