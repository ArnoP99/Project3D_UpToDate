using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation 
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

    public ConversationElement startElement;
    public ConversationElement activeElement;
    public ConversationStartUser startingUser;
    public ConversationState currentState;

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
