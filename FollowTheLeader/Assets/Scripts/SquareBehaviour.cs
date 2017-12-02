using UnityEngine;

public class SquareBehaviour : MonoBehaviour {

    public Transform entrance;
    public Transform exit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LinkToPrevious(SquareBehaviour prev)
    {
        // we need to match our entrance with the previous exit:
        Vector3 move = prev.exit.position - this.entrance.position;

        //now move the whole square
        this.transform.position += move;
    }
}
