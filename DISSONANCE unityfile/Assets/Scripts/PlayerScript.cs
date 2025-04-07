using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    // -{ Enum States }- \\
    private enum State
    {
        Walk,
        Sprint,
        Idle,
    }

    private State currentState = State.Idle;

    // -{Vectors}- \\
    Vector2 movement;
    Vector2 mousemovement;

    // -{ Serialized Fields}- \\

    [SerializeField]
    float speed = 2.0f;

    [SerializeField]
    float mouseSensitivity = 100;

    [SerializeField]
    GameObject cam;

    [SerializeField]
    float gravityVal = 9.8f;
    
    [SerializeField]
    AudioSource walkingfootsteps;

    [SerializeField]
    AudioSource runningfootsteps;

    // -{ Bools }- \\

    bool isRunning = false;
    bool isWalking = false;
    bool hasJumped = false;

    // -{ Remaining Variables}- \\

    CharacterController controller;
    float cameraUpRotation = 0;
    float ySpeed = 0;

    float JumpHeight = 1.0f;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case State.Idle:
                OnIdle();
                print("idle");
                break;
            case State.Walk:
                print("walking");
                OnWalk();
                break;
            case State.Sprint:
                print("sprinting");
                OnRun();
                break;

        }
        float lookX = mousemovement.x * Time.deltaTime * mouseSensitivity;
        float lookY = mousemovement.y * Time.deltaTime * mouseSensitivity;

        cameraUpRotation -= lookY;
        cameraUpRotation = Mathf.Clamp(cameraUpRotation, -90, 90);

        cam.transform.localRotation = Quaternion.Euler(cameraUpRotation, 0, 0);

        transform.Rotate(Vector3.up * lookX);
    }


    void OnLook(InputValue lookVal)
    {
        mousemovement = lookVal.Get<Vector2>();
        print(mousemovement);

    }

    void OnMove(InputValue moveVal)
    {
        movement = moveVal.Get<Vector2>();
    }

    void MovePlayer(float player_speed)
    {
        float moveX = movement.x;
        float moveZ = movement.y;

        Vector3 applied_movement = new Vector3 (moveX, 0, moveZ);
        controller.Move(applied_movement * Time.deltaTime * speed);

    }


    // -States- \\

    void OnRun()
    {
        //Audio
        runningfootsteps.volume = 1;
        walkingfootsteps.volume = 0;
        MovePlayer(speed * 2);

        if (!Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            currentState = State.Idle;
        }

        if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            currentState = State.Walk;
        }
    }

    void OnWalk()
    {
        //Audio
        walkingfootsteps.volume = 1;
        runningfootsteps.volume = 0;
        MovePlayer(speed);
        isWalking = true;
        isRunning = false;

        if (isWalking = true && Input.GetKey(KeyCode.LeftShift))
        {
            currentState = State.Sprint;
            isRunning = true;
        }
    }

    void OnIdle()
    {
        //Audio
        runningfootsteps.volume = 0;
        walkingfootsteps.volume = 0;
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            currentState = State.Walk;
            isWalking = true;
        }
    }



}