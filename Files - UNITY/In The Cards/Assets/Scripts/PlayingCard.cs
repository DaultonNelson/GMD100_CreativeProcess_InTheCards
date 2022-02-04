using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayingCard : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Represents one card suit text component.
    /// </summary>
    public TextMeshPro cardSuite001;
    /// <summary>
    /// Represents a second card suit text component.
    /// </summary>
    public TextMeshPro cardSuite002;
    /// <summary>
    /// Represents one card value text component.
    /// </summary>
    public TextMeshPro cardValue001;
    /// <summary>
    /// Represents a second card value text component.
    /// </summary>
    public TextMeshPro cardValue002;
    /// <summary>
    /// Represents one card value face component.
    /// </summary>
    public TextMeshPro cardFaceText;

    /// <summary>
    /// The numeric value of the card.
    /// </summary>
    public int cardNumericValue = 0;

    /// <summary>
    /// The texture that represents the Jack face.
    /// </summary>
    public Texture2D jackTexture;
    /// <summary>
    /// The texture that represents the Queen face.
    /// </summary>
    public Texture2D queenTexture;
    /// <summary>
    /// The texture that represents the King face.
    /// </summary>
    public Texture2D kingTexture;

    /// <summary>
    /// The Renderer component attached to the card
    /// </summary>
    private Renderer cardRenderer;
    #endregion

    /// <summary>
    /// Initilizes the Playing card
    /// </summary>
    /// <param name="cardInfo">
    /// A two character string used to determine card value and display
    /// </param>
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

    /// <summary>
    /// Fills the card's face with suites
    /// </summary>
    /// <param name="amt">
    /// The amount of suites that will fill the face
    /// </param>
    /// <param name="suite">
    /// The type of suite that will feed the face
    /// </param>
    private void FillFaceText(int amt, char suite)
    {
        for (int i = 0; i < amt; i++)
        {
            cardFaceText.text += suite;
        }
    }
}