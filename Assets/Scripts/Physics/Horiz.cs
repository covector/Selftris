﻿using UnityEngine;

[System.Serializable]
public class Horiz
{
    private int horizCS = 0;    // Horizontal Movement Controller State
    private int lastMove = 0;   // control in the last step
    private float moveCoolDown = 0f;     // if >1 then move right, if <1 then move left
    private float cpsCapCoolDown = 0f;
    private Tester tester;
    private GameSettings settings;

    public Horiz(GameSettings settings, Tester tester)
    {
        this.settings = settings;
        this.tester = tester;
    }

    public Effect ExeHoriz(Vector2Int pos, int rot, float deltaTime)     // check whether the piece should move horizontally, call every step
    {
        // hold to move
        moveCoolDown += deltaTime * settings.horizontalSpeed * horizCS;

        // change of the force direction
        if (horizCS != lastMove)
        {
            moveCoolDown = 0;
        }

        // tap to move
        if (cpsCapCoolDown < 1f)
        {
            cpsCapCoolDown += deltaTime / settings.cpsCap;
        }

        // record last control
        lastMove = horizCS;

        // reset Locking due to Horizontal Movement
        bool softResetLock = horizCS != 0;

        // register hoizontal movement
        int deltaX = 0;
        bool move = IntentCheck() && FeasibCheck(pos, rot);
        if (move)
        {
            deltaX = horizCS;
        }

        return new Effect(deltaX, 0, 0, softResetLock, false, move);
    }

    private bool IntentCheck()
    {
        // check if the piece gain enough momentum to move
        if (horizCS * moveCoolDown > settings.accelerateDelay)
        {
            moveCoolDown -= horizCS;
            return true;
        }

        // tap to Move
        if (lastMove != horizCS && horizCS != 0 && cpsCapCoolDown >= 1f)
        {
            cpsCapCoolDown = 0f;
            return true;
        }

        return false;
    }

    private bool FeasibCheck(Vector2Int pos, int rot)
    {
        return tester.OccupationTest(pos + new Vector2Int(horizCS, 0), rot);
    }
}

