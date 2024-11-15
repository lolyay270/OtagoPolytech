using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UnityEvent<Interactable> OnInteractableClick = new();

    [SerializeField] GameObject player;
    public bool clickHeld;

    Interactable lastClickedObj;

    [Header("Range Hood")]
    [SerializeField][Tooltip("Buttons to change fan speed; off, then lowest to highest")]
        List<Interactable> fanButtons = new();
    [SerializeField][Tooltip("Fan rotation speeds; off, then lowest to highest")]
        List<float> fanSpeeds = new();
    [SerializeField] GameObject fanBlade;
    [SerializeField] Interactable lightButton;
    [SerializeField] List<Light> hoodLights;
    [SerializeField][Tooltip("How far buttons move, from on to off")] 
        float postitionDif;
    float currentFanSpeed;

    [Header("Room Lights")]
    [SerializeField] Interactable lightSwitch;
    [SerializeField][Tooltip("How far to rotate from on to off")] 
        float switchAngleDif;
    [SerializeField] Interactable lightDial;
    [SerializeField][Tooltip("Lowest rotation value, mirrored to max rotation value")] 
        float dialMinRotate;
    [SerializeField][Tooltip("Allow for raycasts to hit light switch and wall")] 
        float raycastPadding;
    [SerializeField] List<Light> roomLights;
    [SerializeField] Material lightOn;
    [SerializeField] Material lightOff;
    Material currentlightMat;
    float currentIntensity;


    //runs when game starts, before Start method
    void Awake()
    {
        //singleton setup
        if (Instance == null) Instance = this; 
        else Destroy(gameObject);

        Application.targetFrameRate = 60;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        OnInteractableClick.AddListener(HandleInteractClicked);
    }

    //runs first frame
    void Start()
    {
        currentFanSpeed = fanSpeeds[0];
        currentIntensity = 1;
        currentlightMat = lightOn;

        //lightDial rotate to correct pos at start of game
    }

    // runs every frame after Start method
    void Update()
    {
        fanBlade.transform.Rotate(0, 0, currentFanSpeed * Time.deltaTime);

        if (clickHeld) HandleInteractHeld();
    }

    // called when Interactable gets clicked by player
    void HandleInteractClicked(Interactable obj)
    {
        lastClickedObj = obj;

        //range hood
        if (fanButtons.Contains(obj)) FanSpeedButtons(obj);
        else if (obj == lightButton) RangeHoodLight();

        //room hoodLights
        else if (obj == lightSwitch) RoomLightSwitch();
        else if (obj == lightDial) 
        { 
            //hover effect??? doesnt show cause other popup message
            //UIManager.Instance.ShowPopupText("Click and hold dial to fade the lights.");
            //do nothing else since handled by HandleInteractHeld
        }

        else print("What was that click??? nvm it mustve been the wind. It couldnt have been " + obj.name);
    }

    //called when Interactable click is held by player
    void HandleInteractHeld()
    {
        if (lastClickedObj == lightDial) RoomLightDial();
    }

    // allows fan to change speed
    void FanSpeedButtons(Interactable obj)
    {
        int oldIndex = fanSpeeds.FindIndex((x) => x == currentFanSpeed);
        int newIndex = fanButtons.FindIndex((x) => x == obj);

        //push old button out, new button in
        fanButtons[oldIndex].transform.Translate(Vector3.up * postitionDif);
        fanButtons[newIndex].transform.Translate(Vector3.down * postitionDif);

        //change fan speed to new button
        currentFanSpeed = fanSpeeds[newIndex];

        //popup message depending on which button pressed
        string popupText = "";

        if (oldIndex == newIndex) popupText += "Fan speed is already ";
        else popupText += "Setting fan speed to ";

        if (obj == fanButtons[0])
        {
            if (oldIndex == newIndex) popupText = "Fan is already turned off."; //assigning, not adding to
            else popupText = "Turning fan off.";
        }
        else if (obj == fanButtons[1]) popupText += "low.";
        else if (obj == fanButtons[fanButtons.Count - 1]) popupText += "high.";
        else popupText += newIndex.ToString() + "."; //handle if more than 3 speeds added

        UIManager.Instance.ShowPopupText(popupText);
        print(popupText);
    }

    // allows range hood light toggle
    void RangeHoodLight()
    {
        foreach (Light light in hoodLights) light.enabled = !light.enabled;
        if (hoodLights[0].enabled)
        {
            lightButton.transform.Translate(Vector3.down * postitionDif);
            UIManager.Instance.ShowPopupText("Turning rangehood lights on.");
        }
        else
        {
            lightButton.transform.Translate(Vector3.up * postitionDif);
            UIManager.Instance.ShowPopupText("Turning rangehood lights off.");
        }
    }

    // allows room hoodLights toggle
    void RoomLightSwitch()
    {
        if (lightSwitch.transform.rotation.eulerAngles.z < 180) //currently on, turning off, z = 8
        {
            lightSwitch.transform.Rotate(0, 0, -switchAngleDif);
            currentlightMat = lightOff;
            UIManager.Instance.ShowPopupText("Turning room lights off.");
        }
        else //currently off, turning on, z = 352 = -8
        {
            lightSwitch.transform.Rotate(0, 0, switchAngleDif);
            currentlightMat = lightOn;
            UIManager.Instance.ShowPopupText("Turning room lights on.");
        }

        //swap sphere materials (emmision/no emmision)
        //if (currentlightMat == lightOn) currentlightMat = lightOff;
        //else if (currentlightMat == lightOff) 

        foreach (Light light in roomLights) // change each light
        {
            light.enabled = !light.enabled;
            light.GetComponentInChildren<MeshRenderer>().material = currentlightMat;
        }
    }

    // allows room hoodLights fade
    void RoomLightDial()
    {
        //get mouse position in world
        Ray ray = player.GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, player.GetComponent<PlayerInteract>().InteractionDistance))
        {
            //rotate dial to point at mouse pos
            Vector3 dialPos = lightDial.transform.position;
            float angle = Mathf.Rad2Deg * (Mathf.Atan2(hit.point.y - dialPos.y, hit.point.x - dialPos.x)) + 180;
            if (angle > dialMinRotate && angle < 180 - dialMinRotate) { }// if in deadzone, do nothing
            else lightDial.transform.rotation = Quaternion.Euler(0, 0, angle);

            //set light intensity to rotation
            if (angle < 90) angle += 360; //stopping angle jumping from 360 to 0 mid range (90 is direct down)
            float a = 360 + dialMinRotate; //lowest in new range (was min rotate)
            float b = 180 - dialMinRotate; //highest in new range 
            float percent = (angle - a) / (b - a);
            currentIntensity = Mathf.Clamp01(percent);
            foreach (Light light in roomLights) light.intensity = currentIntensity;

        }
        UIManager.Instance.ShowPopupText("Lights set at " + Mathf.Round(currentIntensity * 100) + "%");
    }

    void DialRotateToIntensity()
    {

    }
}
