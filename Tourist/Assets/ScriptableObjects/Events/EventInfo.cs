using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName = "Objects/Event")]
public class EventInfo : ScriptableObject
{
    [SerializeField] string nameEvent;
    [SerializeField] ItemsInfo[] items;
    [SerializeField] float positiveEffect;
    [SerializeField] float negativeEffect;

    public string NameEvent => nameEvent;
    public ItemsInfo[] Items => items;
    public float PositiveEffect => positiveEffect;
    public float NegativeEffect => negativeEffect;
}
