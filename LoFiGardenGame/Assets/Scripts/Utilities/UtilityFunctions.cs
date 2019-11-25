using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityFunctions
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

    public static GameObject GetChildRecursively(Transform parent, string tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            if (child.CompareTag(tag))
            {
                return child.gameObject; 
            }

            if (child.childCount > 0)
            {
                GameObject ret = GetChildRecursively(child, tag);

                if (ret != null)
                {
                    return ret;
                }
            }
        }

        return null;
    }
}
