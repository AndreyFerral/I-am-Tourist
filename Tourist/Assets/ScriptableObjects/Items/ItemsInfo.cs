using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Objects/Item")]
public class ItemsInfo : ScriptableObject
{
    [SerializeField] string nameItem;
    [SerializeField] int weightItem;

    public string NameItem => nameItem;
    public int WeightItem => weightItem;
}
