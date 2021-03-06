using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssignAuth : NetworkBehaviour
{
    public int nurseScore = 0;
    public int agressorScore = 0;

    public int highObjectsA = 0;
    public int highObjectsN = 0;
    public int mediumObjectsA = 0;
    public int mediumObjectsN = 0;
    public int lowObjectsA = 0;
    public int lowObjectsN = 0;

    GameObject gameManager;

    Scene scene;

    bool nurseWon = false;
    bool agressorWon = false;

    public void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }
    public void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "EndRoom")
        {
            if (gameObject.GetComponent<NetworkIdentity>().isLocalPlayer && gameObject.gameObject.tag == "Server")
            {
                RpcSendScores(gameManager.GetComponent<GameManager>().NurseScoreGM, gameManager.GetComponent<GameManager>().AgressorScoreGM, gameManager.GetComponent<GameManager>().HighObjectsNGM, gameManager.GetComponent<GameManager>().HighObjectsAGM, gameManager.GetComponent<GameManager>().MediumObjectsNGM, gameManager.GetComponent<GameManager>().MediumObjectsAGM, gameManager.GetComponent<GameManager>().LowObjectsNGM, gameManager.GetComponent<GameManager>().LowObjectsAGM);

                if (gameObject.GetComponent<HPReverbControls>().conversationEnded)
                {
                    if (ConversationManager.Instance.GetActiveConversation().activeElement.UState == ConversationElement.UserState.Nurse)
                    {
                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseLoses");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorWins");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2NurseEndedGood");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2AgressorEndedBad");
                    }
                    else
                    {
                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseWins");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorLoses");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2NurseEndedBad");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2AgressorEndedGood");
                    }
                }
                else
                {
                    if (gameManager.GetComponent<GameManager>().NurseScoreGM > gameManager.GetComponent<GameManager>().AgressorScoreGM)
                    {
                        nurseWon = true;
                    }
                    else
                    {
                        agressorWon = true;
                    }

                    if (nurseWon)
                    {
                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseWins");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorLoses");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Nurse");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Agressor");


                        GameObject.Find("HighScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().HighObjectsNGM.ToString();
                        GameObject.Find("MediumScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().MediumObjectsNGM.ToString();
                        GameObject.Find("LowScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().LowObjectsNGM.ToString();
                        GameObject.Find("TotalScoreNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().NurseScoreGM.ToString();

                        GameObject.Find("HighScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().HighObjectsAGM.ToString();
                        GameObject.Find("MediumScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().MediumObjectsAGM.ToString();
                        GameObject.Find("LowScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().LowObjectsAGM.ToString();
                        GameObject.Find("TotalScoreAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().AgressorScoreGM.ToString();
                    }
                    else
                    {
                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseLoses");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorWins");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Nurse");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Agressor");

                        GameObject.Find("HighScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().HighObjectsNGM.ToString();
                        GameObject.Find("MediumScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().MediumObjectsNGM.ToString();
                        GameObject.Find("LowScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().LowObjectsNGM.ToString();
                        GameObject.Find("TotalScoreNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().NurseScoreGM.ToString();

                        GameObject.Find("HighScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().HighObjectsAGM.ToString();
                        GameObject.Find("MediumScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().MediumObjectsAGM.ToString();
                        GameObject.Find("LowScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().LowObjectsAGM.ToString();
                        GameObject.Find("TotalScoreAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().AgressorScoreGM.ToString();
                    }
                }

            }
            if (gameObject.GetComponent<NetworkIdentity>().isLocalPlayer && gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Agressor")
            {
                if (gameObject.GetComponent<HPReverbControls>().conversationEnded)
                {
                    if (ConversationManager.Instance.GetActiveConversation().activeElement.UState == ConversationElement.UserState.Nurse)
                    {
                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseLoses");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorWins");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2NurseEndedBad");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2AgressorEndedGood");
                    }
                    else
                    {
                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseWins");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorLoses");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2NurseEndedGood");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2AgressorEndedBad");
                    }
                }
                else
                {
                    if (gameManager.GetComponent<GameManager>().NurseScoreGM > gameManager.GetComponent<GameManager>().AgressorScoreGM)
                    {
                        nurseWon = true;
                    }
                    else
                    {
                        agressorWon = true;
                    }

                    if (nurseWon)
                    {

                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseWins");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorLoses");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Nurse");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Agressor");

                        GameObject.Find("HighScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().HighObjectsNGM.ToString();
                        GameObject.Find("MediumScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().MediumObjectsNGM.ToString();
                        GameObject.Find("LowScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().LowObjectsNGM.ToString();
                        GameObject.Find("TotalScoreNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().NurseScoreGM.ToString();

                        GameObject.Find("HighScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().HighObjectsAGM.ToString();
                        GameObject.Find("MediumScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().MediumObjectsAGM.ToString();
                        GameObject.Find("LowScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().LowObjectsAGM.ToString();
                        GameObject.Find("TotalScoreAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().AgressorScoreGM.ToString();
                    }
                    else
                    {

                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseLoses");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorWins");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Nurse");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Agressor");
                        GameObject.Find("HighScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().HighObjectsNGM.ToString();
                        GameObject.Find("MediumScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().MediumObjectsNGM.ToString();
                        GameObject.Find("LowScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().LowObjectsNGM.ToString();
                        GameObject.Find("TotalScoreNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().NurseScoreGM.ToString();

                        GameObject.Find("HighScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().HighObjectsAGM.ToString();
                        GameObject.Find("MediumScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().MediumObjectsAGM.ToString();
                        GameObject.Find("LowScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().LowObjectsAGM.ToString();
                        GameObject.Find("TotalScoreAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().AgressorScoreGM.ToString();
                    }
                }

            }
            else if (gameObject.GetComponent<NetworkIdentity>().isLocalPlayer && gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Nurse")
            {
                if (gameObject.GetComponent<HPReverbControls>().conversationEnded)
                {
                    if (ConversationManager.Instance.GetActiveConversation().activeElement.UState == ConversationElement.UserState.Nurse)
                    {
                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseLoses");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorWins");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2NurseEndedBad");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2AgressorEndedGood");
                    }
                    else
                    {
                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseWins");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorLoses");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2NurseEndedGood");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2AgressorEndedBad");
                    }
                }
                else
                {
                    if (gameManager.GetComponent<GameManager>().NurseScoreGM > gameManager.GetComponent<GameManager>().AgressorScoreGM)
                    {
                        nurseWon = true;
                    }
                    else
                    {
                        agressorWon = true;
                    }

                    if (nurseWon)
                    {
                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseWins");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorLoses");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Nurse");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Agressor");

                        GameObject.Find("HighScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().HighObjectsNGM.ToString();
                        GameObject.Find("MediumScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().MediumObjectsNGM.ToString();
                        GameObject.Find("LowScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().LowObjectsNGM.ToString();
                        GameObject.Find("TotalScoreNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().NurseScoreGM.ToString();

                        GameObject.Find("HighScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().HighObjectsAGM.ToString();
                        GameObject.Find("MediumScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().MediumObjectsAGM.ToString();
                        GameObject.Find("LowScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().LowObjectsAGM.ToString();
                        GameObject.Find("TotalScoreAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().AgressorScoreGM.ToString();
                    }
                    else
                    {

                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseLoses");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorWins");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Nurse");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Agressor");

                        GameObject.Find("HighScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().HighObjectsNGM.ToString();
                        GameObject.Find("MediumScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().MediumObjectsNGM.ToString();
                        GameObject.Find("LowScoreTextNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().LowObjectsNGM.ToString();
                        GameObject.Find("TotalScoreNurse").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().NurseScoreGM.ToString();

                        GameObject.Find("HighScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().HighObjectsAGM.ToString();
                        GameObject.Find("MediumScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().MediumObjectsAGM.ToString();
                        GameObject.Find("LowScoreTextAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().LowObjectsAGM.ToString();
                        GameObject.Find("TotalScoreAgressor").GetComponent<TextMeshPro>().text = gameManager.GetComponent<GameManager>().AgressorScoreGM.ToString();
                    }
                }
            }
        }
    }

    public void ExecuteCmdHandGoesPoof(int hand, GameObject player)
    {
        if (this == isClient && this != isServer && this == isLocalPlayer && hand == 0)
        {
            CmdHandGoesPoof(0, player);
        }
        else if (this == isClient && this != isServer && this == isLocalPlayer && hand == 1)
        {
            CmdHandGoesPoof(1, player);
        }
    }

    public void ExecuteCmdHandComesBack(int hand, GameObject player)
    {
        if (this == isClient && this != isServer && this == isLocalPlayer && hand == 0)
        {
            CmdHandComesBack(0, player);
        }
        else if (this == isClient && this != isServer && this == isLocalPlayer && hand == 1)
        {
            CmdHandComesBack(1, player);
        }
    }
    // Function where we can check if player isClient and isLocalPlayer and not isServer before executing CmdAssignAuthority
    public void ExecuteCmdAssignAuthority(NetworkIdentity objectID)
    {
        if (this == isClient && this != isServer && this == isLocalPlayer)
        {
            CmdAssignAuthority(objectID, this.GetComponent<NetworkIdentity>());
        }
    }

    // Function where we can check if player isClient and isLocalPlayer and not isServer before executing CmdRemoveAuthority
    public void ExecuteCmdRemoveAuthority(NetworkIdentity objectID)
    {
        if (this == isClient && this != isServer && this == isLocalPlayer)
        {
            CmdRemoveAuthority(objectID);
        }
    }

    public void ExecuteCmdSendPlayerScore(int score, int player, int highObject, int mediumObject, int lowObject)
    {
        if (this == isClient && this != isServer && this == isLocalPlayer)
        {
            CmdSendPlayerScore(score, player, highObject, mediumObject, lowObject);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdSendPlayerScore(int score, int player, int highObject, int mediumObject, int lowObject)
    {
        if (this.isServer)
        {
            NetworkIdentity nurseID = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
            NetworkIdentity agressorID = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();

            if (player == 0)
            {
                GameObject nurseBar = GameObject.Find("NurseBar");
                nurseBar.GetComponent<ScoreBar>().SetScore(score);
                nurseScore = score;
                highObjectsN = highObject;
                mediumObjectsN = mediumObject;
                lowObjectsN = lowObject;

                gameManager.GetComponent<GameManager>().NurseScoreGM = nurseScore;
                gameManager.GetComponent<GameManager>().HighObjectsNGM = highObjectsN;
                gameManager.GetComponent<GameManager>().MediumObjectsNGM = mediumObjectsN;
                gameManager.GetComponent<GameManager>().LowObjectsNGM = lowObjectsN;

                TargetSendNurseScore(agressorID.connectionToClient, nurseScore, highObjectsN, mediumObjectsN, lowObjectsN);
            }
            else if (player == 1)
            {
                GameObject agressorBar = GameObject.Find("AgressorBar");
                agressorBar.GetComponent<ScoreBar>().SetScore(score);
                agressorScore = score;
                highObjectsA = highObject;
                mediumObjectsA = mediumObject;
                lowObjectsA = lowObject;

                gameManager.GetComponent<GameManager>().AgressorScoreGM = agressorScore;
                gameManager.GetComponent<GameManager>().HighObjectsAGM = highObjectsA;
                gameManager.GetComponent<GameManager>().MediumObjectsAGM = mediumObjectsA;
                gameManager.GetComponent<GameManager>().LowObjectsAGM = lowObjectsA;

                TargetSendAgressorScore(nurseID.connectionToClient, agressorScore, highObjectsA, mediumObjectsA, lowObjectsA);
            }
        }
    }

    [TargetRpc]
    public void TargetSendNurseScore(NetworkConnection playerConnection, int otherPlayerScore, int highObjectN, int mediumObjectN, int lowObjectN)
    {
        nurseScore = otherPlayerScore;

        highObjectsN = highObjectN;
        mediumObjectsN = mediumObjectN;
        lowObjectsN = lowObjectN;

        gameManager.GetComponent<GameManager>().NurseScoreGM = nurseScore;
        gameManager.GetComponent<GameManager>().HighObjectsNGM = highObjectsN;
        gameManager.GetComponent<GameManager>().MediumObjectsNGM = mediumObjectsN;
        gameManager.GetComponent<GameManager>().LowObjectsNGM = lowObjectsN;

        if (scene.name == "ZiekenhuisKamer")
        {
            GameObject nurseBar = GameObject.Find("NurseBar");
            nurseBar.GetComponent<ScoreBar>().SetScore(otherPlayerScore);
        }
    }

    [TargetRpc]
    public void TargetSendOwnScoreN(NetworkConnection playerConnection, int ownScore, int highObjectN, int mediumObjectN, int lowObjectN)
    {
        nurseScore = ownScore;
        highObjectsN = highObjectN;
        mediumObjectsN = mediumObjectN;
        lowObjectsN = lowObjectN;

        gameManager.GetComponent<GameManager>().NurseScoreGM = nurseScore;
        gameManager.GetComponent<GameManager>().HighObjectsNGM = highObjectsN;
        gameManager.GetComponent<GameManager>().MediumObjectsNGM = mediumObjectsN;
        gameManager.GetComponent<GameManager>().LowObjectsNGM = lowObjectsN;
    }

    [TargetRpc]
    public void TargetSendAgressorScore(NetworkConnection playerConnection, int otherPlayerScore, int highObjectA, int mediumObjectA, int lowObjectA)
    {
        agressorScore = otherPlayerScore;

        highObjectsA = highObjectA;
        mediumObjectsA = mediumObjectA;
        lowObjectsA = lowObjectA;

        gameManager.GetComponent<GameManager>().AgressorScoreGM = agressorScore;
        gameManager.GetComponent<GameManager>().HighObjectsAGM = highObjectsA;
        gameManager.GetComponent<GameManager>().MediumObjectsAGM = mediumObjectsA;
        gameManager.GetComponent<GameManager>().LowObjectsAGM = lowObjectsA;

        if (scene.name == "ZiekenhuisKamer")
        {
            GameObject agressorBar = GameObject.Find("AgressorBar");
            agressorBar.GetComponent<ScoreBar>().SetScore(otherPlayerScore);
        }
    }

    [TargetRpc]
    public void TargetSendOwnScoreA(NetworkConnection playerConnection, int ownScore, int highObjectA, int mediumObjectA, int lowObjectA)
    {
        agressorScore = ownScore;
        highObjectsA = highObjectA;
        mediumObjectsA = mediumObjectA;
        lowObjectsA = lowObjectA;

        gameManager.GetComponent<GameManager>().AgressorScoreGM = agressorScore;
        gameManager.GetComponent<GameManager>().HighObjectsAGM = highObjectsA;
        gameManager.GetComponent<GameManager>().MediumObjectsAGM = mediumObjectsA;
        gameManager.GetComponent<GameManager>().LowObjectsAGM = lowObjectsA;
    }

    [ClientRpc]
    public void RpcSendScores(int scoreN, int scoreA, int highN, int highA, int mediumN, int mediumA, int lowN, int lowA)
    {
        nurseScore = scoreN;
        agressorScore = scoreA;
        highObjectsA = highA;
        highObjectsN = highN;
        mediumObjectsA = mediumA;
        mediumObjectsN = mediumN;
        lowObjectsA = lowA;
        lowObjectsN = lowN;

        gameManager.GetComponent<GameManager>().AgressorScoreGM = agressorScore;
        gameManager.GetComponent<GameManager>().HighObjectsAGM = highObjectsA;
        gameManager.GetComponent<GameManager>().MediumObjectsAGM = mediumObjectsA;
        gameManager.GetComponent<GameManager>().LowObjectsAGM = lowObjectsA;
        gameManager.GetComponent<GameManager>().NurseScoreGM = nurseScore;
        gameManager.GetComponent<GameManager>().HighObjectsNGM = highObjectsN;
        gameManager.GetComponent<GameManager>().MediumObjectsNGM = mediumObjectsN;
        gameManager.GetComponent<GameManager>().LowObjectsNGM = lowObjectsN;
    }

    [Command(requiresAuthority = false)]
    public void CmdAssignAuthority(NetworkIdentity objectID, NetworkIdentity playerID)
    {
        objectID.AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
    }

    [Command(requiresAuthority = false)]
    public void CmdRemoveAuthority(NetworkIdentity objectID)
    {
        objectID.RemoveClientAuthority();
    }

    [Command(requiresAuthority = false)]
    public void CmdHandGoesPoof(int hand, GameObject player)
    {
        if (hand == 0)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            RpcHandGoesPoof(0, player);
        }
        else if (hand == 1)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
            RpcHandGoesPoof(1, player);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdHandComesBack(int hand, GameObject player)
    {
        if (hand == 0)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            RpcHandComesBack(0, player);
        }
        else if (hand == 1)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
            RpcHandComesBack(1, player);
        }
    }

    [ClientRpc]
    public void RpcHandGoesPoof(int hand, GameObject player)
    {
        if (hand == 0)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (hand == 1)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    [ClientRpc]
    public void RpcHandComesBack(int hand, GameObject player)
    {
        if (hand == 0)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (hand == 1)
        {
            player.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
