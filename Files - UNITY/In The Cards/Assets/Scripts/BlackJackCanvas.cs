using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlackJackCanvas : MonoBehaviour
{
    #region Variables
    public GameObject playerBanner;
    [Header("Your Bet Input Field")]
    public GameObject yourBetInputField;
    public TMP_Text betInput;
    public TMP_Text finalizeBetLabel;

    private bool validBet;
    #endregion

    public void PrepareGameplayElements()
    {
        yourBetInputField.SetActive(true);
        playerBanner.SetActive(true);
    }

    public void EvaluateBetInput(string playerInput)
    {
        if (playerInput == string.Empty || playerInput == null)
        {
            return;
        }

        int possibleBet = int.Parse(playerInput);

        if (possibleBet < 100)
        {
            validBet = false;
            finalizeBetLabel.gameObject.SetActive(false);
            betInput.color = Color.red;
        }
        else
        {
            validBet = true;
            finalizeBetLabel.gameObject.SetActive(true);
            betInput.color = Color.green;
        }
    }

    public void PlaceBet(string playerInput)
    {

        if (playerInput == string.Empty || playerInput == null)
        {
            return;
        }

        if (validBet)
        {
            int bet = int.Parse(playerInput);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                print($"Player's bet: {bet}");
                yourBetInputField.SetActive(false);
            } 
        }
    }
}