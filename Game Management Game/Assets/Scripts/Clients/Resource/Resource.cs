using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    int size;
    public Resource(int s)
    {
        size = s;
        if (size > 3)
        {
            size = 3;
        }
        else if (size < 1)
        {
            size = 1;
        }
    }
    public int getValue() { return size; }
}
