
using System;
using UnityEngine;
using UnityEngine.UI;

public class GooseGameUI : BasicUIScreen, IGameUI
{
    public Text announcementLabel;
    public Text instructionsLabel;
    public Text playerTurnLabel;


    public override void FillText()
    {
    }

    public override void HandleError(string errorName)
    {
        throw new System.NotImplementedException();
    }

    public void BoardPlacement()
    {
        throw new System.NotImplementedException();
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
