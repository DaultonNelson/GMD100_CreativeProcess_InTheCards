using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardListBoxItem : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// The text that displays the card info
    /// </summary>
    public TMP_Text cardInfoText;
    /// <summary>
    /// The text that displays the actual numeric value of the card
    /// </summary>
    public TMP_Text actualValueText;
    /// <summary>
    /// The image that outlines this list box item
    /// </summary>
    public GameObject itemOutline;
    /// <summary>
    /// A collection of choice buttons for when we get an ace (1 or 11)
    /// </summary>
    public GameObject[] choiceButtons;

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
    /// Fills this list box item based on the given card value
    /// </summary>
    /// <param name="cardInfo">
    /// Information pertaining to the value and suite of the card
    /// </param>
    public void FillItem(string cardInfo, out int num)
    {
        cardInfoText.text = cardInfo;

        //Suites
        char suite = cardInfo[0];

        if (suite == '♥' || suite == '♦')
        {
            cardInfoText.color = Color.red;
        }

        //Values
        char cardVal = cardInfo[1];
        num = GetValueFromInfo(cardVal);

        if (actualValueText.gameObject.activeInHierarchy)
        {
            actualValueText.text = num.ToString();
        }
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
                actualValueText.gameObject.SetActive(false);
                itemOutline.gameObject.SetActive(true);

                foreach (GameObject b in choiceButtons)
                {
                    b.SetActive(true);
                }

                return 0;
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
    
    //TODO: Disable/Enable Action buttons & next turn until player makes a choice
    public void ChoiceButton(int num)
    {
        actualValueText.gameObject.SetActive(true);
        actualValueText.text = num.ToString();
        itemOutline.gameObject.SetActive(false);
        bjc.UpdatePlayerScore(num);

        foreach (GameObject b in choiceButtons)
        {
            b.SetActive(false);
        }
    }
}