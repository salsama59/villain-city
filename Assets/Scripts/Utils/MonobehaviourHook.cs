using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonobehaviourHook : MonoBehaviour
{
    public Action onUpdate;

    // Update is called once per frame
    void Update()
    {
        if(onUpdate != null)
        {
            this.onUpdate();
        }
    }
}
