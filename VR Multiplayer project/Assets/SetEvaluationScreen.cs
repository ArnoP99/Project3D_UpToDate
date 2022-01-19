using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetEvaluationScreen : NetworkBehaviour
{

    Scene scene;

    bool nurseWon = false;
    bool agressorWon = false;

    // Start is called before the first frame update
    public void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "EndRoom")
        {
            if (gameObject.GetComponent<NetworkIdentity>().isLocalPlayer && gameObject.gameObject.tag == "Server")
            {

                Debug.Log("This is server");
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
                    if (gameObject.GetComponent<AssignAuth>().nurseScore > gameObject.GetComponent<AssignAuth>().agressorScore)
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

                        Debug.Log("Nurse score: " + gameObject.GetComponent<AssignAuth>().nurseScore);
                        Debug.Log("Nurse highscore:" + gameObject.GetComponent<AssignAuth>().highObjectsN);
                        Debug.Log("Nurse mediumscore:" + gameObject.GetComponent<AssignAuth>().mediumObjectsN);
                        Debug.Log("Nurse lowscore:" + gameObject.GetComponent<AssignAuth>().lowObjectsN);

                        GameObject.Find("HighScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().highObjectsN.ToString();
                        GameObject.Find("MediumScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().mediumObjectsN.ToString();
                        GameObject.Find("LowScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().lowObjectsN.ToString();
                        GameObject.Find("TotalScoreNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().nurseScore.ToString();

                        GameObject.Find("HighScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().highObjectsA.ToString();
                        GameObject.Find("MediumScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().mediumObjectsA.ToString();
                        GameObject.Find("LowScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().lowObjectsA.ToString();
                        GameObject.Find("TotalScoreAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().agressorScore.ToString();
                    }
                    else
                    {
                        Debug.Log("Nurse score: " + gameObject.GetComponent<AssignAuth>().nurseScore);
                        Debug.Log("Nurse highscore:" + gameObject.GetComponent<AssignAuth>().highObjectsN);
                        Debug.Log("Nurse mediumscore:" + gameObject.GetComponent<AssignAuth>().mediumObjectsN);
                        Debug.Log("Nurse lowscore:" + gameObject.GetComponent<AssignAuth>().lowObjectsN);

                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseLoses");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorWins");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Nurse");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Agressor");

                        GameObject.Find("HighScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().highObjectsN.ToString();
                        GameObject.Find("MediumScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().mediumObjectsN.ToString();
                        GameObject.Find("LowScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().lowObjectsN.ToString();
                        GameObject.Find("TotalScoreNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().nurseScore.ToString();

                        GameObject.Find("HighScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().highObjectsA.ToString();
                        GameObject.Find("MediumScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().mediumObjectsA.ToString();
                        GameObject.Find("LowScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().lowObjectsA.ToString();
                        GameObject.Find("TotalScoreAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().agressorScore.ToString();
                    }
                }
            }
            if (gameObject.GetComponent<NetworkIdentity>().isLocalPlayer && gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Agressor")
            {
                Debug.Log("this is agressor");
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
                    if (gameObject.GetComponent<AssignAuth>().nurseScore > gameObject.GetComponent<AssignAuth>().agressorScore)
                    {
                        nurseWon = true;
                    }
                    else
                    {
                        agressorWon = true;
                    }

                    if (nurseWon)
                    {
                        Debug.Log("Nurse score: " + gameObject.GetComponent<AssignAuth>().nurseScore);
                        Debug.Log("Nurse highscore:" + gameObject.GetComponent<AssignAuth>().highObjectsN);
                        Debug.Log("Nurse mediumscore:" + gameObject.GetComponent<AssignAuth>().mediumObjectsN);
                        Debug.Log("Nurse lowscore:" + gameObject.GetComponent<AssignAuth>().lowObjectsN);

                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseWins");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorLoses");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Nurse");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Agressor");

                        GameObject.Find("HighScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().highObjectsN.ToString();
                        GameObject.Find("MediumScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().mediumObjectsN.ToString();
                        GameObject.Find("LowScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().lowObjectsN.ToString();
                        GameObject.Find("TotalScoreNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().nurseScore.ToString();

                        GameObject.Find("HighScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().highObjectsA.ToString();
                        GameObject.Find("MediumScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().mediumObjectsA.ToString();
                        GameObject.Find("LowScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().lowObjectsA.ToString();
                        GameObject.Find("TotalScoreAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().agressorScore.ToString();
                    }
                    else
                    {
                        Debug.Log("Nurse score: " + gameObject.GetComponent<AssignAuth>().nurseScore);
                        Debug.Log("Nurse highscore:" + gameObject.GetComponent<AssignAuth>().highObjectsN);
                        Debug.Log("Nurse mediumscore:" + gameObject.GetComponent<AssignAuth>().mediumObjectsN);
                        Debug.Log("Nurse lowscore:" + gameObject.GetComponent<AssignAuth>().lowObjectsN);

                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseLoses");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorWins");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Nurse");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Agressor");

                        GameObject.Find("HighScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().highObjectsN.ToString();
                        GameObject.Find("MediumScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().mediumObjectsN.ToString();
                        GameObject.Find("LowScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().lowObjectsN.ToString();
                        GameObject.Find("TotalScoreNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().nurseScore.ToString();

                        GameObject.Find("HighScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().highObjectsA.ToString();
                        GameObject.Find("MediumScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().mediumObjectsA.ToString();
                        GameObject.Find("LowScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().lowObjectsA.ToString();
                        GameObject.Find("TotalScoreAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().agressorScore.ToString();
                    }
                }
            }
            else if (gameObject.GetComponent<NetworkIdentity>().isLocalPlayer && gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Nurse")
            {
                Debug.Log("dit is de nurse");
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
                    if (gameObject.GetComponent<AssignAuth>().nurseScore > gameObject.GetComponent<AssignAuth>().agressorScore)
                    {
                        nurseWon = true;
                    }
                    else
                    {
                        agressorWon = true;
                    }

                    if (nurseWon)
                    {
                        Debug.Log("Nurse score: " + gameObject.GetComponent<AssignAuth>().nurseScore);
                        Debug.Log("Nurse highscore:" + gameObject.GetComponent<AssignAuth>().highObjectsN);
                        Debug.Log("Nurse mediumscore:" + gameObject.GetComponent<AssignAuth>().mediumObjectsN);
                        Debug.Log("Nurse lowscore:" + gameObject.GetComponent<AssignAuth>().lowObjectsN);

                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseWins");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorLoses");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Nurse");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Agressor");

                        GameObject.Find("HighScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().highObjectsN.ToString();
                        GameObject.Find("MediumScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().mediumObjectsN.ToString();
                        GameObject.Find("LowScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().lowObjectsN.ToString();
                        GameObject.Find("TotalScoreNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().nurseScore.ToString();

                        GameObject.Find("HighScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().highObjectsA.ToString();
                        GameObject.Find("MediumScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().mediumObjectsA.ToString();
                        GameObject.Find("LowScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().lowObjectsA.ToString();
                        GameObject.Find("TotalScoreAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().agressorScore.ToString();
                    }
                    else
                    {
                        Debug.Log("Nurse score: " + gameObject.GetComponent<AssignAuth>().nurseScore);
                        Debug.Log("Nurse highscore:" + gameObject.GetComponent<AssignAuth>().highObjectsN);
                        Debug.Log("Nurse mediumscore:" + gameObject.GetComponent<AssignAuth>().mediumObjectsN);
                        Debug.Log("Nurse lowscore:" + gameObject.GetComponent<AssignAuth>().lowObjectsN);

                        GameObject.Find("NursePicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/NurseLoses");
                        GameObject.Find("AgressorPicture").GetComponent<Renderer>().material = Resources.Load<Material>("Style/AgressorWins");

                        GameObject.Find("NursePlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Nurse");
                        GameObject.Find("AgressorPlane").GetComponent<Renderer>().material = Resources.Load<Material>("Style/EvaluationCardV2Agressor");

                        GameObject.Find("HighScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().highObjectsN.ToString();
                        GameObject.Find("MediumScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().mediumObjectsN.ToString();
                        GameObject.Find("LowScoreTextNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().lowObjectsN.ToString();
                        GameObject.Find("TotalScoreNurse").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().nurseScore.ToString();

                        GameObject.Find("HighScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().highObjectsA.ToString();
                        GameObject.Find("MediumScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().mediumObjectsA.ToString();
                        GameObject.Find("LowScoreTextAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().lowObjectsA.ToString();
                        GameObject.Find("TotalScoreAgressor").GetComponent<TextMeshPro>().text = gameObject.GetComponent<AssignAuth>().agressorScore.ToString();
                    }
                }
            }
        }
    }
}
