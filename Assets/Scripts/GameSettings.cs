using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    [Header("Game Physics")]
    public float dropSpeed;
    public float horizontalSpeed;
    public float accelerateDelay;
    public float dropMultiplier;
    public float softLockSpeed;
    public float hardLockSpeed;
    public int previewCount;
    public float actionDelay;

    [Header("Game")]
    public int playerCount;

    [Header("Display")]
    public float spacing;
}
