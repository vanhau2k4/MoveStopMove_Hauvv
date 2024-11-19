using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string KilledName = "";
    public int scoin;
    public int buyScoin;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
