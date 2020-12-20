using System;
using UnityEngine;

[Serializable]
public class ChoiceData
{
    [SerializeField] private string _text;
    [SerializeField] private int _beatId;
    [SerializeField] private ChoiceType _type;
    [SerializeField] private string _functionName;

    public string DisplayText { get { return _text; } }
    public int NextID { get { return _beatId; } }
    public ChoiceType GetChoiceType { get { return _type; } }
}

public enum ChoiceType
{
    Function,
    Text,
}