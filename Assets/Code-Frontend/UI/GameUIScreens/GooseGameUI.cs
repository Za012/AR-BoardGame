
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GooseGameUI : BasicUIScreen, IGameUI
{
    public Text announcementLabel;
    public Text instructionsLabel;
    public Text playerTurnLabel;
    public GameObject[] playerSlots;
    public Color localPlayer;
    public Color remotePlayer;
    public Image dice1;
    public Image dice2;

    private GameObject localPlayerSlot;
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
    public void InstantiatePlayerList(Player[] players)
    {
        int slotIndex = 0;
        foreach (Player player in players)
        {
            playerSlots[slotIndex].transform.Find("PlayerName").GetComponent<Text>().text = player.NickName;
            if(player == PhotonNetwork.LocalPlayer)
            {
                playerSlots[slotIndex].transform.Find("Background").GetComponent<Image>().color = localPlayer;
                localPlayerSlot = playerSlots[slotIndex];
            }
            else
            {
                playerSlots[slotIndex].transform.Find("Background").GetComponent<Image>().color = remotePlayer;
            }
            playerSlots[slotIndex].SetActive(true);
            playerSlots[slotIndex].transform.Find("Icon").GetComponent<Image>().gameObject.SetActive(false);
            slotIndex++;
        }
    }

    public void PlayerTurn(int turnNumber, int maxPlayerNumber)
    {
        // Dice roll Icon
        Debug.Log("Icon Active: " + turnNumber);
        playerSlots[turnNumber].transform.Find("Icon").GetComponent<Image>().gameObject.SetActive(true);
        int prevTurn = turnNumber - 1;
        if (prevTurn < 0)
            prevTurn = maxPlayerNumber;

        Debug.Log("Icon DeActive: " + prevTurn);
        playerSlots[prevTurn].transform.Find("Icon").GetComponent<Image>().gameObject.SetActive(false);
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
