using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//The tutorial level will not spawn random squares, but show a fixed sequence of squares
public class TutorialBuilder : MonoBehaviour {
    public SquareBehaviour connectSquare;
    public Follower followerSpawnPrefab;

    [System.Serializable]
    public struct TutorialSequence
    {
        public string tutorialText;
        public SquareBehaviour square;
    }
    public SquareBehaviour[] fixedSequence;
    private Queue<SquareBehaviour> levels;

    private void Start()
    {
        levels = new Queue<SquareBehaviour>();
        //connectSquare = Instantiate<SquareBehaviour>(prefabs[Random.Range(0, prefabs.Length)]);
        //connectSquare.transform.position = Vector3.zero;
        SquareBehaviour lastSquare = connectSquare;
        for(int i = 0; i<fixedSequence.Length; ++i)
        {
            var t = fixedSequence[i];
            SquareBehaviour n = Instantiate<SquareBehaviour>(t);
            n.transform.SetParent(this.transform);
            n.LinkToPrevious(lastSquare);
            BuildFollowers(n.transform, ((float) fixedSequence.Length)/i, i);
            levels.Enqueue(n);
            lastSquare = n;
        }
    }


    private void BuildFollowers(Transform transform, float followerSpawnProb, int spawnCount)
    {
        float followerSpawnRadius = 1f;
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.CompareTag("FollowerSpawnPoint"))
            {
                if (Random.value < followerSpawnProb)
                {
                    Debug.Log("Building " + spawnCount + " followers");
                    for (int j = 0; j < spawnCount; j++)
                    {
                        Vector3 spawnPos = child.transform.position + (Vector3)Random.insideUnitCircle * followerSpawnRadius;
                        Instantiate(followerSpawnPrefab, spawnPos, Quaternion.identity);
                        GameObject.Destroy(child.gameObject);
                    }
                }
            }
        }
    }

}
