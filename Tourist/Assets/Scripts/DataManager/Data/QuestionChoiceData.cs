using System.Collections.Generic;

namespace DataNamespace
{
    [System.Serializable]
    public class QuestionChoiceData
    {
        public string nameQuestion;
        public Answers[] answerResult;

        public QuestionChoiceData(string name, Answers[] answers)
        {
            nameQuestion = name;
            answerResult = answers;
        }
    }

    [System.Serializable]
    public class Answers
    {
        public string answer;
        public bool isOn;
    }
}