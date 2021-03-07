using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    private void OnTriggerEnter(Collider other) 
    {
        switch (other.gameObject.tag)
        {
            case "Terrain":
                break;
            case "Friendly":
                print("This thing is friendly");
                break;
            default:
                StartCrashSequence();
                break;
        }

    }
    private void StartCrashSequence()
    {
        GetComponent<PlayerControls>().enabled = false;
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    private void ReloadLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }
}
