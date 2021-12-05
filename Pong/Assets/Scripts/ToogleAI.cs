using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToogleAI : MonoBehaviour
{
    public static bool aiActive = false;

    public void makeAiActive()
    {
        aiActive = !aiActive;
    }

    public bool isAiActive()
    {
        return aiActive;
    }
}
