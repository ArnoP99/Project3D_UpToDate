using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{

    private static ConversationManager instance = null;
    private static readonly object padlock = new object();

    private List<Conversation> allConversations;
    private Conversation activeConversation;
    private static List<GameObject> conversationParticipants = new List<GameObject>();
    private static GameObject activeParticipant;

    private static Conversation generalCheckUp;

    private ConversationManager()
    {

    }

    public static ConversationManager Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ConversationManager();
                }
                return instance;
            }
        }
    }

    // Initialize different Conversations that will be used in the game
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        generalCheckUp = new Conversation();

        generalCheckUp.StartElement = ConversationElementInitializer.GeneralCheckupConversation();
        generalCheckUp.ActiveElement = generalCheckUp.StartElement;
    }

    public static void StartConversation()
    {
            GameObject nurse = GameObject.FindGameObjectWithTag("Nurse").gameObject;
            Debug.Log(nurse.tag);
            conversationParticipants.Add(nurse);

            activeParticipant = nurse;
            nurse.gameObject.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshPro>().text = generalCheckUp.StartElement.Text;
    }

    private void EndConversation(Conversation conversationToEnd)
    {

    }

    private void ChooseConversation(Conversation chosenConversation)
    {

    }

    private void UpdateConversation(Conversation conversationToUpdate)
    {
        if (conversationToUpdate.CurrentState == Conversation.ConversationState.Ended)
        {

        }
    }
}
