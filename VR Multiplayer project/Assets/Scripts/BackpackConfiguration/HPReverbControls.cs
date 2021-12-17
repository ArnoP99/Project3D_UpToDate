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

    public GameObject textPopUp;
    public GameObject activeChoice;

    public bool lastAudioPlayed = false;

    float timeRemaining = 5; //moet eventueel nog meer of minder

    bool firstTime = true;

    Color selectColor = new Color(0, 180, 207);

    AudioSource audioSource;

    Scene scene;

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

    public void PrimaryButton(InputAction.CallbackContext context)
    {
    }

    public void GripButton(InputAction.CallbackContext context)
    {
        Debug.Log("Gripbutton Pressed");
        if (context.started)
        {
            Debug.Log("Gripbutton Pressed inside");
            if (context.control.device.name == "HPReverbG2ControllerOpenXR")
            {
                this.GetComponent<AssignAuth>().ExecuteCmdHandGoesPoof(0, gameObject);
            }
            else if (context.control.device.name == "HPReverbG2ControllerOpenXR1")
            {
                this.GetComponent<AssignAuth>().ExecuteCmdHandGoesPoof(1, gameObject);
            }
        }
        if (context.performed)
        {
            Debug.Log("Gripbutton released");
            if (context.control.device.name == "HPReverbG2ControllerOpenXR")
            {
                this.GetComponent<AssignAuth>().ExecuteCmdHandComesBack(0, gameObject);
            }
            else if (context.control.device.name == "HPReverbG2ControllerOpenXR1")
            {
                this.GetComponent<AssignAuth>().ExecuteCmdHandComesBack(1, gameObject);
            }
        }
    }

    public void GetNurseActiveChoice()
    {
        nurse = GameObject.FindGameObjectWithTag("Nurse");
        textPopUp = nurse.transform.parent.transform.GetChild(3).gameObject;

        if (textPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color == selectColor && nurse.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            activeChoice = textPopUp.transform.GetChild(0).gameObject;
            textPopUp.SetActive(false);

            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && ConversationManager.Instance.ActiveConversation == -1)
            {
                CmdSetConversation(1);
            }

            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && firstTime == false)
            {
                CmdUpdateActiveElement(1);
                Debug.Log("active element nurse:" + ConversationManager.Instance.GetActiveConversation().ActiveElement.Text);
                if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Continue)
                {

                    CmdUpdateAgressorText();
                }
                Debug.Log("active element nurse 2de check:" + ConversationManager.Instance.GetActiveConversation().ActiveElement.Text);
            }
            else if (firstTime)
            {
                CmdUpdateAgressorText();
                firstTime = false;
            }
        }
        else if (textPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == selectColor && nurse.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            activeChoice = textPopUp.transform.GetChild(1).gameObject;
            textPopUp.SetActive(false);

            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && ConversationManager.Instance.ActiveConversation == -1)
            {
                CmdSetConversation(2);
            }

            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && firstTime == false)
            {
                CmdUpdateActiveElement(2);
                Debug.Log("active element nurse:" + ConversationManager.Instance.GetActiveConversation().ActiveElement.Text);
                if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Continue)
                {

                    CmdUpdateAgressorText();
                }
                Debug.Log("active element nurse 2de check:" + ConversationManager.Instance.GetActiveConversation().ActiveElement.Text);
            }
            else if (firstTime)
            {
                CmdUpdateAgressorText();
                firstTime = false;
            }
        }
        else if (textPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == selectColor && nurse.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            activeChoice = textPopUp.transform.GetChild(2).gameObject;
            textPopUp.SetActive(false);

            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && ConversationManager.Instance.ActiveConversation == -1)
            {
                CmdSetConversation(3);
            }

            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && firstTime == false)
            {
                CmdUpdateActiveElement(3);
                Debug.Log("active element nurse:" + ConversationManager.Instance.GetActiveConversation().ActiveElement.Text);
                if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Continue)
                {

                    CmdUpdateAgressorText();

                }

                Debug.Log("active element nurse 2de check:" + ConversationManager.Instance.GetActiveConversation().ActiveElement.Text);
            }
            else if (firstTime)
            {
                CmdUpdateAgressorText();
                firstTime = false;
            }
        }
        else
        {
            Debug.Log("No Active Choice Found.");
        }
    }

    public void GetAgressorActiveChoice()
    {
        agressor = GameObject.FindGameObjectWithTag("Agressor");
        textPopUp = agressor.transform.parent.transform.GetChild(3).gameObject;

        if (textPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color == selectColor && agressor.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            activeChoice = textPopUp.transform.GetChild(0).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true)
            {
                Debug.Log("AgressorChoice ARE: " + ConversationManager.Instance.ActiveReactionElements.Count);
                Debug.Log("AgressorChoice CVM RE: " + ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements.Count);
                Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement.Text);
                ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
                Debug.Log("AgressorChoice ARE: " + ConversationManager.Instance.ActiveReactionElements.Count);
                CmdUpdateActiveElement(1);
                if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Continue)
                {

                    CmdUpdateNurseText();
                }
            }
        }
        else if (textPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == selectColor && agressor.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            activeChoice = textPopUp.transform.GetChild(1).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true)
            {
                Debug.Log("AgressorChoice ARE: " + ConversationManager.Instance.ActiveReactionElements.Count);
                Debug.Log("AgressorChoice CVM RE: " + ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements.Count);
                Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement.Text);
                ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
                Debug.Log("AgressorChoice ARE: " + ConversationManager.Instance.ActiveReactionElements.Count);
                CmdUpdateActiveElement(2);
                if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Continue)
                {

                    CmdUpdateNurseText();
                }
            }
        }
        else if (textPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == selectColor && agressor.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            activeChoice = textPopUp.transform.GetChild(2).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true)
            {
                Debug.Log("AgressorChoice ARE: " + ConversationManager.Instance.ActiveReactionElements.Count);
                Debug.Log("AgressorChoice CVM RE: " + ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements.Count);
                Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement.Text);
                ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
                Debug.Log("AgressorChoice ARE: " + ConversationManager.Instance.ActiveReactionElements.Count);
                CmdUpdateActiveElement(3);
                if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Continue)
                {

                    CmdUpdateNurseText();
                }
            }
        }
        else
        {
            Debug.Log("No Active Choice Found.");
        }

        Debug.Log("ActiveChoice Agressor: " + activeChoice);
    }

    [Command(requiresAuthority = false)]
    public void CmdSetConversation(int currentConversation)
    {
        if (gameObject.GetComponent<NetworkIdentity>().isServer)
        {
            ConversationManager.Instance.ActiveConversation = currentConversation;
            ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
            Debug.Log(ConversationManager.Instance.ActiveConversation);
            Debug.Log("Cmd SCV: " + ConversationManager.Instance.ActiveReactionElements.Count);
            NetworkIdentity nurseID = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
            NetworkIdentity AgressorID = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
            TargetSetConversationNurse(nurseID.connectionToClient, currentConversation);
            TargetSetConversationAgressor(AgressorID.connectionToClient, currentConversation);
        }
    }

    [TargetRpc]
    public void TargetSetConversationNurse(NetworkConnection target, int currentConversation)
    {
        Debug.Log("hallo nurse");
        nurse = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject;
        if (nurse.GetComponent<NetworkIdentity>().isClient && nurse.GetComponent<NetworkIdentity>().isLocalPlayer && nurse.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Nurse")
        {
            ConversationManager.Instance.ActiveConversation = currentConversation;
            Debug.Log("Active conversation nurse set");
        }
    }

    [TargetRpc]
    public void TargetSetConversationAgressor(NetworkConnection target, int currentConversation)
    {
        Debug.Log("hallo agressor");
        agressor = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject;
        if (agressor.GetComponent<NetworkIdentity>().isClient && agressor.GetComponent<NetworkIdentity>().isLocalPlayer && agressor.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Agressor")
        {
            ConversationManager.Instance.ActiveConversation = currentConversation;
            Debug.Log("Active conversation agressor set");
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateAgressorText()
    {
        ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
        Debug.Log("Cmd UAT: " + ConversationManager.Instance.ActiveReactionElements.Count);
        NetworkIdentity AgressorID = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
        NetworkIdentity nurseID = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
        TargetUpdateAgressorText(AgressorID.connectionToClient);
        TargetPlayAudioOnSender(nurseID.connectionToClient);
    }

    [TargetRpc]
    public void TargetUpdateAgressorText(NetworkConnection target)
    {
        if (ConversationManager.Instance.GetActiveConversation().activeElement.AState == ConversationElement.ActiveState.Continue)
        {
            agressor = GameObject.FindGameObjectWithTag("Agressor").gameObject;
            textPopUp = agressor.transform.parent.transform.GetChild(3).gameObject;
            Debug.Log(ConversationManager.Instance.ActiveReactionElements.Count);
            Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement.Text);
            ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
            Debug.Log("Target UAT: " + ConversationManager.Instance.ActiveReactionElements.Count);

            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player.GetComponent<NetworkIdentity>().isLocalPlayer == false && player.GetComponent<NetworkIdentity>().netId == 15)
                {
                    Debug.Log("audiosource set");
                    audioSource = player.GetComponent<AudioSource>();
                }
            }
            Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement);
            Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement.TextToSpeech);
            audioSource.clip = ConversationManager.Instance.GetActiveConversation().activeElement.TextToSpeech;

            Debug.Log("play audio starting");
            Debug.Log(audioSource.isActiveAndEnabled);
            audioSource.Play();
            Debug.Log("play audio finished");

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
        ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
        Debug.Log("Cmd UNT: " + ConversationManager.Instance.ActiveReactionElements.Count);
        NetworkIdentity nurseID = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
        NetworkIdentity agressorID = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
        TargetUpdateNurseText(nurseID.connectionToClient);
        TargetPlayAudioOnSender(agressorID.connectionToClient);
    }

    [TargetRpc]
    public void TargetUpdateNurseText(NetworkConnection target)
    {
        if (ConversationManager.Instance.GetActiveConversation().activeElement.AState == ConversationElement.ActiveState.Continue)
        {
            nurse = GameObject.FindGameObjectWithTag("Nurse").gameObject;
            textPopUp = nurse.transform.parent.transform.GetChild(3).gameObject;
            Debug.Log(ConversationManager.Instance.ActiveReactionElements.Count);
            Debug.Log(ConversationManager.Instance.GetActiveConversation());
            Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement.Text);
            Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements.Count);
            ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
            Debug.Log("Target UNT: " + ConversationManager.Instance.ActiveReactionElements.Count);

            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player.GetComponent<NetworkIdentity>().isLocalPlayer == false && player.GetComponent<NetworkIdentity>().netId == 15)
                {
                    Debug.Log("audiosource set");
                    audioSource = player.GetComponent<AudioSource>();
                }
            }
            Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement);
            Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement.TextToSpeech);
            audioSource.clip = ConversationManager.Instance.GetActiveConversation().activeElement.TextToSpeech;

            Debug.Log("play audio starting");
            Debug.Log(audioSource.isActiveAndEnabled);
            audioSource.Play();
            Debug.Log("play audio finished");
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

        if (this.isServer)
        {

            ConversationManager.Instance.ActiveReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
            Debug.Log("Cmd UAE: " + ConversationManager.Instance.ActiveReactionElements.Count);
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
            Debug.Log("Updated active element on server");
            NetworkIdentity nurseID = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
            NetworkIdentity agressorID = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
            TargetUpdateActiveElementNurse(nurseID.connectionToClient, activeChoice);
            TargetUpdateActiveElementAgressor(agressorID.connectionToClient, activeChoice);

            if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Ended)
            {

                TargetPlayAudioOnSender(agressorID.connectionToClient);
                TargetPlayAudioOnSender(nurseID.connectionToClient);
                lastAudioPlayed = true;
            }

            if (ConversationManager.Instance.GetActiveConversation().ActiveElement.AState == ConversationElement.ActiveState.Phase2)
            {
                Debug.Log("phase 2 started");
            }


        }
    }

    [TargetRpc]
    public void TargetUpdateActiveElementNurse(NetworkConnection target, int activeChoice)
    {
        Debug.Log("Updated active element on nurse");
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
        Debug.Log("active element nurse targetRPC: " + ConversationManager.Instance.GetActiveConversation().activeElement);
    }

    [TargetRpc]
    public void TargetUpdateActiveElementAgressor(NetworkConnection target, int activeChoice)
    {
        Debug.Log("Updated active element on agressor");
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
    }
    [TargetRpc]
    public void TargetPlayAudioOnSender(NetworkConnection target)
    {
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer == false && player.GetComponent<NetworkIdentity>().netId == 15)
            {
                Debug.Log("audiosource set");
                audioSource = player.GetComponent<AudioSource>();
            }
        }
        Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement);
        Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement.TextToSpeech);
        audioSource.clip = ConversationManager.Instance.GetActiveConversation().activeElement.TextToSpeech;

        Debug.Log("play audio starting");
        Debug.Log(audioSource.isActiveAndEnabled);
        audioSource.Play();
        Debug.Log("play audio finished");
        if (ConversationManager.Instance.GetActiveConversation().activeElement.AState == ConversationElement.ActiveState.Ended || ConversationManager.Instance.GetActiveConversation().activeElement.AState == ConversationElement.ActiveState.Phase2)
        {
            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
            {
                player.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }
}


