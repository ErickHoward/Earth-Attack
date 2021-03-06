using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(this.name + "--bumped into--" + other.gameObject.name);
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log($"{this.name} ** Trigged by** {other.gameObject.name}");
    }
}
