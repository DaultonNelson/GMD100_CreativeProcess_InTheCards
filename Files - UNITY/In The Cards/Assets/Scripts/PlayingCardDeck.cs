using System.Collections;
using System.Collections.Generic;

//A123456789 10 J Q K
//♠♥♦♣

public class PlayingCardDeck
{
    #region Variables
    public Queue<string> deckContents = new Queue<string>();

    private string suites = "♠♥♦♣";
    private string values = "234567890JQKA";
    #endregion

    public PlayingCardDeck()
    {
        foreach (char c in suites)
        {
            foreach (char d in values)
            {
                deckContents.Enqueue($"{c}{d}");
            }
        }
    }
}