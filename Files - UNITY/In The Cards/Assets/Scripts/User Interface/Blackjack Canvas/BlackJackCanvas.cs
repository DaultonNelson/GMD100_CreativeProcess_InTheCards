using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlackJackCanvas : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// A module in charge of betting
    /// </summary>
    public Betting betting;
    /// <summary>
    /// A module in charge of player information
    /// </summary>
    public PlayerInfo playerInfo;

    /// <summary>
    /// The Text that displays the turn status
    /// </summary>
    public TMP_Text turnStatus;


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
    /// The Text that displays the game's end result (win/lose)
    /// </summary>
    [Header("Result Group")]
    public TMP_Text gameResultLabel;
    /// <summary>
    /// The Text that displays the reason as to why the player got their result
    /// </summary>
    public TMP_Text reasonResultLabel;
    /// <summary>
    /// The Text that displays the player's earnings post-game
    /// </summary>
    public TMP_Text betResultLabel;

    /// <summary>
    /// A translucent black image that makes everything darker
    /// </summary>
    [Header("Other variables")]
    public GameObject translucentBlack;
    /// <summary>
    /// The Dealer AI
    /// </summary>
    public DealerIntelligence dealerAI;
    /// <summary>
    /// A collection of all card dealing objects in the scene
    /// </summary>
    public List<CardDealer> cardDistributors;

    //Private Variables
    /// <summary>
    /// The current score the player has with their drawn cards
    /// </summary>
    private int currentPlayerScore;
    /// <summary>
    /// The Animator attached to the Blackjack Canvas
    /// </summary>
    private Animator canvasAnimator;
    /// <summary>
    /// A collection of all the spawned card box list items
    /// </summary>
    private List<GameObject> spawnedCardBoxItems = new List<GameObject>();
    #endregion

    private void Start()
    {
        canvasAnimator = GetComponent<Animator>();

        currentScoreDisplay.text = 0.ToString("00");
    }

    /// <summary>
    /// Prepares the Gameplay Elements
    /// </summary>
    public void PrepareGameplayElements()
    {
        //TODO: these are important for when the camera is swooping in
        //yourBetInputField.SetActive(true);
        //playerBanner.SetActive(true);
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

        GameObject a = Instantiate(listBoxItem, parent: cardListBox.transform);
        CardListBoxItem item = a.GetComponent<CardListBoxItem>();
        item.FillItem(cardValue, out valueDrawn);
        spawnedCardBoxItems.Add(a);

        UpdatePlayerScore(valueDrawn);

        if (currentPlayerScore == 21) //NOTE: Player gets blackjack here: Cards total 21
        {
            currentScoreDisplay.color = Color.yellow;
            EndGame("BLACKJACK!", "Your cards Total Exactly 21", 1.5f);
            return;
        }
        else if (currentPlayerScore > 21) //NOTE: Player loses here: Cards total over 21
        {
            currentScoreDisplay.color = Color.red;
            EndGame("You Lose", "Your Cards Totaled Over 21", 0f);
            return;
        }

        //Have the dealer take their turn if nothing is blocking the way
        dealerAI.TakeTurn();
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

    /// <summary>
    /// Ends the current game
    /// </summary>
    /// <param name="result"> The result of the game </param>
    /// <param name="reason"> The reason the result was reached </param>
    /// <param name="betMultiplier"> The bet multiplier that will effect the player's bet </param>
    public void EndGame(string result, string reason, float betMultiplier)
    {
        int winningPrize = (int)(betting.PlayerBetValue * betMultiplier);
        playerInfo.playerMoneyValue += winningPrize;

        //Turn important elements on
        translucentBlack.SetActive(true);
        gameResultLabel.gameObject.SetActive(true);
        reasonResultLabel.gameObject.SetActive(true);
        betResultLabel.gameObject.SetActive(true);

        //Turn some other important elements off
        playerInfo.playerBetText.text = $"Bet: $0";
        turnStatus.text = string.Empty;
        canvasAnimator.SetTrigger("ActionsFoldOut");

        //Display results, have player press continue or quit application (for now)
        gameResultLabel.text = result;
        reasonResultLabel.text = reason;
        betResultLabel.text = $"Earnings: BET x {betMultiplier:0.00} = ${winningPrize}";

        //<color=yellow><b>ENTER</b></color>
        playerInfo.playerMoneyText.text = $"Money: ${playerInfo.playerMoneyValue}<color=yellow><b> (+ ${winningPrize})</b></color>";
    }

    /// <summary>
    /// Resets everything for a new game
    /// </summary>
    public void ResetToNewGame()
    {
        Debug.Log("Player chooses to play a new game of blackjack.", gameObject);

        //Get rid of status labels
        gameResultLabel.gameObject.SetActive(false);
        reasonResultLabel.gameObject.SetActive(false);
        betResultLabel.gameObject.SetActive(false);

        //Show betting elements as if they were brand new
        betting.ShowNewBettingElements();

        //Get rid of gold in front of player info
        playerInfo.playerMoneyText.text = $"Money: ${playerInfo.playerMoneyValue}";

        //Clear Cards off the table, put them back in the deck but on the bottom
        foreach (CardDealer cd in cardDistributors)
        {
            cd.CollectDealtCards();
        }

        //Clear Card List Box
        while (spawnedCardBoxItems.Count > 0)
        {
            GameObject obj = spawnedCardBoxItems.First();
            spawnedCardBoxItems.Remove(obj);
            Destroy(obj);
        }
        spawnedCardBoxItems.Clear();
        currentScoreDisplay.text = 0.ToString("00");
        currentScoreDisplay.color = Color.white;

        //Reset dealer's AI
        dealerAI.ResetAI();

        //Clear player's score
        currentPlayerScore = 0;
    }

    /// <summary>
    /// Quits the application
    /// </summary>
    public void QuitApplication()
    {
        //TODO: when clicked, have the camera zoom out then close on end of animation
        Debug.Log("Player chooses to quit out of application.", gameObject);
        Application.Quit();
    }

}