using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BeatData
{
    [SerializeField] private List<ChoiceData> _choices;
    [SerializeField] private string _text;
    [SerializeField] private int _id;
    //[SerializeField] private ChoiceType _type;
    //[SerializeField] private string _functionName;

    //public ChoiceType GetChoiceType { get { return _type; } }
    public List<ChoiceData> Decision { get { return _choices; } }
    public string DisplayText { get { return _text; } }
    public int ID { get { return _id; } }
}
