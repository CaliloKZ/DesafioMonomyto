using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera mainCamera { get; private set; }

    private void Awake()
    {
        mainCamera = Camera.main;
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Another instance of GameManager was found, destroying gameObject: " + gameObject.name);
            Destroy(gameObject);
        }
    }
}
