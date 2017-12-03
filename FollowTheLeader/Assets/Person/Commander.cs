using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour {

    //VARIABLES
	private PersonController myController;
	private ScoreManager scoreManager;
    private int _playerNumber;

	private int fingerForMove = -1;
	private Vector3 fingerForMoveStart = Vector3.zero;
    
    //METHODS

    public int PlayerNumber
    {
        get { return _playerNumber; }
        set { _playerNumber = value; }
    }

	private void Awake() {
		myController = GetComponent<PersonController> ();
		scoreManager = GameObject.FindObjectOfType<ScoreManager> ();
	}

    private void Update()
    {
        Inputs();

		MobileInputs ();

		scoreManager.UpdateScore(_playerNumber, myController.GetFollowerGroupCount ());
    }

    private void Inputs()
    {

        if (Input.GetAxis("Horizontal_p"+ PlayerNumber) != 0 || Input.GetAxis("Vertical_p" + PlayerNumber) != 0)
        {
         Vector2 direction = new Vector2(Input.GetAxis("Horizontal_p"+PlayerNumber), Input.GetAxis("Vertical_p" + PlayerNumber));
			if (direction.magnitude > 1f) {
				direction.Normalize();
			}
         gameObject.SendMessage("Move", direction);    
        } 

        if(Input.GetButtonDown("Jump_p"+PlayerNumber)) gameObject.SendMessage("Jump");

        //if(Input.GetButtonDown("Attack")) gameObject.SendMessage("Attack");
	}

	private void MobileInputs() {
		// Screen size
		Vector2 size = new Vector2(Screen.width, Screen.height);

		for (int i = 0; i < Input.touchCount; ++i)
		{
			Touch t = Input.GetTouch(i);
			if (fingerForMove == t.fingerId) {
				if (t.phase == TouchPhase.Ended) {
					// Released the finger!
					fingerForMove = -1;
					return;
				}
				if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary) {
					// Moving the finger!

					Vector2 direction = (t.position - size / 2)/(Screen.height);
					if (direction.magnitude > 1) {
						direction.Normalize ();
					}
					SendMessage ("Move", direction);
				}
			}

			if (fingerForMove == -1) {
				// We are not touching to move yet

				// Check if the finger just started in the range!
				if (t.phase == TouchPhase.Began) {
					Vector2 pos = t.position;
					if ((pos - size / 2).magnitude / (Screen.height) < 1f) {
						// Touch is in center part of screen (circle with radius 50% of height of screen)
						fingerForMove = t.fingerId;
					} else {
						gameObject.SendMessage("Jump");
					}
				}
			}

			// Jump!
			if (t.phase == TouchPhase.Began) {
				Vector2 pos = t.position;
				if ((pos - size / 2).magnitude / (Screen.height) > 1f) {
					gameObject.SendMessage("Jump");
				}
			}
		}
	}
    

}
