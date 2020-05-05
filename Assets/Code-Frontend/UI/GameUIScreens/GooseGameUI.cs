
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
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

    private GameObject localPlayerSlot;
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

    public void PlayerTurn(int turnNumber)
    {
        // Dice roll Icon
        playerSlots[turnNumber].transform.Find("Icon").GetComponent<Image>().gameObject.SetActive(true);
        int prevTurn = turnNumber - 1;
        if (prevTurn < 0)
            prevTurn = playerSlots.Length - 1;

        playerSlots[prevTurn].transform.Find("Icon").GetComponent<Image>().gameObject.SetActive(false);
    }

    public void DiceRoll()
    {
        //.. Do dice roll
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
