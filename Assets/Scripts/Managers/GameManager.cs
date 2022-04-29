using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Camera mainCamera { get; private set; }

    private void Awake()
    {
        mainCamera = Camera.main;
    }
}
