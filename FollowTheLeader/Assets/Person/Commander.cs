using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour {

    //VARIABLES
    [SerializeField] private int _playerNumber;
    
    //METHODS

    public int PlayerNumber
    {
        get { return _playerNumber; }
        set { _playerNumber = value; }
    }

    private void Update()
    {
        Inputs();
    }

    private void Inputs()
    {

        if (Input.GetAxis("Horizontal_p"+PlayerNumber) != 0 || Input.GetAxis("Vertical_p" +PlayerNumber) != 0)
        {
         Vector2 direction = new Vector2(Input.GetAxis("Horizontal_p" + PlayerNumber), Input.GetAxis("Vertical_p" + PlayerNumber));
         gameObject.SendMessage("Move", direction);    
        } 

        if(Input.GetButtonDown("Jump_p" + PlayerNumber)) gameObject.SendMessage("Jump");

        //if(Input.GetButtonDown("Attack")) gameObject.SendMessage("Attack");
    }
    

}
