using UnityEngine;
using System.Collections.Generic;
public class FmsScript : MonoBehaviour
{
    CharacterController controller;
    public Transform cameraTransform; 
    public float playerSpeed = 5;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;

    public float mouseSensivity = 3; 
    Vector2 look;

    Animator anim;
    public GameObject player;

    Vector3 velocity; 
    float mass = 1f;
    public float jumpSpeed = 5f;

    bool textShow = true;
    public GameObject text;
    public GameObject targetImg;

    public LayerMask layerMaskWeapon;

    public GameObject blueImg;
    public GameObject greenImg;
    public GameObject redImg;

    public GameObject BlueWeapon;
    public GameObject GreenWeapon;
    public GameObject RedWeapon;
    public GameObject currentWeapon;

    public Transform weaponSpawn;


    private void Awake()
    {
        // targetImg.SetActive(false);
        controller = GetComponent<CharacterController>();
    }
    void Start()
    {
        anim = player.GetComponent<Animator> ();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        UpdateLook(); 
        UpdateMovement(); 
        UpdateGravity(); 
  

        if(Input.GetKeyDown(KeyCode.E)){
            float pickUpDistance = 3f;
            if(Physics.Raycast(targetImg.transform.position, targetImg.transform.forward, out RaycastHit raycastHit, pickUpDistance, layerMaskWeapon)){
                Destroy(raycastHit.transform.parent.gameObject);
                if(raycastHit.transform.name.StartsWith("B")){
                    blueImg.SetActive(true);
                }else if(raycastHit.transform.name.StartsWith("G")){
                    greenImg.SetActive(true);
                }else{
                    redImg.SetActive(true);
                }
                
            }
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
        

        if(Input.GetMouseButtonDown(0) && currentWeapon){
            if(anim.GetBool("BShoot")){
                anim.SetBool("BShoot", false);
            }else{
                anim.SetBool("BShoot", true);
            }
        }
     }

     void replaceWeapon(int weapon){
        if(!anim.GetBool("BShoot")){
            if(currentWeapon){
                Destroy(currentWeapon);
            }
            Vector3 weaponRotation = new Vector3(90, 0, 0);
            if(weapon == 1 && blueImg.activeSelf){
                currentWeapon = Instantiate(BlueWeapon, weaponSpawn.position, Quaternion.Euler(weaponRotation));
            }else if(weapon == 2 && greenImg.activeSelf){
                currentWeapon = Instantiate(GreenWeapon, weaponSpawn.position, Quaternion.Euler(weaponRotation));
            }else if(weapon == 3 && redImg.activeSelf){
                currentWeapon = Instantiate(RedWeapon, weaponSpawn.position, Quaternion.Euler(weaponRotation));
            }
            currentWeapon.transform.parent = weaponSpawn;
        }
     }

    void UpdateLook()
    {  
        look.x += Input.GetAxis("Mouse X") * mouseSensivity; 
        look.y += Input.GetAxis("Mouse Y") * mouseSensivity; 
        look.y = Mathf.Clamp(look.y, -90, 90);
        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0); 
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
        input = Vector3.ClampMagnitude(input, 1f);

        if(textShow && isMoving){
            Destroy(text);
            textShow = false;
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
        // if(isMoving){
        //     anim.SetFloat ("Blend", playerSpeed, VerticalAnimTime, Time.deltaTime * 2f);
        // }else{
        //     anim.SetFloat ("Blend", 0, VerticalAnimTime, Time.deltaTime * 2f);
        // }
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

