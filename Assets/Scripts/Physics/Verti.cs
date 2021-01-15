using UnityEngine;

[System.Serializable]
public class Verti
{
    private bool softDropCS = false;    // Soft Drop Controller State
    private bool hardDropCS = false;    // Hard Drop Controller State
    private float dropCoolDown = 0f;     // if >1 then drop 1 block
    private Tester tester;
    private GameSettings settings;

    public Verti(GameSettings settings, Tester tester)
    {
        this.settings = settings;
        this.tester = tester;
    }

    public Effect ExeVerti(Vector2Int pos, int rot, float deltaTime)     // check whether the piece should drop, call every step
    {
        bool hardResetLock = false;
        bool resetRotIsLast = false;
        int deltaY = 0;

        // check if dropping is allowed
        if (DropCheck(pos, rot))
        {
            // increment cooldown
            float dropMultiplier = 1f;
            if (softDropCS) { dropMultiplier = settings.dropMultiplier; }
            dropCoolDown += deltaTime * settings.dropSpeed * dropMultiplier;

            // check if it can actually drop
            if (dropCoolDown > 1f)
            {
                deltaY = -1;
                resetRotIsLast = false;
                dropCoolDown -= 1f;
            }

            // prevent locking mid air
            hardResetLock = true;
        }
        else    // there must be something blocking beneath
        {
            dropCoolDown = 0f;
        }

        return new Effect(0, deltaY, 0, false, hardResetLock, resetRotIsLast);
    }

    public Effect ExeHardDrop(Vector2Int pos, int rot)     // check whether it should hard drop, call every step
    {
        int deltaY = 0;
        bool resetRotIsLast = false;

        // check if there is an intention to hard drop
        if (hardDropCS)
        {
#if UNITY_EDITOR
            // debugging var
            int debugging = 0;
#endif

            // keep moving down until not allowed anymore
            while (DropCheck(pos + new Vector2Int(0, deltaY), rot))
            {
                // move down 1 unit
                deltaY--;
                resetRotIsLast = true;

#if UNITY_EDITOR
                // Infinite loop prevention
                debugging++;
                if (debugging > 50) { Debug.Log("Infinite Loop Occured"); break; }
#endif
            }

            // set hard drop controller state to false to prevent double hard dropping
            hardDropCS = false;
        }

        return new Effect(0, deltaY, 0, false, false, resetRotIsLast);
    }

    private bool DropCheck(Vector2Int pos, int rot)    // check if dropping downward by 1 block is allowed
    {
        return tester.OccupationTest(pos + new Vector2Int(0, -1), rot);
    }
}

