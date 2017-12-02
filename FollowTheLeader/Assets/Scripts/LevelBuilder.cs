using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {

    public SquareBehaviour[] prefabs;
    private Queue<SquareBehaviour> levels;
    private SquareBehaviour lastSquare;

    private void Start()
    {
        lastSquare = Instantiate<SquareBehaviour>(prefabs[Random.Range(0, prefabs.Length)]);
        lastSquare.transform.position = Vector3.zero;
        levels.Enqueue(lastSquare);
    }

    [ContextMenu("AddSquare")]
    public void AddSquare()
    {
        levels.Enqueue(GenerateSquare());
    }

    private SquareBehaviour GenerateSquare()
    {
        SquareBehaviour result = Instantiate<SquareBehaviour>(prefabs[Random.Range(0, prefabs.Length)]);
        result.LinkToPrevious(lastSquare);
        lastSquare = result;
        return result;
    }
}
