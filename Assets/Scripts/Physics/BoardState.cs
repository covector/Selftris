using UnityEngine;

[System.Serializable]
public class BoardState
{
    public Vector2Int pos;
    public void deltaX(int delta)
    {
        pos += new Vector2Int(delta, 0);
    }
    public void deltaY(int delta)
    {
        pos += new Vector2Int(0, delta);
    }

    public int rot {
        get { return rot; }
        set { rot = (value + 4) % 4; } 
    }

    public bool rotIsLast;

    public float softLockTimer = 0f;
    public void resetSoftLock(bool reset = true)
    {
        if (reset)
        {
            softLockTimer = 0f;
        }
    }

    public float hardLockTimer = 0f;
    public void resetHardLock(bool reset = true)
    {
        if (reset)
        {
            hardLockTimer = 0f;
        }
    }


}