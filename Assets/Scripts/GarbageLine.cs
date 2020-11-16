using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GarbageLine", order = 2)]
public class GarbageLine : ScriptableObject
{
    [Header("Basic")]
    public int[] regular;
    [Header("T-Spin")]
    public int[] tspin;
    public int[] tspinMini;
    [Header("Others")]
    public bool perfectClearAddition;   // treat perfectClear as adding to the basic line sent
    public int perfectClear;
    public int Back2back;
    public int[] combo;
}
