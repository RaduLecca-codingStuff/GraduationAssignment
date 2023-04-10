using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    public enum Type
    {
        time,
        resource
    }
    public Type type;
    int size;
    // Start is called before the first frame update
    public ResourceObject(int s, Type t)
    {
        this.type = t;
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
}
