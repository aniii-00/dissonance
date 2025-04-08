using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    // --{State Stuff :) }--

     private enum State
    {
        Walking,
        Running,
    }
//  -{ Variables}-    
    Vector2 movement;
    Vector2 mouseMovement;
    float cameraUpRotation = 0.0f;
    CharacterController controller;
    bool hasJumped = false;
    float ySpeed = 0;

    Vector3 actual_movement;

    private State currentState = State.Walking;

    bool walking = false;

    bool running = false;

    
    

 

// -{Serialize Fields}-


    [SerializeField]
    float speed = 2.0f;
    [SerializeField]
    float mouseSensitivity = 100;


    [SerializeField]
    GameObject cam;


    [SerializeField]
    float JumpHeight = 1.0f;

    [SerializeField]
    float gravityVal = 9.8f;

    [SerializeField]

    AudioSource walkingfootsteps;

    [SerializeField]

    AudioSource runningfootsteps;

    [SerializeField]
    float sprint = 2.0f;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Camera
        float lookX= mouseMovement.x * Time.deltaTime * mouseSensitivity;
        float lookY= mouseMovement.y * Time.deltaTime * mouseSensitivity;

        cameraUpRotation -= lookY;

        cameraUpRotation = Mathf.Clamp(cameraUpRotation , -90, 90);

        cam.transform.localRotation = Quaternion.Euler(cameraUpRotation,0,0);


       //Movement
        transform.Rotate(Vector3.up * lookX);
        float moveX = movement.x;
        float moveZ = movement.y;
        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            actual_movement = (transform.forward * moveZ) * sprint + (transform.right * moveX) * sprint;
        }
        else
        {
            actual_movement = (transform.forward * moveZ) + (transform.right * moveX);
        }
        

        //Jumping

        if (hasJumped)
        {
            hasJumped = false;
            ySpeed = JumpHeight;
            walkingfootsteps.volume = 0;
            runningfootsteps.volume = 0;
        }

        ySpeed -= gravityVal * Time.deltaTime; 
        actual_movement.y = ySpeed;
    
       controller.Move(actual_movement * speed * Time.deltaTime);


       //Footsteps

       switch(currentState)
        {
            case State.Walking:
                OnWalk();
                break;
            case State.Running:
                OnRun();
                break;
        }
       

         


    }

    void OnMove(InputValue moveVal)
    {
        movement = moveVal.Get<Vector2>();
        
        

    }

    void OnLook(InputValue lookVal)
    {
        mouseMovement = lookVal.Get<Vector2>();

    }

    void OnJump()
    {
        if (controller.isGrounded)
        {
            hasJumped = true;
            print("Jumping");   
        }
    }



    void OnRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            runningfootsteps.volume = 1;
            walkingfootsteps.volume = 0;
            running = true;
        }


        else
        {
            currentState=State.Walking;
            running = false;
            runningfootsteps.volume = 0;
            print("running");
        }
        
    }

    void OnWalk()
    {
        walkingfootsteps.volume = 0;
        runningfootsteps.volume = 0;
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
           walkingfootsteps.volume = 1;
           runningfootsteps.volume = 0;
           walking = true;

        }
        else
        {
            walkingfootsteps.volume = 0;
            walking = false;
        }

        if (walking = true && Input.GetKey(KeyCode.LeftShift) )
        {
            currentState = State.Running;
        }
    }

}
