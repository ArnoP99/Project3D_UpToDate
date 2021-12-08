using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConversationManager : NetworkBehaviour
{
    private static ConversationManager instance = null;
    private static readonly object padlock = new object();

    public Conversation[] allConversations;
    public int activeConversation;
    public List<GameObject> conversationParticipants = new List<GameObject>();
    public GameObject activeParticipant;
    public List<ConversationElement> activeReactionElements = new List<ConversationElement>();


    public Conversation generalCheckUpCv;
    public Conversation timeForMedicationCv;
    public Conversation helpButtonCv;

    public ConversationManager()
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
                    instance = GameObject.Find("ConversationManager").GetComponent<ConversationManager>();
                }
                return instance;
            }
        }
    }

    // Initialize different Conversations that will be used in the game
    private void Start()
    {
        ConversationElementInitializer.SetReactionElements();

        allConversations = new Conversation[3];

        activeConversation = -1;

        generalCheckUpCv = new Conversation();
        timeForMedicationCv = new Conversation();
        helpButtonCv = new Conversation();

        generalCheckUpCv.StartElement = ConversationElementInitializer.GeneralCheckupConversation();
        generalCheckUpCv.ActiveElement = generalCheckUpCv.StartElement;

        timeForMedicationCv.StartElement = ConversationElementInitializer.TimeForMedicationConversation();
        timeForMedicationCv.ActiveElement = timeForMedicationCv.StartElement;

        helpButtonCv.StartElement = ConversationElementInitializer.HelpButtonConversation();
        helpButtonCv.ActiveElement = helpButtonCv.StartElement;

        allConversations[0] = generalCheckUpCv;
        allConversations[1] = timeForMedicationCv;
        allConversations[2] = helpButtonCv;
    }

    public void StartConversation(GameObject nurse)
    {
        conversationParticipants.Add(nurse);
        activeParticipant = nurse;

        nurse.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
        nurse.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = ConversationElementInitializer.GeneralCheckupConversation().Text;
        nurse.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = ConversationElementInitializer.TimeForMedicationConversation().Text;
        nurse.transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = ConversationElementInitializer.HelpButtonConversation().Text;
    }

    private void EndConversation(Conversation conversationToEnd)
    {

    }

    private void UpdateConversation(Conversation conversationToUpdate)
    {
        if (conversationToUpdate.CurrentState == Conversation.ConversationState.Ended)
        {

        }
    }

    public void Update()
    {
    }

    public void SetConversation(int choice)
    {
        if (this.isServer)
        {
            if (activeConversation == -1)
            {
                if (choice == 1)
                {
                    activeConversation = 1;
                }
                else if (choice == 2)
                {
                    activeConversation = 2;
                }
                else if (choice == 3)
                {
                    activeConversation = 3;
                }
                Debug.Log(choice);
            }
        }
    }

    public Conversation GetActiveConversation()
    {
        Debug.Log("Executed GAC");
        if (activeConversation == 1)
        {
            Debug.Log(generalCheckUpCv.ToString());
            return generalCheckUpCv;
        }
        else if (activeConversation == 2)
        {
            Debug.Log(timeForMedicationCv.ToString());
            return timeForMedicationCv;
        }
        else if (activeConversation == 3)
        {
            Debug.Log(helpButtonCv.ToString());
            return helpButtonCv;
        }

        Debug.Log("No active conversation found.");
        return null;
    }

    //[Command(requiresAuthority = false)]
    //public void CmdStartConversation(GameObject nurse)
    //{
    //    Debug.Log("During cmd: " + nurse);
    //    TargetStartConversation(nurse.GetComponent<NetworkIdentity>().connectionToServer, nurse);
    //    Debug.Log("After TargetRpc: " + nurse);
    //}

    //[TargetRpc]
    //public void TargetStartConversation(NetworkConnection target, GameObject nurse)
    //{
    //    Debug.Log("During TargetRpc: " + nurse);

    //}

    //pass choice to server with an int and then from server to agressor

    public Conversation GeneralCheckupConversation
    {
        get
        {
            return generalCheckUpCv;
        }
    }

    public Conversation TimeForMedicationConversation
    {
        get
        {
            return timeForMedicationCv;
        }
    }

    public Conversation HelpButtonConversation
    {
        get
        {
            return helpButtonCv;
        }
    }

    public int ActiveConversation
    {
        get
        {
            return activeConversation;
        }
        set
        {
            activeConversation = value;
        }
    }

    public List<ConversationElement> ActiveReactionElements
    {
        get
        {
            return activeReactionElements;
        }
        set
        {
            activeReactionElements = value;
        }
    }
}
