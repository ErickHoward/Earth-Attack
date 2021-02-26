using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] Vector2 controlSpeed = new Vector2(30f,30f);
    [SerializeField] Vector2 playerShipXRange; // x value is how far left the ship can go, should be a minus number, y is how far right the ship can go, should be a positive number
    [SerializeField] Vector2 playerShipYRange; // x value is how far down the ship can go, should be a minus number, y is how far up the ship can go, should be a positive number
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() 
    {
        movement.Enable();
    }
    private void OnDisable() 
    {
        movement.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        float xThrow = movement.ReadValue<Vector2>().x;
        float yThrow = movement.ReadValue<Vector2>().y;

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
}
