using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    public static Database instance { get; private set; }
    
    public Item[] ItemDataBase;

    void Awake()
    {
        instance = this;
    }
}