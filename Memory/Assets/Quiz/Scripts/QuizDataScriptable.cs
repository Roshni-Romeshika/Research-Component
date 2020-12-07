//Script created by IT17100076
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionsData", menuName = "QuestionsData", order = 1)]
public class QuizDataScriptable : ScriptableObject
{
    //Creates generic collection which contaions questions and it grow automatically when add new questions
    public List<Question> questions;
}
// End of the QuizDataScriptable script