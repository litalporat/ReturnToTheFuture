using UnityEngine;
public class FmsScript : MonoBehaviour
{
    CharacterController controller;
    public Transform cameraTransform; 
    public float playerSpeed = 5;
    public float StartAnimTime = 0.3f;
    public float StopAnimTime = 0.15f;
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;

    public float mouseSensivity = 3; 
    Vector2 look;

    public Animator anim;
    public GameObject player;

    Vector3 velocity; 
    float mass = 1f;
    public float jumpSpeed = 5f;

    private void Awake()
    {
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


        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y += jumpSpeed;
        }
        controller.Move((input * playerSpeed + velocity) * Time.deltaTime);
        if(isMoving){
            anim.SetFloat ("Blend", playerSpeed, VerticalAnimTime, Time.deltaTime * 2f);
        }else{
            anim.SetFloat ("Blend", 0, VerticalAnimTime, Time.deltaTime * 2f);
        }
    }
    private void UpdateGravity()
    {    
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = controller.isGrounded ? -1 : velocity.y + gravity.y;
    }
}

