using UnityEngine;

[CreateAssetMenu(fileName = "BackpackData", menuName = "Objects/Backpack")]
public class BackpackInfo : ScriptableObject
{
    [SerializeField] int idBackpack;
    [SerializeField] string nameBackpack;
    [SerializeField] float stamina;

    public int IdBackpack => idBackpack;
    public string NameBackpack => nameBackpack;
    public float Stamina => stamina;
}