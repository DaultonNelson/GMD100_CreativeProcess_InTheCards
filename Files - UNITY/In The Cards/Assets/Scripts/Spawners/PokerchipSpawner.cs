using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerchipSpawner : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// The time between each poker chip spawn
    /// </summary>
    public float secondsBetweenSpawns = 0.5f;
    /// <summary>
    /// The pokerchip prefab
    /// </summary>
    public GameObject pokerchip;

    /// <summary>
    /// The texture for the value 25 poker chip
    /// </summary>
    [Header("Pokerchip Textures")]
    public Texture2D chip_25;
    /// <summary>
    /// The texture for the value 100 poker chip
    /// </summary>
    public Texture2D chip_100;
    /// <summary>
    /// The texture for the value 200 poker chip
    /// </summary>
    public Texture2D chip_200;
    /// <summary>
    /// The texture for the value 500 poker chip
    /// </summary>
    public Texture2D chip_500;
    #endregion

    /// <summary>
    /// Converts an int value into poker chips
    /// </summary>
    /// <param name="chipValue">
    /// The value being converted
    /// </param>
    public void ConvertValueToChips(int chipValue)
    {
        int fivehundreds = chipValue / 500;
        chipValue -= fivehundreds * 500;
        int twohundreds = chipValue / 200;
        chipValue -= twohundreds * 200;
        int onehundreds = chipValue / 100;
        chipValue -= onehundreds * 100;
        int twentyfives = chipValue / 25;
        chipValue -= twentyfives * 25;

        List<int> chipAmounts = new List<int>() { fivehundreds, twohundreds, onehundreds, twentyfives };
        if (chipValue > 0)
        {
            print("Left overs: " + chipValue); 
        }
        StartCoroutine(Spawner(chipAmounts));
    }

    /// <summary>
    /// Spawns Pokerchips
    /// </summary>
    /// <param name="chipAmts">
    /// The amounts of each poker chip type to spawn
    /// </param>
    IEnumerator Spawner(List<int> chipAmts)
    {
        for (int h = 0; h < chipAmts.Count; h++)
        {
            for (int i = 0; i < chipAmts[h]; i++)
            {
                yield return new WaitForSeconds(secondsBetweenSpawns);
                GameObject a = Instantiate(pokerchip, transform.position, Quaternion.identity);
                Renderer r = a.GetComponent<Renderer>();

                switch (h)
                {
                    case 0:
                        a.name += " 500";
                        r.material.color = Color.yellow;
                        r.materials[1].mainTexture = chip_500; 
                        break;
                    case 1:
                        a.name += " 200";
                        r.material.color = Color.red;
                        r.materials[1].mainTexture = chip_200; 
                        break;
                    case 2:
                        a.name += " 100";
                        r.material.color = Color.magenta;
                        r.materials[1].mainTexture = chip_100;
                        break;
                    case 3:
                        a.name += " 025";
                        r.material.color = new Color(1f, 0.5f, 0f, 1f);
                        r.materials[1].mainTexture = chip_25;
                        break;
                    default:
                        Debug.LogError("Index invalid!", gameObject);
                        break;
                }
            } 
        }
    }
}