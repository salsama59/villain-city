using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VilainOrganization
{
    private float fund;
    private int level;
    private int minionCount;
    private List<StolenObject> ownedObjects;

    public VilainOrganization(float fund, int level, int minionCount, List<StolenObject> ownedObjects)
    {
        Fund = fund;
        Level = level;
        MinionCount = minionCount;
        OwnedObjects = ownedObjects;
    }

    public float Fund { get => fund; set => fund = value; }
    public int Level { get => level; set => level = value; }
    public int MinionCount { get => minionCount; set => minionCount = value; }
    public List<StolenObject> OwnedObjects { get => ownedObjects; set => ownedObjects = value; }
}
