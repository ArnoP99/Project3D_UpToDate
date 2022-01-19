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

    private int nurseScoreGM = 0;
    private int agressorScoreGM = 0;

    private int highObjectsAGM = 0;
    private int highObjectsNGM = 0;
    private int mediumObjectsAGM = 0;
    private int mediumObjectsNGM = 0;
    private int lowObjectsAGM = 0;
    private int lowObjectsNGM = 0;

    public GameManager()
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

    public int NurseScoreGM { get => nurseScoreGM; set => nurseScoreGM = value; }
    public int AgressorScoreGM { get => agressorScoreGM; set => agressorScoreGM = value; }
    public int HighObjectsAGM { get => highObjectsAGM; set => highObjectsAGM = value; }
    public int HighObjectsNGM { get => highObjectsNGM; set => highObjectsNGM = value; }
    public int MediumObjectsAGM { get => mediumObjectsAGM; set => mediumObjectsAGM = value; }
    public int MediumObjectsNGM { get => mediumObjectsNGM; set => mediumObjectsNGM = value; }
    public int LowObjectsAGM { get => lowObjectsAGM; set => lowObjectsAGM = value; }
    public int LowObjectsNGM { get => lowObjectsNGM; set => lowObjectsNGM = value; }

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
    }
}
