using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    public enum ConversationState
    {
        Started,
        Ended,
        ToBegin
    }

    public enum ConversationStartUser
    {
        Agressor,
        Nurse,
        Anyone
    }

    private ConversationElement startElement;
    private ConversationElement activeElement;
    private ConversationStartUser startingUser;
    private ConversationState currentState;

    public Conversation()
    {
        currentState = ConversationState.ToBegin;
        startElement = new ConversationElement();
        startingUser = ConversationStartUser.Nurse;
        activeElement = null;

    }
    

    private void SelectNextElement()
    {
        
    }

    public ConversationElement StartElement
    {
        get
        {
            return startElement;
        }
        set
        {
            startElement = value;
        }
    }

    public ConversationElement ActiveElement
    {
        get
        {
            return activeElement;
        }
        set
        {
            activeElement = value;
        }
    }

    public ConversationStartUser StartingUser
    {
        get
        {
            return startingUser;
        }
        set
        {
            startingUser = value;
        }
    }

    public ConversationState CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
        }
    }
}
