using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlackJackCanvas : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// The Banner that holds the player's information
    /// </summary>
    [Header("Player Banner")]
    public GameObject playerBanner;
    /// <summary>
    /// The Text that displays how much money the player has
    /// </summary>
    public TMP_Text playerMoneyText;
    /// <summary>
    /// The Text that displays what kind of bet the player has made
    /// </summary>
    public TMP_Text playerBetText;
    /// <summary>
    /// The Text that displays the turn status
    /// </summary>
    public TMP_Text turnStatus;


    /// <summary>
    /// The input field where the player enters their bet
    /// </summary>
    [Header("Your Bet Input Field")]
    public GameObject yourBetInputField;
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
    /// A bool that determines whether or not the bet the player placed was valid
    /// </summary>
    private bool validBet;

    /// <summary>
    /// The numeric amout of money the player has
    /// </summary>
    [Header("Player Data")]
    public int playerMoneyValue = 5000;
    //TODO: This variable holds the value of the bet the player made, meant to be used later
    private int playerBetValue = 0;

    /// <summary>
    /// A collection of buttons in the action menu
    /// </summary>
    [Header("Actions Menu")]
    public List<Button> actionMenuButtons;

    /// <summary>
    /// A list box holding data on all the cards drawn for the player
    /// </summary>
    [Header("Card List Box")]
    public GameObject cardListBox;
    /// <summary>
    /// The text that holds the player's current score
    /// </summary>
    public TMP_Text currentScoreDisplay;
    /// <summary>
    /// The list box item prefab to spawn
    /// </summary>
    public GameObject listBoxItem;

    /// <summary>
    /// The current score the player has with their drawn cards
    /// </summary>
    private int currentPlayerScore;
    #endregion

    private void Start()
    {
        playerMoneyText.text = $"Money: ${playerMoneyValue}";
        currentScoreDisplay.text = 0.ToString("00");
    }

    /// <summary>
    /// Prepares the Gameplay Elements
    /// </summary>
    public void PrepareGameplayElements()
    {
        yourBetInputField.SetActive(true);
        playerBanner.SetActive(true);
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

        //if (possibleBet > 100 && possibleBet % 25 == 0)
        //{
        //    //Valid bet
        //    validBet = true;
        //    finalizeBetLabel.text = "Press <color=yellow><b>ENTER</b></color> to finalize bet!";
        //    betInput.color = Color.green * 0.5f;
        //}
        //else
        //{
        //    //Not valid bets
        //    validBet = false;
        //    finalizeBetLabel.gameObject.SetActive(false);
        //    betInput.color = Color.red;
        //}

        if (possibleBet < playerMoneyValue)
        {
            if (possibleBet >= 100)
            {
                if (possibleBet % 25 == 0)
                {
                    //Valid bet
                    validBet = true;
                    finalizeBetLabel.color = Color.black;
                    finalizeBetLabel.text = "Press <color=yellow><b>ENTER</b></color> to finalize bet!";
                    betInput.color = Color.green * 0.5f;
                }
                else
                {
                    validBet = false;
                    finalizeBetLabel.color = Color.red;
                    finalizeBetLabel.text = "Bet needs to be a multiple of 25.";
                    betInput.color = Color.red;
                }
            }
            else
            {
                validBet = false;
                finalizeBetLabel.color = Color.red;
                finalizeBetLabel.text = "Bet is lower than $100 buy in.";
                betInput.color = Color.red;
            }
        }
        else
        {
            validBet = false;
            finalizeBetLabel.color = Color.red;
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

            if(bet > playerMoneyValue)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                print($"Player's bet: {bet}");

                playerMoneyValue -= bet;
                playerBetValue = bet;

                playerMoneyText.text = $"Money: ${playerMoneyValue}";
                playerBetText.text = $"Bet: ${bet}";

                yourBetInputField.SetActive(false);

                OnPlayerBetPlaced.Invoke(bet);

                turnStatus.text = "Your Turn";
            } 
        }
    }

    /// <summary>
    /// Toggles all buttons in the action menu
    /// </summary>
    public void ToggleActionButtons()
    {
        foreach (Button b in actionMenuButtons)
        {
            b.interactable = !b.interactable;
        }
    }

    /// <summary>
    /// Adds a card to the list box
    /// </summary>
    /// <param name="cardValue">
    /// The value of the card coming in
    /// </param>
    public void AddCardToListBox(string cardValue)
    {
        int valueDrawn;

        //print($"Card value is {cardValue}");
        GameObject a = Instantiate(listBoxItem, parent: cardListBox.transform);
        CardListBoxItem item = a.GetComponent<CardListBoxItem>();
        item.FillItem(cardValue, out valueDrawn);

        UpdatePlayerScore(valueDrawn);

        if (currentPlayerScore == 21)
        {
            currentScoreDisplay.color = Color.yellow;
        }
        else if (currentPlayerScore > 21)
        {
            currentScoreDisplay.color = Color.red;
            //TODO: Player loses, hand is over 21, start the game over (reset everything) and have player place a new bet
        }
    }

    /// <summary>
    /// Updates the player's score
    /// </summary>
    /// <param name="incomingScore">
    /// Value to add onto current score
    /// </param>
    public void UpdatePlayerScore(int incomingScore)
    {

        currentPlayerScore += incomingScore;
        currentScoreDisplay.text = currentPlayerScore.ToString();
    }
}