using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WorldEvent 
{
    [SerializeField]
    private string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private EventCost cost;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public EventCost Cost { get => cost; set => cost = value; }
}
