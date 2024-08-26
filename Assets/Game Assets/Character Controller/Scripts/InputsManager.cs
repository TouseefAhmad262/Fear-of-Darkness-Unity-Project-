using GameControlls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsManager : MonoBehaviour
{
    public static InputsManager Instance;

    [HideInInspector]
    public PlayerControls Controls;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Instance = this;

        Controls = new PlayerControls();
    }

    void OnEnable()
    {
        Controls.Enable();
    }

    void OnDisable()
    {
        Controls.Disable();
    }
}