using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ExtensionMethods
{
    /// <summary>
    /// Shuffles a list of T
    /// </summary>
    /// <typeparam name="T">
    /// A generic type, can be anything
    /// </typeparam>
    /// <param name="list">
    /// The list to shuffle
    /// </param>
    /// <returns>
    /// A shuffled list
    /// </returns>
    public static List<T> Shuffle<T>(this List<T> list)
    {
        Random rnd = new Random();

        List<T> output = new List<T>();
        List<T> oldList = new List<T>(list);

        while (oldList.Count > 0)
        {
            T pick = oldList[rnd.Next(0, oldList.Count)];
            output.Add(pick);
            oldList.Remove(pick);
        }

        return output;
    }
}