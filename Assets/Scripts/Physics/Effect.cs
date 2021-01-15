using UnityEngine;

[System.Serializable]
public class Effect
{
    private Vector2Int deltaPos;
    private int deltaRot;
    private bool softResetLock;
    private bool hardResetLock;
    private bool resetRotIsLast;

    public Effect(int deltaX = 0, int deltaY = 0, int deltaRot = 0, bool softResetLock = false, bool hardResetLock = false, bool resetRotIsLast = false)
    {
        deltaPos = new Vector2Int(deltaX, deltaY);
        this.deltaRot = deltaRot;
        this.softResetLock = softResetLock;
        this.hardResetLock = hardResetLock;
        this.resetRotIsLast = resetRotIsLast;
    }
}

