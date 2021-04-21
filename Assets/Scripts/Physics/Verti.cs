using UnityEngine;

[System.Serializable]
public class Verti
{
    private bool softDropCS = false;    // Soft Drop Controller State
    private bool hardDropCS = false;    // Hard Drop Controller State
    private float dropCoolDown = 0f;     // if >1 then drop 1 block
    private bool lastMove = false;   // Last hardDropCS
    private float actionDelayCoolDown = 0f;
    private Tester tester;
    private GameSettings settings;
    private BoardState state;

    public Verti(GameSettings settings, Tester tester, BoardState state)
    {
        this.settings = settings;
        this.tester = tester;
        this.state = state;
    }

    public void ExeVerti(float deltaTime)     // check whether the piece should drop, call every step
    {
        // check if dropping is allowed
        if (DropCheck(state.pos, state.rot))
        {
            // increment cooldown
            float dropMultiplier = 1f;
            if (softDropCS) { dropMultiplier = settings.dropMultiplier; }
            dropCoolDown += deltaTime * settings.dropSpeed * dropMultiplier;

            // check if it can actually drop
            if (dropCoolDown > 1f)
            {
                state.deltaY(-1);
                state.rotIsLast = false;
                dropCoolDown -= 1f;
            }

            // prevent locking mid air
            state.resetHardLock();
        }
        else    // there must be something blocking beneath
        {
            dropCoolDown = 0f;
        }
    }

    public void ExeHardDrop(float deltaTime)     // check whether it should hard drop, call every step
    {
        int deltaY = 0;

        // increment cps cap timer
        if (actionDelayCoolDown < settings.actionDelay)
        {
            actionDelayCoolDown += deltaTime;
        }

        // record last control to prevent multiple harddrop when holding, force to release then press again
        lastMove = hardDropCS;

        // check if there is an intention to hard drop
        if (lastMove != hardDropCS && hardDropCS && actionDelayCoolDown >= settings.actionDelay)
        {
#if UNITY_EDITOR
            // debugging var
            int debugging = 0;
#endif

            // keep moving down until not allowed anymore
            while (DropCheck(state.pos + new Vector2Int(0, deltaY), state.rot))
            {
                // move down 1 unit
                deltaY--;
                state.rotIsLast = false;

#if UNITY_EDITOR
                // Infinite loop prevention
                debugging++;
                if (debugging > 50) { Debug.Log("Infinite Loop Occured"); break; }
#endif
            }
            state.deltaY(deltaY);

            // reset cps cap timer
            actionDelayCoolDown = 0f;
        }
    }

    private bool DropCheck(Vector2Int pos, int rot)    // check if dropping downward by 1 block is allowed
    {
        return tester.OccupationTest(pos + new Vector2Int(0, -1), rot);
    }
}

