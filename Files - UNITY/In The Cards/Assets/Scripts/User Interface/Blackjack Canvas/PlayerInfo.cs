using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// The Text that displays how much money the player has
    /// </summary>
    public TMP_Text playerMoneyText;
    /// <summary>
    /// The Text that displays what kind of bet the player has made
    /// </summary>
    public TMP_Text playerBetText;
    /// <summary>
    /// The numeric amout of money the player has
    /// </summary>
    public int playerMoneyValue = 5000;
    #endregion

    private void Start()
    {
        playerMoneyText.text = $"Money: ${playerMoneyValue}";
    }

}