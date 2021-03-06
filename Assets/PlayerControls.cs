using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    [SerializeField] Vector2 controlSpeed = new Vector2(30f,30f);
    [SerializeField] Vector2 playerShipXRange; // x value is how far left the ship can go, should be a minus number, y is how far right the ship can go, should be a positive number
    [SerializeField] Vector2 playerShipYRange; // x value is how far down the ship can go, should be a minus number, y is how far up the ship can go, should be a positive number
    [SerializeField] GameObject[] lasers;


    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -7f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;
    
    float xThrow, yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() 
    {
        movement.Enable();
        fire.Enable();
    }
    private void OnDisable() 
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

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        
        float pitch =  pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * controlSpeed.x;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, playerShipXRange.x, playerShipXRange.y);

        float yOffset = yThrow * Time.deltaTime * controlSpeed.y;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, playerShipYRange.x, playerShipYRange.y);

        transform.localPosition = new Vector3
        (
            clampedXPos,
            clampedYPos,
            transform.localPosition.z
        );
    }

    private void ProcessFiring()
    {
        if (fire.ReadValue<float>() > 0.5)
        {
            ActiveLasers();
            
        }
        else
        {
            DeactivateLasers();
            
        }
    }

    private void ActiveLasers()
    {
        foreach(GameObject laser in lasers)
        {
            laser.SetActive(true);
        }
    }

    private void DeactivateLasers()
    {
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false);
        }
    }
}
