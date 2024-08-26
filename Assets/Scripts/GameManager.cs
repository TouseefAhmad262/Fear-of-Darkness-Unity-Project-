using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;

    void Awake()
    {
        Instance = this;
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}