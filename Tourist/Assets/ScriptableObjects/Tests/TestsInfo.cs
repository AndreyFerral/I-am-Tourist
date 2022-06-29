using UnityEngine;

[CreateAssetMenu(fileName = "TestData", menuName = "Objects/Test")]
public class TestsInfo : ScriptableObject
{
    [SerializeField] int idTest;
    [SerializeField] string nameTest;
    [SerializeField] string textTest;
    [SerializeField] string[] answerChoices;
    [SerializeField] string[] correctChoices;
    [SerializeField] int positiveEffect;
    [SerializeField] string nameItem;

    public int IdTest => idTest;
    public string NameTest => nameTest;
    public string TextTest => textTest;
    public string[] AnswerChoices => answerChoices;
    public string[] CorrectChoices => correctChoices;
    public int PositiveEffect => positiveEffect;
    public string NameItem => nameItem;
}