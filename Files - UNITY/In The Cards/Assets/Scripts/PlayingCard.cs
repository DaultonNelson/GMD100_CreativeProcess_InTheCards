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
    public TextMeshPro cardFaceText;

    public int cardNumericValue = 0;

    public Texture2D jackTexture;
    public Texture2D queenTexture;
    public Texture2D kingTexture;

    private Renderer cardRenderer;
    #endregion

    public void InitilizeCard(string cardInfo)
    {
        cardRenderer = GetComponent<Renderer>();

        //Suites
        char suite = cardInfo[0];

        cardSuite001.text = suite.ToString();
        cardSuite002.text = suite.ToString();

        //If suite is hearts or diamonds, make text red
        if (suite == '♥' || suite == '♦')
        {
            cardSuite001.color = Color.red;
            cardSuite002.color = Color.red;
            cardValue001.color = Color.red;
            cardValue002.color = Color.red;
            cardFaceText.color = Color.red;
        }

        //Values
        cardValue001.text = cardInfo[1] == '0' ? "10" : cardInfo[1].ToString();
        cardValue002.text = cardInfo[1] == '0' ? "10" : cardInfo[1].ToString();

        char stringValue = cardInfo[1];
        int cardValue = 0;

        switch (stringValue)
        {
            case 'A':
                cardValue = 11;
                cardFaceText.text = "A";
                break;
            case '0':
                cardValue = 10;
                FillFaceText(cardValue, suite);
                break;
            case 'J':
                cardValue = 10;
                cardRenderer.materials[1].mainTexture = jackTexture;
                break;
            case 'Q':
                cardValue = 10;
                cardRenderer.materials[1].mainTexture = queenTexture;
                break;
            case 'K':
                cardValue = 10;
                cardRenderer.materials[1].mainTexture = kingTexture;
                break;
            default:
                cardValue = int.Parse(stringValue.ToString());
                FillFaceText(cardValue, suite);
                break;
        }

        cardNumericValue = cardValue;
    }

    private void FillFaceText(int amt, char suite)
    {
        for (int i = 0; i < amt; i++)
        {
            cardFaceText.text += suite;
        }
    }
}