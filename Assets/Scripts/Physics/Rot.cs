using UnityEngine;

[System.Serializable]
public class Rot
{
    private int rotateCS = 0;    // Rotate Controller State
    private int lastMove = 0;   // Last rotateCS
    private float actionDelayCoolDown = 0f;
    private Tester tester;
    private GameSettings settings;
    private BoardState state;

    public Rot(GameSettings settings, Tester tester, BoardState state)
    {
        this.settings = settings;
        this.tester = tester;
        this.state = state;
    }

    public void ExeRot(float deltaTime)     // check whether it should rotate, call every step
    {
        // increment cps cap timer
        if (actionDelayCoolDown < settings.actionDelay)
        {
            actionDelayCoolDown += deltaTime;
        }

        // record last control to prevent multiple rotation when holding, force to release then press again
        lastMove = rotateCS;

        int newRot = (state.rot + rotateCS + 4) % 4;  // Assumed that |direction| <= 4

        if (lastMove != rotateCS && rotateCS != 0 && actionDelayCoolDown >= settings.actionDelay)
        {
            if (tester.WallKickTest(state.rot, rotateCS, newRot))
            {
                state.rot = newRot;
                state.rotIsLast = true;
                state.resetSoftLock();

                // reset cps cap timer
                actionDelayCoolDown = 0f;
            }
        }
    }
}

