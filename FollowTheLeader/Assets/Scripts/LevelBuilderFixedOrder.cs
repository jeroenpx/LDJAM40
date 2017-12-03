using System.Collections.Generic;
using UnityEngine;

public class LevelBuilderFixedOrder : MonoBehaviour {

    public int SquaresAhead = 2;
    public int SquaresTotal = 5;
    public SquareBehaviour connectSquare;
    public SquareBehaviour[] prefabs;
    private Queue<SquareBehaviour> levels;

	public float followerSpawnRadius=2;
	public float followerSpawnProb=0.3f;
	public int followerSpawnMin=2;
	public int followerSpawnMax=6;
	public Transform followerSpawnPrefab;

    private int highestNr = 0;
    private int prefabIndex = 0;

    private void Awake()
    {
        connectSquare.CommanderEnteredEvent += CommanderEnteredHandler;
        levels = new Queue<SquareBehaviour>();
        levels.Enqueue(connectSquare);
    }

    private void Update()
    {
        while (connectSquare.getSerialNr() < (highestNr + SquaresAhead))
        {
            connectSquare = GenerateSquare();
            levels.Enqueue(connectSquare);
        }

        while (levels.Count > SquaresTotal)
        {
            SquareBehaviour trail = levels.Dequeue();
            trail.Dissolve();
			levels.Peek ().CloseStart ();
        }
    }

    [ContextMenu("AddSquare")]
    public void AddSquare()
    {
        levels.Enqueue(GenerateSquare());
    }

    private SquareBehaviour GenerateSquare()
    {
        SquareBehaviour result = Instantiate<SquareBehaviour>(prefabs[prefabIndex++]);
        result.transform.SetParent(this.transform);
        result.CommanderEnteredEvent += CommanderEnteredHandler;
        result.LinkToPrevious(connectSquare);
		BuildFollowers (result.transform);
        return result;
    }

	private void BuildFollowers(Transform transform){
		int count = transform.childCount;
		for (int i = 0; i < count; i++) {
			Transform child = transform.GetChild (i);
			if (child.CompareTag ("FollowerSpawnPoint")) {
				if (Random.value < followerSpawnProb) {
					int spawnCount = Random.Range (followerSpawnMin, followerSpawnMax);
					for(int j=0;j<spawnCount;j++) {
						Vector3 spawnPos = child.transform.position+(Vector3)Random.insideUnitCircle*followerSpawnRadius;
						Instantiate(followerSpawnPrefab, spawnPos, Quaternion.identity);
						GameObject.Destroy(child.gameObject);
					}
				}
			}
		}
	}

    private void CommanderEnteredHandler(SquareBehaviour s, Commander c)
    {
        if (s.getSerialNr() > highestNr)
        {
            highestNr = s.getSerialNr();
        }
    }

    public void AddSquareFromTutorial(SquareBehaviour s)
    {
        if (s.getSerialNr() < connectSquare.getSerialNr())
        {
            Debug.LogError("squares from tutorial have been added in the wrong sequence; this would have them destroyed in the wrong order");
        }
        else
        {
            this.connectSquare = s;
        }
        s.CommanderEnteredEvent += CommanderEnteredHandler;
        levels.Enqueue(s);
    }
}
