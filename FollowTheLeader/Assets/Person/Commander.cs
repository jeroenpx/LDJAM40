using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour {

    //VARIABLES

    
    //METHODS

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
         Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
         gameObject.SendMessage("Move", direction);
        } 

        if(Input.GetButtonDown("Jump")) gameObject.SendMessage("Jump");
    }
    

}
