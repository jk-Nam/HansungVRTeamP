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

    protected bool isDefused = false;

    public abstract void InitiallizeModule();

    public bool IsDefused()
    {
        return isDefused;
    }

    public abstract void DefuseModule();
}
