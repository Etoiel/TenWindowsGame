using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

interface IInteractable {
    public void Interact();
}
public class PlayerOnClickInteraction : MonoBehaviour
{
    // Declare a variable to hold the PlayerInputActions instance
    private GameUnityInputSystem inputActions;

    // Flag to control if the coroutine is allowed to run
    private bool coroutineAllowed = true;
    // Counter to keep track of the number of clicks
    private int clickCounter = 0;
    // Variable to store the time of the first click
    private float firstClickTime;
    public Transform InteractorSource;
    public float InteractRange;

    // This method is called when the script instance is being loaded
    private void Awake()
    {
        // Initialize the PlayerInputActions instance
        inputActions = new GameUnityInputSystem();
    }

    // This method is called when the object becomes enabled and active
    private void OnEnable()
    {
        // Enable the Gameplay action map
        inputActions.Player.Enable();
        // Subscribe to the performed event of the Interact action
        inputActions.Player.Interact.performed += OnInteract;
    }

    // This method is called when the object becomes disabled or inactive
    private void OnDisable()
    {
        // Unsubscribe from the performed event of the Interact action
        inputActions.Player.Interact.performed -= OnInteract;
        // Disable the Gameplay action map
        inputActions.Player.Disable();
    }

    // Callback method for when the Interact action is performed
    private void OnInteract(InputAction.CallbackContext context)
    {
        // Increment the click counter
        clickCounter += 1;
        // Check if it's the first click and if the coroutine is allowed to run
        if (clickCounter == 1 && coroutineAllowed)
        {
            // Store the time of the first click
            firstClickTime = Time.time;
            // Start the coroutine to detect double clicks
            StartCoroutine(DoubleClickDetection());
        }
    }

    // Coroutine to detect double clicks
    private IEnumerator DoubleClickDetection()
    {
        // Prevent the coroutine from running multiple times
        coroutineAllowed = false;
        // Loop until the allowed time for double click detection has passed
        while (Time.time - firstClickTime < 0.5f)
        {
            // Check if the second click has occurred
            if (clickCounter == 2)
            {
                Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
                Debug.Log("Double Click Detected!");
                if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
                {
                    Debug.DrawLine(r.origin, hitInfo.point);
                    Debug.Log("Tripple Click Detected!");
                    if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                    {
                        interactObj.Interact();
                        // Log the detection of a double click
                        Debug.Log("Mega Click Detected!");
                        // Add your double click logic here
                        break;
                    }
                }

            }
            // Wait for the next frame
            yield return null;
        }

        // Reset the click counter and allow the coroutine to run again
        clickCounter = 0;
        coroutineAllowed = true;
    }
}
