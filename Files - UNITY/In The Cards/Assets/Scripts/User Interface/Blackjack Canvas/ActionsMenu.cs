using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsMenu : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// The object that deal's the player's cards
    /// </summary>
    public CardDealer playerDealer;

    /// <summary>
    /// A reference to the Blackjack Canvas, should be one in scene
    /// </summary>
    private BlackJackCanvas bjc;
    #endregion

    private void Awake()
    {
        if (FindObjectsOfType<BlackJackCanvas>().Length > 1)
        {
            Debug.LogWarning("There is more than one Blackjack Canvas script in the scene!");
        }

        bjc = FindObjectOfType<BlackJackCanvas>();
    }

    /// <summary>
    /// HIT - Take a card
    /// </summary>
    public void Hit()
    {
        bjc.ToggleActionButtons();
        bjc.turnStatus.text = "Dealer's Turn";
        playerDealer.DealCard();
    }

    //TODO: STAND - End turn, do not take card, if twice in row then finish
    public void Stand()
    {

    }

    //TODO: DOUBLE - Double Wager, take card, finish
    public void Double()
    {

    }

    //TODO: SURRENDER - Give up half of bet, end game (finish)
    public void Surrender()
    {

    }
}