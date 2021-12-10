using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationElement
{
    public enum ElementState
    {
        Neutral,
        Agressive,
        Defensive
    }

    public enum UserState
    {
        Nurse,
        Agressor,
        Anyone
    }

    private string text;
    private ElementState elementState;
    private List<ConversationElement> reactionElements = new List<ConversationElement>();
    private UserState userstate;
    private AudioClip textToSpeech;

    public ConversationElement()
    {

    }

    public ConversationElement(string m_text, ElementState m_elementState, UserState m_userState)
    {
        text = m_text;
        elementState = m_elementState;
        reactionElements = new List<ConversationElement>();
        userstate = m_userState;
        textToSpeech = null;
    }

    public ConversationElement(string m_text, ElementState m_elementState, UserState m_userState, AudioClip m_audio)
    {
        text = m_text;
        elementState = m_elementState;
        reactionElements = new List<ConversationElement>();
        userstate = m_userState;
        textToSpeech = m_audio;
    }

    public void AddElementToReactions(ConversationElement reactie)
    {
        reactionElements.Add(reactie);
    }

    public string Text
    {
        get
        {
            return text;
        }
    }

    public List<ConversationElement> ReactionElements
    {
        get
        {
            return reactionElements;
        }
    }

    public AudioClip TextToSpeech
    {
        get
        {
            return textToSpeech;
        }
    }

}
