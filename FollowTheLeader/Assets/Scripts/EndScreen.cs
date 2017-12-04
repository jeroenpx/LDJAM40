using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {


    private void Update()
    {
        if (Input.GetButtonDown("Start_p0"))
        {
            Destroy(GameObject.Find("RoleLocks"));
            SceneManager.LoadScene(0);
        }
    }
}
