using UnityEngine;

[System.Serializable]
public class Horiz
{
    private int lastMove = 0;   // control in the last step
    private float moveCoolDown = 0f;     // if >1 then move right, if <1 then move left
    private float actionDelayCoolDown = 0f;
    private Tester tester;
    private GameSettings settings;
    private BoardState state;

    public Horiz(GameSettings settings, Tester tester, BoardState state)
    {
        this.settings = settings;
        this.tester = tester;
        this.state = state;
    }

    public void ExeHoriz(int horiz, float deltaTime)     // check whether the piece should move horizontally, call every step
    {
        // hold to move
        moveCoolDown += deltaTime * settings.horizontalSpeed * horiz;

        // change of the force direction
        if (horiz != lastMove)
        {
            moveCoolDown = 0;
        }

        // tap to move
        if (actionDelayCoolDown < settings.actionDelay)
        {
            actionDelayCoolDown += deltaTime;
        }

        // record last control
        lastMove = horiz;

        // reset Locking due to Horizontal Movement
        state.resetSoftLock(horiz != 0);

        // register hoizontal movement
        bool move = IntentCheck(horiz) && FeasibCheck(horiz);
        if (move)
        {
            state.deltaX(horiz);
        }
    }

    private bool IntentCheck(int horiz)
    {
        // check if the piece gain enough momentum to move
        if (horiz * moveCoolDown > settings.accelerateDelay)
        {
            moveCoolDown -= horiz;
            return true;
        }

        // tap to Move
        if (lastMove != horiz && horiz != 0 && actionDelayCoolDown >= settings.actionDelay)
        {
            actionDelayCoolDown = 0f;
            return true;
        }

        return false;
    }

    private bool FeasibCheck(int horiz)
    {
        return tester.OccupationTest(state.pos + new Vector2Int(horiz, 0), state.rot);
    }
}

