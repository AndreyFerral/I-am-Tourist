using UnityEngine;

[CreateAssetMenu(fileName = "CraftData", menuName = "Objects/Craft")]
public class CraftInfo : ScriptableObject
{
    [SerializeField] ItemsInfo[] craftItems;
    [SerializeField] ItemsInfo[] deleteItems;
    [SerializeField] ItemsInfo craftedItem;

    public ItemsInfo[] CraftItems => craftItems;
    public ItemsInfo[] DeleteItems => deleteItems;
    public ItemsInfo CraftedItem => craftedItem;

}