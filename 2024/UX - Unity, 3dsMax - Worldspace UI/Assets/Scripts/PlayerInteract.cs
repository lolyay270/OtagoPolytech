using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract Instance;

    Camera cam;
    InputAction interact;

    [SerializeField] float interactionDistance;
    public float InteractionDistance { get { return interactionDistance; } }

    //Awake runs when game starts, before Start method
    void Awake()
    {
        if (Instance == null) Instance = this; 

        cam = GetComponentInChildren<Camera>();

        interact = InputSystem.actions.FindAction("Interact");
    }

    //Start runs on first frame
    void Start()
    {
        interact.started += ctx => { HandleInteract(); };
    }

    //Update runs every frame after Start method
    void Update()
    {
        GameManager.Instance.clickHeld = interact.IsInProgress();
    }

    //called when player presses an interact button
    void HandleInteract()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            GameObject obj = hit.collider.gameObject;
            try 
            {
                Interactable inter = obj.GetComponent<Interactable>(); 
                GameManager.Instance.OnInteractableClick.Invoke(inter); //? makes the event only fire if it has any listeners
            } 
            catch 
            {
                print("GO=  " + obj.name + "    no interactable present");
            }
        }
    }
}
