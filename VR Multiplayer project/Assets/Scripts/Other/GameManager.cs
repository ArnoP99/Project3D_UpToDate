using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    private static GameManager instance = null;
    private static readonly object padlock = new object();

    private bool nurseOnSpawn = false;
    private bool agressorOnSpawn = false;

    private GameManager()
    {
    }

    public static GameManager Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void GoToHospitalRoom(int onSpawnCheck, int active)
    {
        if (onSpawnCheck == 1 && active == 1)
        {
            nurseOnSpawn = true;
        }
        else if (onSpawnCheck == 1 && active == 0)
        {
            nurseOnSpawn = false;
        }

        if (onSpawnCheck == 2 && active == 1)
        {
            agressorOnSpawn = true;
        }
        else if (onSpawnCheck == 2 && active == 0)
        {
            agressorOnSpawn = false;
        }

        if (nurseOnSpawn == true && agressorOnSpawn == true && this == isServer)
        {
            NetworkManager.singleton.ServerChangeScene("ZiekenhuisKamer");
        }
    }

    public void GoToIntroductionRoom(int onSpawnCheck, int active)
    {
        if (onSpawnCheck == 1 && active == 1)
        {
            nurseOnSpawn = true;
        }
        else if (onSpawnCheck == 1 && active == 0)
        {
            nurseOnSpawn = false;
        }

        if (onSpawnCheck == 2 && active == 1)
        {
            agressorOnSpawn = true;
        }
        else if (onSpawnCheck == 2 && active == 0)
        {
            agressorOnSpawn = false;
        }

        if (nurseOnSpawn == true && agressorOnSpawn == true && this == isServer)
        {
            NetworkManager.singleton.ServerChangeScene("IntroductionRoom");
        }
        //on enter true on exit false
    }
}
