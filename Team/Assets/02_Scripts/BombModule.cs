using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BombModule : MonoBehaviour
{
    public enum BombMoudleType
    {
        Wire,
        Keypad,
        Maze

    }

    public BombMoudleType moduleType;

    protected int incorrectCnt = 0;
    protected bool isDefused = false;
    int num = 0;

    public abstract void InitiallizeModule();

    public bool IsDefused()
    {
        return isDefused;
    }

    public abstract void DefuseModule();

    public abstract void Fail();
}
