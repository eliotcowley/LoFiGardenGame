using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rollover
{
    public static int Increment(int current, int max)
    {
        if (current == max)
        {
            return 0;
        }
        else
        {
            return current + 1;
        }
    }

    public static int Decrement(int current, int max)
    {
        if (current == 0)
        {
            return max;
        }
        else
        {
            return current - 1;
        }
    }
}
