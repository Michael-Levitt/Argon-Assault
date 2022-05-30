using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    float xThrow, yThrow;
    
    [Header("Translation Tuning Variables")]
    [SerializeField] float controlSpeed = 30f;
    [SerializeField] float xRange = 8f;
    [SerializeField] float yRangeMin = -1f;
    [SerializeField] float yRangeMax = 11f;

    [Header("Rotation Tuning Variables")]
    [SerializeField] float positionPitchFactor = -2f; // Negative to keep pointing straight forwards(ish)
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 1.5f; 
    [SerializeField] float controlRollFactor = -10f;

    [Header("Lasers")]
    [SerializeField] GameObject[] lasers;

    void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        // Get x and y input (see player rig for controls)
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        // Turn x input into new x position
        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        // Turn y input into new y position
        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, yRangeMin, yRangeMax);

        // Generate new position vector relative to player rig
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        // Try to keep player pointed forwards wherever on the screen they are
        
        // Modify pitch based on screen position and y input
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToThrow;
        
        // Modify yaw based on screen position
        float yaw = transform.localPosition.x * positionYawFactor;

        // Modify roll based on x input
        float roll = xThrow * controlRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        // Fires lasers when "fire" button is pressed
        if(fire.ReadValue<float>() > 0.5)
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            // Stops emitting when not firing but lasers don't disappear instantly
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
