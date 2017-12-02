using UnityEngine;

public class SquareBehaviour : MonoBehaviour {

    public Transform entrance;
    public Transform exit;
    private int nr;
    private static int serial = 0;

    public delegate void CommanderEnteredHandler(SquareBehaviour s, Commander c);
    public event CommanderEnteredHandler CommanderEnteredEvent;

	// Use this for initialization
	void Awake () {
        this.nr = ++serial;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //to be called by level builder to make sure each new square connects to the previous
    public void LinkToPrevious(SquareBehaviour prev)
    {
        // we need to match our entrance with the previous exit:
        Vector3 move = prev.exit.position - this.entrance.position;

        //now move the whole square
        this.transform.position += move;
    }

    //to be called by level builder when the hero has advanced far enough. Will trigger destruction animation
    public void Dissolve()
    {
        Destroy(this.gameObject);
    }

    public int getSerialNr()
    {
        return nr;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.CommanderEnteredEvent == null) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Commander c = collision.GetComponent<Commander>();
            if (c != null) this.CommanderEnteredEvent(this, c);
        }
    }

}
