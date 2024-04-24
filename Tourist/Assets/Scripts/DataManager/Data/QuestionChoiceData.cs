using System.Collections.Generic;

namespace DataNamespace
{
    [System.Serializable]
    public class QuestionChoiceData
    {
        public string nameQuestion;
        public int valueStamina;
        public Answers[] answerResult;

        public QuestionChoiceData(string name, Answers[] answers, int value)
        {
            nameQuestion = name;
            answerResult = answers;
            valueStamina = value;
        }
    }

    [System.Serializable]
    public class Answers
    {
        public string answer;
        public bool isOn;
    }
}