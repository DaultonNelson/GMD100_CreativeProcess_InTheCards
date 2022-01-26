using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayingCard : MonoBehaviour
{
    #region Variables
    public TextMeshPro cardSuite001;
    public TextMeshPro cardSuite002;
    public TextMeshPro cardValue001;
    public TextMeshPro cardValue002;

    public int cardNumericValue = 0;
    #endregion

    public void InitilizeCard(string cardInfo)
    {
        cardSuite001.text = cardInfo[0].ToString();
        cardSuite002.text = cardInfo[0].ToString();

        cardValue001.text = cardInfo[1] == '0' ? "10" : cardInfo[1].ToString();
        cardValue002.text = cardInfo[1] == '0' ? "10" : cardInfo[1].ToString();

        char stringValue = cardInfo[1];
        int cardValue = 0;

        switch (stringValue)
        {
            case 'A':
                cardValue = 11;
                break;
            case 'J': case 'Q': case 'K': case '0':
                cardValue = 10;
                break;
            default:
                cardValue = int.Parse(stringValue.ToString());
                break;
        }

        cardNumericValue = cardValue;
    }
}