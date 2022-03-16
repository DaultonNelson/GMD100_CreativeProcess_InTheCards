using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardDealer : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Vector 3 Direction Enum
    /// </summary>
    public enum V3Direction { UP, DOWN, LEFT, RIGHT, FORWARD, BACK}

    /// <summary>
    /// The playing card prefab
    /// </summary>
    public GameObject playingCard;
    /// <summary>
    /// Determines whether the playing card should be dealt face up or face down
    /// </summary>
    public bool dealFaceUp;
    /// <summary>
    /// The force at which the card is throw (the card is light, so this should be multiplied by one hundreth)
    /// </summary>
    public float dealForce;
    /// <summary>
    /// An enum of directions that the playing card can be thrown in.  Purely for the editor
    /// </summary>
    public V3Direction throwDirection;
    /// <summary>
    /// The event that fires once a card is drawn
    /// </summary>
    /// <returns>
    /// The card value that was drawn
    /// </returns>
    public StringEventWrapper OnCardDrawn;
    
    /// <summary>
    /// The Deck of Cards this deals from (made up of 4 full decks, all dealers share one deck)
    /// </summary>
    private static CardDeckDataModel deck = new CardDeckDataModel(4);
    /// <summary>
    /// The direction at which the playing card will be thrown
    /// </summary>
    private Vector3 pushDirection;
    #endregion

    private void Start()
    {
        //print(name + " " + deck.CardCount);

        //Determine push direction by throw direction (lol)
        switch (throwDirection)
        {
            case V3Direction.UP:
                pushDirection = Vector3.up;
                break;
            case V3Direction.DOWN:
                pushDirection = Vector3.down;
                break;
            case V3Direction.LEFT:
                pushDirection = Vector3.left;
                break;
            case V3Direction.RIGHT:
                pushDirection = Vector3.right;
                break;
            case V3Direction.FORWARD:
                pushDirection = Vector3.forward;
                break;
            case V3Direction.BACK:
                pushDirection = Vector3.back;
                break;
            default:
                pushDirection = Vector3.zero;
                break;
        }
    }
    
    /// <summary>
    /// Deals a card
    /// </summary>
    public void DealCard()
    {
        if (deck.CardCount > 0)
        {
            //Draw one card
            string draw = deck.DrawCards(1)[0];

            //Spawn playing card
            GameObject a = Instantiate(playingCard, transform.position, Quaternion.identity);

            //Fire Off Card Drawn Event
            OnCardDrawn.Invoke(draw);

            //Fill out Playing card
            PlayingCard pc = a.GetComponent<PlayingCard>();
            pc.InitilizeCard(draw);

            //Spawn it face up or down
            if (dealFaceUp)
            {
                a.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
            }
            else
            {
                a.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
            }

            //Push the card!
            Rigidbody rb = a.GetComponent<Rigidbody>();
            rb.AddForce((dealForce * .01f) * pushDirection, ForceMode.Impulse);

            //Check the card count (just in case)
            print(name + " " + deck.CardCount);
        }
    }
}