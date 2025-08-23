using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog System/Dialog", order = 1)]
public class Dialog : ScriptableObject
{
    [Header("Dialog Text")]
    [SerializeField] public string speakerName;
    [TextArea(3, 10)]
    [SerializeField] public string text;

    [Header("Player Choices")]
    public List<Choice> choices;

    [System.Serializable]
    public class Choice
    {
        [TextArea(1, 3)]
        public string choiceText;
        public int nextNodeIndex;
    }

}
