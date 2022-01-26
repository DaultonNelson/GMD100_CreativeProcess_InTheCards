using System.Collections;
using System.Collections.Generic;
using System.Text;

//A123456789 10 J Q K
//♠♥♦♣

public class CardDeckDataModel
{
    #region Variables
    /// <summary>
    /// A Queue, which is a FIFO (First In, First Out) collection.  Perfect for drawing playing cards
    /// </summary>
    private Queue<string> deckContents = new Queue<string>();

    /// <summary>
    /// A string containing all the playing card suites.
    /// </summary>
    private string suites = "♠♥♦♣";
    /// <summary>
    /// A string containing all the playing card values.
    /// </summary>
    private string values = "234567890JQKA";
    #endregion

    /// <summary>
    /// A Playing Card Deck Data Model
    /// </summary>
    /// <param name="numberOfDecks">
    /// Determines how many playing card sets we generate into one deck
    /// </param>
    public CardDeckDataModel(int numberOfDecks)
    {
        List<string> generatedCards = new List<string>();

        for (int i = 0; i < numberOfDecks; i++)
        {
            foreach (char c in suites)
            {
                foreach (char d in values)
                {
                    generatedCards.Add($"{c}{d}");
                }
            } 
        }

        generatedCards = new List<string>(generatedCards.Shuffle());

        deckContents = new Queue<string>(generatedCards.ToArray());
    }

    public string DrawCard()
    {
        return deckContents.Dequeue();
    }

    public override string ToString()
    {
        StringBuilder output = new StringBuilder();

        int i = 0;

        foreach (string item in deckContents)
        {
            output.Append($"[{item}] ");
            i++;
            if (i != 0 && i % 13 == 0)
            {
                output.Append("\n");
            }
        }

        return output.ToString();
    }
}