using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] Vector2 controlSpeed = new Vector2(30f,30f);
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
        float newXPos = transform.localPosition.x + xOffset;

        float yOffset = yThrow * Time.deltaTime * controlSpeed.y;
        float newYPos = transform.localPosition.y + yOffset;
        transform.localPosition = new Vector3 
        (
            newXPos,
            newYPos,
            transform.localPosition.z
        );
        
    }
}
