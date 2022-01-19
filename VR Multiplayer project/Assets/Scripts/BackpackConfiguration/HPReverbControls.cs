using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class HPReverbControls : NetworkBehaviour
{
    public GameObject nurse;
    public GameObject agressor;
    long testcounter = 0;
    public GameObject textPopUp;
    public GameObject activeChoice;

    public Animator handAnimatorLeft;
    public Animator handAnimatorRight;

    public bool lastAudioPlayed = false;

    float timeRemaining = 5; //moet eventueel nog meer of minder

    bool firstTime = true;

    bool checkControllerInstantiation = false;
    bool checkControllerInstantiation1 = false;
    public bool uitlegKader = false;

    Color selectColor = new Color(0, 180, 207);

    AudioSource audioSource;

    Scene scene;

    public bool conversationEnded = false;

    private void Start()
    {
    }

    public void PressTrigger(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (this.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Agressor")
            {
                try
                {
                    GetAgressorActiveChoice();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex);
                }
            }

            if (this.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Nurse")
            {
                try
                {
                    GetNurseActiveChoice();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex);
                }
            }
        }
    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (lastAudioPlayed && this.isServer)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0 && scene.name == "ZiekenhuisKamer")
            {
                NetworkManager.singleton.ServerChangeScene("EndRoom");
            }
        }
    }

    public void Joystick(InputAction.CallbackContext context)
    {
    }

    public void GetNurseActiveChoice()
    {
        nurse = GameObject.FindGameObjectWithTag("Nurse");
        textPopUp = nurse.transform.parent.transform.GetChild(3).gameObject;

        if (textPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color == selectColor && nurse.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            if (uitlegKader == true)
            {
                this.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                activeChoice = textPopUp.transform.GetChild(0).gameObject;
                textPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
                textPopUp.SetActive(false);

                if (gameObject.GetComponent<NetworkIdentity>().isClient == true && ConversationManager.Instance.ActiveConversation == -1)
                {
                    CmdSetConversation(1);
                }

                if (gameObject.GetComponent<NetworkIdentity>().isClient == true && firstTime == false)
                {
                    CmdUpdateActiveElement(1);
                    if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Continue)
                    {

                        CmdUpdateAgressorText();
                    }
                }
                else if (firstTime)
                {
                    CmdUpdateAgressorText();
                    firstTime = false;
                }
            }
        }
        else if (textPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == selectColor && nurse.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            if (uitlegKader == true)
            {
                this.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                activeChoice = textPopUp.transform.GetChild(1).gameObject;
                textPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color = Color.white;
                textPopUp.SetActive(false);

                if (gameObject.GetComponent<NetworkIdentity>().isClient == true && ConversationManager.Instance.ActiveConversation == -1)
                {
                    CmdSetConversation(2);
                }

                if (gameObject.GetComponent<NetworkIdentity>().isClient == true && firstTime == false)
                {
                    CmdUpdateActiveElement(2);
                    if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Continue)
                    {

                        CmdUpdateAgressorText();
                    }
                }
                else if (firstTime)
                {
                    CmdUpdateAgressorText();
                    firstTime = false;
                }
            }
        }
        else if (textPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == selectColor && nurse.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            if (uitlegKader == true)
            {
                this.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                activeChoice = textPopUp.transform.GetChild(2).gameObject;
                textPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color = Color.white;
                textPopUp.SetActive(false);

                if (gameObject.GetComponent<NetworkIdentity>().isClient == true && ConversationManager.Instance.ActiveConversation == -1)
                {
                    CmdSetConversation(3);
                }

                if (gameObject.GetComponent<NetworkIdentity>().isClient == true && firstTime == false)
                {
                    CmdUpdateActiveElement(3);
                    if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Continue)
                    {

                        CmdUpdateAgressorText();

                    }
                }
                else if (firstTime)
                {
                    CmdUpdateAgressorText();
                    firstTime = false;
                }
            }
        }
        else
        {
            //Debug.Log("No Active Choice Found.");
        }
    }

    public void GetAgressorActiveChoice()
    {
        agressor = GameObject.FindGameObjectWithTag("Agressor");
        textPopUp = agressor.transform.parent.transform.GetChild(3).gameObject;

        if (textPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color == selectColor && agressor.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            if (uitlegKader == true)
            {
                this.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                activeChoice = textPopUp.transform.GetChild(0).gameObject;
                textPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
                textPopUp.SetActive(false);
                if (gameObject.GetComponent<NetworkIdentity>().isClient == true)
                {
                    ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
                    CmdUpdateActiveElement(1);
                    if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Continue)
                    {

                        CmdUpdateNurseText();
                    }
                }
            }
        }
        else if (textPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == selectColor && agressor.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            if (uitlegKader == true)
            {
                this.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                activeChoice = textPopUp.transform.GetChild(1).gameObject;
                textPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color = Color.white;
                textPopUp.SetActive(false);
                if (gameObject.GetComponent<NetworkIdentity>().isClient == true)
                {
                    ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
                    CmdUpdateActiveElement(2);
                    if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Continue)
                    {
                        CmdUpdateNurseText();
                    }
                }
            }
        }
        else if (textPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == selectColor && agressor.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            if (uitlegKader == true)
            {
                this.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                activeChoice = textPopUp.transform.GetChild(2).gameObject;
                textPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color = Color.white;
                textPopUp.SetActive(false);
                if (gameObject.GetComponent<NetworkIdentity>().isClient == true)
                {
                    ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
                    CmdUpdateActiveElement(3);
                    if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Continue)
                    {
                        CmdUpdateNurseText();
                    }
                }
            }
        }
        else
        {
            //Debug.Log("No Active Choice Found.");
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdSetConversation(int currentConversation)
    {
        if (gameObject.GetComponent<NetworkIdentity>().isServer)
        {
            ConversationManager.Instance.ActiveConversation = currentConversation;
            ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
            NetworkIdentity nurseID = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
            NetworkIdentity AgressorID = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
            TargetSetConversationNurse(nurseID.connectionToClient, currentConversation);
            TargetSetConversationAgressor(AgressorID.connectionToClient, currentConversation);
        }
    }

    [TargetRpc]
    public void TargetSetConversationNurse(NetworkConnection target, int currentConversation)
    {
        nurse = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject;
        if (nurse.GetComponent<NetworkIdentity>().isClient && nurse.GetComponent<NetworkIdentity>().isLocalPlayer && nurse.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Nurse")
        {
            ConversationManager.Instance.ActiveConversation = currentConversation;
        }
    }

    [TargetRpc]
    public void TargetSetConversationAgressor(NetworkConnection target, int currentConversation)
    {
        agressor = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject;
        if (agressor.GetComponent<NetworkIdentity>().isClient && agressor.GetComponent<NetworkIdentity>().isLocalPlayer && agressor.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Agressor")
        {
            ConversationManager.Instance.ActiveConversation = currentConversation;
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateAgressorText()
    {
        if (uitlegKader == false)
        {
            ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
            NetworkIdentity AgressorID = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
            NetworkIdentity nurseID = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
            TargetUpdateAgressorText(AgressorID.connectionToClient);
            TargetPlayAudioOnSender(nurseID.connectionToClient);
        }
    }

    [TargetRpc]
    public void TargetUpdateAgressorText(NetworkConnection target)
    {
        if (ConversationManager.Instance.GetActiveConversation().activeElement.AState == ConversationElement.ActiveState.Continue)
        {
            agressor = GameObject.FindGameObjectWithTag("Agressor").gameObject;
            textPopUp = agressor.transform.parent.transform.GetChild(3).gameObject;
            ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;

            GameObject server = GameObject.FindGameObjectWithTag("Server");

            audioSource = server.GetComponent<AudioSource>();

            audioSource.clip = ConversationManager.Instance.GetActiveConversation().activeElement.TextToSpeech;

            audioSource.Play();

            textPopUp.SetActive(true);

            if (ConversationManager.Instance.ActiveReactionElements.Count == 3)
            {
                if (ConversationManager.Instance.ActiveReactionElements[0].EState == ConversationElement.ElementState.Agressive)
                {
                    textPopUp.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Agressive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[0].EState == ConversationElement.ElementState.Defensive)
                {
                    textPopUp.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Defensive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[0].EState == ConversationElement.ElementState.Neutral)
                {
                    textPopUp.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Neutral");
                }
                textPopUp.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = ConversationManager.Instance.ActiveReactionElements[0].Text;

                if (ConversationManager.Instance.ActiveReactionElements[1].EState == ConversationElement.ElementState.Agressive)
                {
                    textPopUp.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Agressive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[1].EState == ConversationElement.ElementState.Defensive)
                {
                    textPopUp.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Defensive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[1].EState == ConversationElement.ElementState.Neutral)
                {
                    textPopUp.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Neutral");
                }
                textPopUp.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = ConversationManager.Instance.ActiveReactionElements[1].Text;

                if (ConversationManager.Instance.ActiveReactionElements[2].EState == ConversationElement.ElementState.Agressive)
                {
                    textPopUp.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Agressive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[2].EState == ConversationElement.ElementState.Defensive)
                {
                    textPopUp.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Defensive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[2].EState == ConversationElement.ElementState.Neutral)
                {
                    textPopUp.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Neutral");
                }
                textPopUp.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = ConversationManager.Instance.ActiveReactionElements[2].Text;
            }
            else if (ConversationManager.Instance.ActiveReactionElements.Count == 2)
            {
                if (ConversationManager.Instance.ActiveReactionElements[0].EState == ConversationElement.ElementState.Agressive)
                {
                    textPopUp.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Agressive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[0].EState == ConversationElement.ElementState.Defensive)
                {
                    textPopUp.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Defensive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[0].EState == ConversationElement.ElementState.Neutral)
                {
                    textPopUp.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Neutral");
                }
                textPopUp.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = ConversationManager.Instance.ActiveReactionElements[0].Text;

                textPopUp.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Neutral");

                textPopUp.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = "";

                if (ConversationManager.Instance.ActiveReactionElements[1].EState == ConversationElement.ElementState.Agressive)
                {
                    textPopUp.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Agressive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[1].EState == ConversationElement.ElementState.Defensive)
                {
                    textPopUp.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Defensive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[1].EState == ConversationElement.ElementState.Neutral)
                {
                    textPopUp.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Neutral");
                }
                textPopUp.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = ConversationManager.Instance.ActiveReactionElements[1].Text;
            }
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateNurseText()
    {
        if (uitlegKader == false)
        {
            ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
            NetworkIdentity nurseID = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
            NetworkIdentity agressorID = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
            TargetUpdateNurseText(nurseID.connectionToClient);
            TargetPlayAudioOnSender(agressorID.connectionToClient);
        }
    }

    [TargetRpc]
    public void TargetUpdateNurseText(NetworkConnection target)
    {
        if (ConversationManager.Instance.GetActiveConversation().activeElement.AState == ConversationElement.ActiveState.Continue)
        {
            nurse = GameObject.FindGameObjectWithTag("Nurse").gameObject;
            textPopUp = nurse.transform.parent.transform.GetChild(3).gameObject;
            ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;

            GameObject server = GameObject.FindGameObjectWithTag("Server");

            audioSource = server.GetComponent<AudioSource>();

            audioSource.clip = ConversationManager.Instance.GetActiveConversation().activeElement.TextToSpeech;

            audioSource.Play();

            textPopUp.SetActive(true);

            if (ConversationManager.Instance.ActiveReactionElements.Count == 3)
            {
                if (ConversationManager.Instance.ActiveReactionElements[0].EState == ConversationElement.ElementState.Agressive)
                {
                    textPopUp.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Agressive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[0].EState == ConversationElement.ElementState.Defensive)
                {
                    textPopUp.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Defensive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[0].EState == ConversationElement.ElementState.Neutral)
                {
                    textPopUp.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Neutral");
                }
                textPopUp.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = ConversationManager.Instance.ActiveReactionElements[0].Text;

                if (ConversationManager.Instance.ActiveReactionElements[1].EState == ConversationElement.ElementState.Agressive)
                {
                    textPopUp.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Agressive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[1].EState == ConversationElement.ElementState.Defensive)
                {
                    textPopUp.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Defensive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[1].EState == ConversationElement.ElementState.Neutral)
                {
                    textPopUp.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Neutral");
                }
                textPopUp.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = ConversationManager.Instance.ActiveReactionElements[1].Text;

                if (ConversationManager.Instance.ActiveReactionElements[2].EState == ConversationElement.ElementState.Agressive)
                {
                    textPopUp.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Agressive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[2].EState == ConversationElement.ElementState.Defensive)
                {
                    textPopUp.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Defensive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[2].EState == ConversationElement.ElementState.Neutral)
                {
                    textPopUp.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Neutral");
                }
                textPopUp.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = ConversationManager.Instance.ActiveReactionElements[2].Text;
            }
            else if (ConversationManager.Instance.ActiveReactionElements.Count == 2)
            {
                if (ConversationManager.Instance.ActiveReactionElements[0].EState == ConversationElement.ElementState.Agressive)
                {
                    textPopUp.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Agressive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[0].EState == ConversationElement.ElementState.Defensive)
                {
                    textPopUp.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Defensive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[0].EState == ConversationElement.ElementState.Neutral)
                {
                    textPopUp.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Neutral");
                }
                textPopUp.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = ConversationManager.Instance.ActiveReactionElements[0].Text;

                textPopUp.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Neutral");

                textPopUp.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = "";

                if (ConversationManager.Instance.ActiveReactionElements[1].EState == ConversationElement.ElementState.Agressive)
                {
                    textPopUp.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Agressive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[1].EState == ConversationElement.ElementState.Defensive)
                {
                    textPopUp.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Defensive");
                }
                else if (ConversationManager.Instance.ActiveReactionElements[1].EState == ConversationElement.ElementState.Neutral)
                {
                    textPopUp.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Neutral");
                }
                textPopUp.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = ConversationManager.Instance.ActiveReactionElements[1].Text;
            }
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateActiveElement(int activeChoice)
    {
        if (uitlegKader == false)
        {
            if (this.isServer)
            {
                ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
                if (ConversationManager.Instance.ActiveReactionElements.Count == 3)
                {
                    if (activeChoice == 1)
                    {
                        ConversationManager.Instance.GetActiveConversation().activeElement = ConversationManager.Instance.ActiveReactionElements[0];
                    }
                    if (activeChoice == 2)
                    {
                        ConversationManager.Instance.GetActiveConversation().activeElement = ConversationManager.Instance.ActiveReactionElements[1];
                    }
                    if (activeChoice == 3)
                    {
                        ConversationManager.Instance.GetActiveConversation().activeElement = ConversationManager.Instance.ActiveReactionElements[2];
                    }
                }
                else if (ConversationManager.Instance.ActiveReactionElements.Count == 2)
                {
                    if (activeChoice == 1)
                    {
                        ConversationManager.Instance.GetActiveConversation().activeElement = ConversationManager.Instance.ActiveReactionElements[0];
                    }
                    if (activeChoice == 3)
                    {
                        ConversationManager.Instance.GetActiveConversation().activeElement = ConversationManager.Instance.ActiveReactionElements[1];
                    }
                }
                NetworkIdentity nurseID = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
                NetworkIdentity agressorID = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();


                TargetUpdateActiveElementNurse(nurseID.connectionToClient, activeChoice);
                TargetUpdateActiveElementAgressor(agressorID.connectionToClient, activeChoice);

                if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Ended)
                {

                    TargetPlayAudioOnSender(agressorID.connectionToClient);
                    TargetPlayAudioOnSender(nurseID.connectionToClient);

                    lastAudioPlayed = true;

                    conversationEnded = true;
                }

                if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Phase2)
                {
                    uitlegKader = true;
                    TargetPlayAudioOnSender(agressorID.connectionToClient);
                    TargetPlayAudioOnSender(nurseID.connectionToClient);

                    GameObject timer = GameObject.Find("Timer");
                    timer.GetComponent<Timer>().TimerIsRunning = true;

                    TargetStartTimer(agressorID.connectionToClient);
                    TargetStartTimer(nurseID.connectionToClient);
                }
            }
        }
    }

    [TargetRpc]
    public void TargetUpdateActiveElementNurse(NetworkConnection target, int activeChoice)
    {
        ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
        if (ConversationManager.Instance.ActiveReactionElements.Count == 3)
        {
            if (activeChoice == 1)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = ConversationManager.Instance.ActiveReactionElements[0];
            }
            if (activeChoice == 2)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = ConversationManager.Instance.ActiveReactionElements[1];
            }
            if (activeChoice == 3)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = ConversationManager.Instance.ActiveReactionElements[2];
            }
        }
        else if (ConversationManager.Instance.ActiveReactionElements.Count == 2)
        {
            if (activeChoice == 1)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = ConversationManager.Instance.ActiveReactionElements[0];
            }
            if (activeChoice == 3)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = ConversationManager.Instance.ActiveReactionElements[1];
            }
        }

        if (ConversationManager.Instance.GetActiveConversation().activeElement.AState == ConversationElement.ActiveState.Phase2)
        {
            uitlegKader = true;
        }
    }

    [TargetRpc]
    public void TargetUpdateActiveElementAgressor(NetworkConnection target, int activeChoice)
    {
        ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
        if (ConversationManager.Instance.ActiveReactionElements.Count == 3)
        {
            if (activeChoice == 1)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = ConversationManager.Instance.ActiveReactionElements[0];
            }
            if (activeChoice == 2)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = ConversationManager.Instance.ActiveReactionElements[1];
            }
            if (activeChoice == 3)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = ConversationManager.Instance.ActiveReactionElements[2];
            }
        }
        else if (ConversationManager.Instance.ActiveReactionElements.Count == 2)
        {
            if (activeChoice == 1)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = ConversationManager.Instance.ActiveReactionElements[0];
            }
            if (activeChoice == 3)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = ConversationManager.Instance.ActiveReactionElements[1];
            }
        }
        if (ConversationManager.Instance.GetActiveConversation().activeElement.AState == ConversationElement.ActiveState.Phase2)
        {
            uitlegKader = true;
        }
    }

    [TargetRpc]
    public void TargetPlayAudioOnSender(NetworkConnection target)
    {
        GameObject server = GameObject.FindGameObjectWithTag("Server");

        audioSource = server.GetComponent<AudioSource>();

        if (ConversationManager.Instance.GetActiveConversation().activeElement.AState == ConversationElement.ActiveState.Ended || ConversationManager.Instance.GetActiveConversation().activeElement.AState == ConversationElement.ActiveState.Phase2)
        {
            uitlegKader = true;
            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (ConversationManager.Instance.GetActiveConversation().activeElement.AState == ConversationElement.ActiveState.Ended)
                {
                    audioSource.clip = ConversationManager.Instance.GetActiveConversation().activeElement.TextToSpeech;

                    audioSource.Play();

                    player.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
                    player.GetComponent<HPReverbControls>().conversationEnded = true;
                }
                else if (ConversationManager.Instance.GetActiveConversation().activeElement.AState == ConversationElement.ActiveState.Phase2)
                {
                    player.GetComponent<HPReverbControls>().uitlegKader = true;
                    if (player.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Agressor" && player.GetComponent<NetworkIdentity>().isLocalPlayer)
                    {
                        audioSource.clip = ConversationManager.Instance.GetActiveConversation().activeElement.TextToSpeech;

                        audioSource.Play();

                        player.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
                        player.transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = "";
                        player.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "";
                        player.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false);
                        player.transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);
                        player.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(true);
                        player.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Neutral");
                        player.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = ConversationElementInitializer.UitlegFase2Agressor().Text;
                        uitlegKader = true;
                    }

                    if (player.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Nurse" && player.GetComponent<NetworkIdentity>().isLocalPlayer)
                    {
                        audioSource.clip = ConversationManager.Instance.GetActiveConversation().activeElement.TextToSpeech;

                        player.GetComponent<HPReverbControls>().uitlegKader = true;

                        audioSource.Play();

                        player.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
                        player.transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = "";
                        player.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "";
                        player.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false);
                        player.transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);
                        player.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(true);
                        player.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Style/VRpleegkunde_SpeechBubbles_WithLogo_Neutral");
                        player.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = ConversationElementInitializer.UitlegFase2Nurse().Text;
                        uitlegKader = true;
                    }
                }
            }
        }

        if (uitlegKader == false)
        {
            audioSource.clip = ConversationManager.Instance.GetActiveConversation().activeElement.TextToSpeech;

            audioSource.Play();
        }
    }

    [TargetRpc]
    public void TargetStartTimer(NetworkConnection target)
    {
        uitlegKader = true;
        GameObject timer = GameObject.Find("Timer");
        timer.GetComponent<Timer>().TimerIsRunning = true;
    }
}


