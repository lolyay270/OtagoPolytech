using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerControls : MonoBehaviour
{
    Camera cam;
    InputAction move;
    InputAction look;

    [SerializeField] float moveSpeed;
    [SerializeField] float mouseLookSpeed;
    [SerializeField] float controllerLookSpeed;
    [SerializeField] float maxVertAngle;
    [SerializeField] float controllerDeadZone;

    bool inputIsDelta;
    float yaw;

    // Awake runs on game start
    void Awake()
    {
        cam = GetComponentInChildren<Camera>();

        move = InputSystem.actions.FindAction("Move");
        look = InputSystem.actions.FindAction("Look");
    }

    // Start runs on first frame (after Awake)
    void Start()
    {
        //listener to change if controller or mouse used
        look.performed += ctx => { inputIsDelta = ctx.control.name == "delta"; }; 
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
        FixTransform(); // required to stop weird collisions
    }

    void Movement()
    {
        Vector2 move2D = StopStickWander(move.ReadValue<Vector2>());
        Vector3 moveVector = new(move2D.x, 0, move2D.y);
        moveVector *= moveSpeed * Time.deltaTime;
        transform.Translate(moveVector);
    }

    void Rotation()
    {   
        Vector2 lookVector = StopStickWander(look.ReadValue<Vector2>());
        if (!inputIsDelta) lookVector *= Time.deltaTime * controllerLookSpeed; // mulitply deltaTime for non mouse inputs
        else lookVector *= mouseLookSpeed;

        // player horizontal (y axis)
        transform.Rotate(0, lookVector.x, 0); // input x axis

        // camera vertical (x axis)
        yaw += -lookVector.y * mouseLookSpeed; //input y axis
        yaw = Mathf.Clamp(yaw, -maxVertAngle, maxVertAngle);
        cam.transform.localRotation = Quaternion.Euler(yaw, 0, 0); //want to set exact value to stop looking upsidedown
    }

    void FixTransform()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, 0.9f, pos.z);

        float rot = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, rot, 0);
    }

    // mitigate old controller sticks that sometimes dont return to center
    Vector2 StopStickWander(Vector2 vector)
    {
        if (Mathf.Abs(vector.x) < controllerDeadZone) vector.x = 0;
        if (Mathf.Abs(vector.y) < controllerDeadZone) vector.y = 0;

        return vector;
    }
}
