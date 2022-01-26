using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public GameObject playingCardPrefab; 
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        CardDeckDataModel deck = new CardDeckDataModel(4);

        string drawn =  deck.DrawCard();
        print(drawn);

        GameObject a = Instantiate(playingCardPrefab, Vector3.zero, Quaternion.identity);
        PlayingCard pc = a.GetComponent<PlayingCard>();
        pc.InitilizeCard(drawn);
    }
}