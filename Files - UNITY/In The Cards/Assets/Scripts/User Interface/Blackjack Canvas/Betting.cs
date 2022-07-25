using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Betting : MonoBehaviour
{

    #region Variables
    /// <summary>
    /// The input field where the player enters their bet
    /// </summary>
    public TMP_InputField yourBetInputField;
    /// <summary>
    /// The text inside the bet input field
    /// </summary>
    public TMP_Text betInput;
    /// <summary>
    /// Text that tells the player the status and validity of their entered bet
    /// </summary>
    public TMP_Text finalizeBetLabel;
    /// <summary>
    /// The event that fires off when the player has placed their bet
    /// </summary>
    /// <returns>
    /// The bet the player placed
    /// </returns>
    public IntEventWrapper OnPlayerBetPlaced;
    
    /// <summary>
    /// The value of the player's bet.  As the game is going on and when the game ends and multiplier is applied
    /// </summary>
    public int PlayerBetValue { get; private set; }

    /// <summary>
    /// The information, scores, and UI tied to the player
    /// </summary>
    private PlayerInfo playerInfo;
    /// <summary>
    /// A bool that determines whether or not the bet the player placed was valid
    /// </summary>
    private bool validBet;
    #endregion


    private void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }

    /// <summary>
    /// Evaluates whether the given input is a valid bet
    /// </summary>
    /// <param name="playerInput">
    /// The player entered bet value
    /// </param>
    public void EvaluateBetInput(string playerInput)
    {
        if (playerInput == string.Empty || playerInput == null)
        {
            finalizeBetLabel.text = string.Empty;
            return;
        }

        int possibleBet = int.Parse(playerInput);


        if (possibleBet < playerInfo.playerMoneyValue)
        {
            if (possibleBet >= 100)
            {
                if (possibleBet % 25 == 0)
                {
                    //Valid bet
                    validBet = true;
                    finalizeBetLabel.color = Color.white;
                    finalizeBetLabel.text = "Press <color=yellow><b>ENTER</b></color> to finalize bet!";
                    betInput.color = Color.green * 0.5f;
                }
                else
                {
                    validBet = false;
                    finalizeBetLabel.text = "Bet needs to be a multiple of 25.";
                    betInput.color = Color.red;
                }
            }
            else
            {
                validBet = false;
                finalizeBetLabel.text = "Bet is lower than $100 buy in.";
                betInput.color = Color.red;
            }
        }
        else
        {
            validBet = false;
            finalizeBetLabel.text = "Bet is higher than the money you have.";
            betInput.color = Color.red;
        }
    }

    /// <summary>
    /// Places the player's bet
    /// </summary>
    /// <param name="playerInput">
    /// The player entered bet value
    /// </param>
    public void PlaceBet(string playerInput)
    {

        if (playerInput == string.Empty || playerInput == null)
        {
            return;
        }

        if (validBet)
        {
            int bet = int.Parse(playerInput);

            if (bet > playerInfo.playerMoneyValue)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                print($"Player's bet: {bet}");

                playerInfo.playerMoneyValue -= bet;
                PlayerBetValue = bet;

                playerInfo.playerMoneyText.text = $"Money: ${playerInfo.playerMoneyValue}";
                playerInfo.playerBetText.text = $"Bet: ${bet}";

                yourBetInputField.gameObject.SetActive(false);

                OnPlayerBetPlaced.Invoke(bet);

                //TODO: Refactoring the turn status text - This needs to go somewhere
                //turnStatus.text = "Your Turn";

                //TODO: Wait until after the dealer has dealt their cards for the actions menu to fold in
            }
        }
    }

    /// <summary>
    /// Causes the Betting Elements to reappear as if they were brand new
    /// </summary>
    public void ShowNewBettingElements()
    {
        yourBetInputField.gameObject.SetActive(true);
        
        yourBetInputField.text = string.Empty;

        finalizeBetLabel.color = Color.white;
        finalizeBetLabel.text = string.Empty;
    }
}