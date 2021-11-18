using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    private static GameManager instance = null;
    private static readonly object padlock = new object();

    NetworkManager networkManager;

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
        networkManager = new NetworkManager();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
    }


    public static void PrepNextScene(int playerRole)
    {
        bool nurseOnSpawn = false;
        bool agressorOnSpawn = false;
        
        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        if (playerRole == 1)
        {
            if (nurseOnSpawn == false)
            {
                Debug.Log("Check Nurse");
                nurseOnSpawn = true;
            }
            else
            {
                nurseOnSpawn = false;
            }
        }
        else if (playerRole == 2)
        {
            if (agressorOnSpawn == false)
            {
                Debug.Log("Check Agressor");
                agressorOnSpawn = true;
            }
            else
            {
                agressorOnSpawn = false;
            }
        }

        if (nurseOnSpawn == true && agressorOnSpawn == true)
        {
            Debug.Log("Networkserver active: " + NetworkServer.active);
            //Debug.Log("IsClient: " + (this == isClient));
            //Debug.Log("IsServer: " + (this == isServer));

            if (NetworkServer.active)
            {
                NetworkManager.singleton.ServerChangeScene("ZiekenhuisKamer");
            }
        }
    }

    // Check if there are 2 different players in the game (Nurse & Agressor) and if they are both present, start a new conversation.
    public static void CheckForTwoPlayers(int button)
    {
        bool nursePlayer = false;
        bool agressorPlayer = false;

        if (button == 1)
        {
            nursePlayer = true;
        }
        else if (button == 2)
        {
            agressorPlayer = true;
        }
        else
        {
            Debug.Log("Invalid number received from button! Check if the correct numbers are passed from each button ...");
        }

        /*if(nursePlayer == true && agressorPlayer == true)
        {
            Debug.Log("Conversation Started.");
            ConversationManager.StartConversation(nurse, agressor);
        }*/

        if (nursePlayer == true)
        {
            Debug.Log("Conversation Started.");
            ConversationManager.StartConversation();
        }

    }
}
