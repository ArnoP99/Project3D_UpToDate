using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private static readonly object padlock = new object();

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

    private void Update()
    {
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
        else if(button == 2)
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

        if(nursePlayer == true)
        {
            Debug.Log("Conversation Started.");
            ConversationManager.StartConversation();
        }

    }
}
