using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerIntelligence : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// How the dealer draws their cards
    /// </summary>
    public CardDealer cardDealer;
    /// <summary>
    /// The Blackjack canavs in the scene (there should only be one)
    /// </summary>
    public BlackJackCanvas blackJackCanvas;
    /// <summary>
    /// The dealer's light
    /// </summary>
    public GameObject dealerLight;

    /// <summary>
    /// The amount of time (in seconds) it takes for the dealer to draw eack of their cards
    /// </summary>
    public float secondsBetweenCardDraws = 0.75f;
    /// <summary>
    /// The amount the dealer will be pushed by
    /// </summary>
    public float pushAmount = 0.5f;
    
    /// <summary>
    /// The score the dealer currently has
    /// </summary>
    private int dealerScore;
    /// <summary>
    /// The starting position of the dealer
    /// </summary>
    private Vector3 startingPos;
    #endregion

    private void Start()
    {
        startingPos = transform.position;
    }

    /// <summary>
    /// The Dealer AI takes it's turn
    /// </summary>
    public void TakeTurn()
    {
        Debug.Log("Dealer takes turn", gameObject);
        dealerLight.SetActive(true);
        StartCoroutine(DealerDrawCard());
    }

    /// <summary>
    /// Resets key values in the dealer AI
    /// </summary>
    public void ResetAI()
    {
        transform.position = startingPos;
        dealerScore = 0;
    }

    /// <summary>
    /// Pushes the dealer to the right
    /// </summary>
    public void PushDealer()
    {
        transform.position = new Vector3(
            transform.position.x - pushAmount,
            transform.position.y,
            transform.position.z
        );
    }

    /// <summary>
    /// Adds to the dealer score based on the card info
    /// </summary>
    /// <param name="cardInfo">
    /// The incoming card info
    /// </param>
    public void AddToScore(string cardInfo)
    {
        char cardVal = cardInfo[1];
        dealerScore += GetValueFromInfo(cardVal);

        print($"Dealer's Score: {dealerScore}");
    }

    /// <summary>
    /// Give a number based on given card info
    /// </summary>
    /// <param name="val">
    /// The value to parses
    /// </param>
    /// <returns>
    /// Numeric value
    /// </returns>
    private int GetValueFromInfo(char val)
    {
        switch (val)
        {
            case 'A':
                //Dealer will always choose 1
                return 1;
            case '0':
                return 10;
            case 'J':
                return 10;
            case 'Q':
                return 10;
            case 'K':
                return 10;
            default:
                int cardValue = int.Parse(val.ToString());
                return cardValue;
        }
    }

    IEnumerator DealerDrawCard()
    {
        yield return new WaitForSeconds(secondsBetweenCardDraws);
        if (dealerScore < 16) //Dealer must draw to 16, then stand
        {
            PushDealer();
            cardDealer.DealCard(); 
        }
        blackJackCanvas.ToggleActionButtons();
        blackJackCanvas.turnStatus.text = "Your Turn";
        dealerLight.SetActive(false);
    }
}