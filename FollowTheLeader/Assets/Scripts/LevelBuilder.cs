using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {

    public int SquaresAhead = 2;
    public int SquaresTotal = 5;
    public SquareBehaviour connectSquare;
    public SquareBehaviour[] prefabs;
    private Queue<SquareBehaviour> levels;

    private int highestNr = 0;

    private void Start()
    {
        levels = new Queue<SquareBehaviour>();
        //connectSquare = Instantiate<SquareBehaviour>(prefabs[Random.Range(0, prefabs.Length)]);
        //connectSquare.transform.position = Vector3.zero;
        levels.Enqueue(connectSquare);
    }

    private void Update()
    {
        while (connectSquare.getSerialNr() < (highestNr + margin))
        {
            connectSquare = GenerateSquare();
        }

        while (levels.Count > SquaresTotal)
        {
            SquareBehaviour trail = levels.Dequeue();
            trail.Dissolve();
        }
    }

    [ContextMenu("AddSquare")]
    public void AddSquare()
    {
        levels.Enqueue(GenerateSquare());
    }

    private SquareBehaviour GenerateSquare()
    {
        SquareBehaviour result = Instantiate<SquareBehaviour>(prefabs[Random.Range(0, prefabs.Length)]);
        result.transform.SetParent(this.transform);
        result.CommanderEnteredEvent += CommanderEnteredEvent;
        result.LinkToPrevious(connectSquare);
        return result;
    }

    private void CommanderEnteredEvent(SquareBehaviour s, Commander c)
    {
        if (s.getSerialNr() > highestNr)
        {
            highestNr = s.getSerialNr();
        }
    }
}
