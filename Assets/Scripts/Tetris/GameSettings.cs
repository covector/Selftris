using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public float dropSpeed;
    public float horizontalSpeed;
    public float accelerateDelay;
    public float dropMultiplier;
    public float lockSpeed;
    public float spawnDelay;
}
