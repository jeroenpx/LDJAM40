using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The tutorial level will not spawn random squares, but show a fixed sequence of squares
public class TutorialBuilder : MonoBehaviour {
    public int SquaresAhead = 2;
    public int SquaresTotal = 5;
    public SquareBehaviour connectSquare;

    [System.Serializable]
    public struct TutorialSequence
    {
        public string tutorialText;
        public SquareBehaviour square;
    }
    public TutorialSequence[] fixedSequence;
    private Queue<SquareBehaviour> levels;

    private void Awake()
    {
        connectSquare.CommanderEnteredEvent += CommanderEnteredEvent;
    }

    private void Start()
    {
        levels = new Queue<SquareBehaviour>();
        //connectSquare = Instantiate<SquareBehaviour>(prefabs[Random.Range(0, prefabs.Length)]);
        //connectSquare.transform.position = Vector3.zero;
        foreach (var t in fixedSequence)
        {
            SquareBehaviour n = Instantiate<SquareBehaviour>(t.square);
            n.transform.SetParent(this.transform);
            n.CommanderEnteredEvent += CommanderEnteredEvent;
            n.LinkToPrevious(connectSquare);
            levels.Enqueue(n);
            connectSquare = n;
        }
    }

    private void CommanderEnteredEvent(SquareBehaviour s, Commander c)
    {
    }
}
