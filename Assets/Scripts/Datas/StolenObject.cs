using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StolenObject
{
    [SerializeField]
    string name = "";
    [SerializeField]
    float price = 0f;

    public StolenObject(string name, float price)
    {
        this.Name = name;
        this.Price = price;
    }

    public string Name { get => name; set => name = value; }
    public float Price { get => price; set => price = value; }
}
