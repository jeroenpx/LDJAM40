using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {


    public SquareBehaviour connectSquare;
    public SquareBehaviour[] prefabs;
    private Queue<SquareBehaviour> levels;

    private void Start()
    {
        levels = new Queue<SquareBehaviour>();
        //connectSquare = Instantiate<SquareBehaviour>(prefabs[Random.Range(0, prefabs.Length)]);
        //connectSquare.transform.position = Vector3.zero;
        levels.Enqueue(connectSquare);
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
        result.LinkToPrevious(connectSquare);
        connectSquare = result;
        return result;
    }
}
