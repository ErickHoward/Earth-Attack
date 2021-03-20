using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [SerializeField]
    private InputAction movement;
    [SerializeField] private InputAction fire;
    [Tooltip("How fast the ship moves")] [SerializeField]
    private Vector2 controlSpeed = new Vector2(30f, 30f);
    [Tooltip("How far the player can move left and right, x value is how far left the ship can go, should be a minus number, y is how far right the ship can go, should be a positive number")]
    [SerializeField]
    private Vector2 playerShipXRange;
    [Tooltip("How far the player can move up and down,  x value is how far down the ship can go, should be a minus number, y is how far up the ship can go, should be a positive number")]
    [SerializeField]
    private Vector2 playerShipYRange;
    [Tooltip("A array of lasers of types GameObject")] [SerializeField]
    private GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField]
    private float positionPitchFactor = -2f;
    [SerializeField] private float positionYawFactor = 2f;

    [Header("Player input based tuning")]
    [SerializeField]
    private float controlPitchFactor = -7f;
    [SerializeField] private float controlRollFactor = -20f;

    private float xThrow, yThrow;

    // Start is called before the first frame update
    private void Start()
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
    private void Update()
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
            SetLasersActive(true);
            
        }
        else
        {
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isActive)
    {
        foreach(GameObject laser in lasers)
        {
            var emmissionModule = laser.GetComponent<ParticleSystem>().emission;
            emmissionModule.enabled = isActive;
        }
    }
}
