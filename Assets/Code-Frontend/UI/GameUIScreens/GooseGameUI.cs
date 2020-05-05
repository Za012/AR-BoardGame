
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GooseGameUI : BasicUIScreen, IGameUI
{
    public Text announcementLabel;
    public Text instructionsLabel;
    public Text playerTurnLabel;
    public Image dice1;
    public Image dice2;

    private Sprite[] diceSides;


    public override void FillText()
    {
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }

    public override void HandleError(string errorName)
    {
        throw new System.NotImplementedException();
    }

    public void BoardPlacement()
    {
        throw new System.NotImplementedException();
    }

    public void ChangeDiceImage(int diceSide1, int diceSide2)
    {
        dice1.gameObject.SetActive(true);
        dice2.gameObject.SetActive(true);
        dice1.sprite = diceSides[diceSide1];
        dice2.sprite = diceSides[diceSide2]; 
    }
    public void HideDiceImage()
    {
        dice1.gameObject.SetActive(false);
        dice2.gameObject.SetActive(false);
    }

    public void PlayerTurn()
    {
        throw new System.NotImplementedException();
    }

    internal void ChangeAnnouncement(string v)
    {
        Debug.Log("Changing announcement to: " + v);
        announcementLabel.text = v;
    }

    internal void ChangeInstruction(string v)
    {
        Debug.Log("Changing instruction to: " + v);
        instructionsLabel.text = v;
    }
}
