using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour {

    //VARIABLES
	private PersonController myController;
	private ScoreManager scoreManager;

    
    //METHODS

	private void Awake() {
		myController = GetComponent<PersonController> ();
		scoreManager = GameObject.FindObjectOfType<ScoreManager> ();
	}

    private void Update()
    {
        Inputs();

		scoreManager.UpdateScore(myController.GetFollowerGroupCount ());
    }

    private void Inputs()
    {

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
         Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			if (direction.magnitude > 1f) {
				direction.Normalize();
			}
         gameObject.SendMessage("Move", direction);    
        } 

        if(Input.GetButtonDown("Jump")) gameObject.SendMessage("Jump");

        //if(Input.GetButtonDown("Attack")) gameObject.SendMessage("Attack");
    }
    

}
