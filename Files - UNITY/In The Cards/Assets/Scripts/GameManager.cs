using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayingCardDeck deck = new PlayingCardDeck();
        foreach (string i in deck.deckContents)
        {
            print(i);
        }
    }
}