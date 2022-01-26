using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ExtensionMethods
{
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