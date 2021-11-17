using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUpManager : MonoBehaviour
{
    [SerializeField]
    protected Transform playerParent;
    [SerializeField]
    protected Transform backpackMenuItemParent;

    public bool gameHasStarted = false;
    protected int currentBackpack = -1;

    [SerializeField]
    protected Color activeColor;
    [SerializeField]
    protected Color inactiveColor;

    protected int activeButtonNumber = -1;

    void Update()
    {
        //Check which backpacks are active
        if (!gameHasStarted)
        {
            for (byte i = 0; i < playerParent.childCount; i++)
            {
                currentBackpack = playerParent.GetChild(i).GetComponent<PlayerConfiguration>().PlayerID;
                if (currentBackpack > 0)
                {
                    backpackMenuItemParent.GetChild(currentBackpack - 1).GetComponent<Image>().color = activeColor;
                }
            }
        }
    }

    public void StartGame()
    {
        gameHasStarted = true;
    }

    public void ToggleFocusButtons(int buttonNumber)
    {
        //When a button is presed change to that player as new focus point, when the player allready is the focuspoint take the origin as focus point

        if (backpackMenuItemParent.GetChild(buttonNumber).GetComponent<Image>().color == activeColor)
        {
            if (buttonNumber != activeButtonNumber)
            {
                for (int i = 0; i < backpackMenuItemParent.childCount; i++)
                {
                    backpackMenuItemParent.GetChild(i).GetChild(1).GetComponent<Image>().color = inactiveColor;
                }

                backpackMenuItemParent.GetChild(buttonNumber).GetChild(1).GetComponent<Image>().color = activeColor;

                GameObject.Find("Main camera").GetComponent<CameraMover>().ChangeFocusTarget(buttonNumber);

                activeButtonNumber = buttonNumber;
            }
            else
            {
                for (int i = 0; i < backpackMenuItemParent.childCount; i++)
                {
                    backpackMenuItemParent.GetChild(i).GetChild(1).GetComponent<Image>().color = inactiveColor;
                }

                GameObject.Find("Main camera").GetComponent<CameraMover>().ChangeFocusTarget(-1);

                activeButtonNumber = -1;
            }
        }
    }
}
