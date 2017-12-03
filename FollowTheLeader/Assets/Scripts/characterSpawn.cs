using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterSpawn : MonoBehaviour {

    //VARIABLES
    [SerializeField] private GameObject[] _characters;
    [SerializeField] private Transform[] _spawns;
    private int[] _selectionNumbers;


    //METHODS
    private void Awake()
    {
		SelectedRoles roles = FindObjectOfType<SelectedRoles> ();

		if (roles == null) {
			_selectionNumbers = new int[] { 0 };
		} else {
			_selectionNumbers = FindObjectOfType<SelectedRoles>().CharacterNumbers;
		}

        for (int i = 0; i < _selectionNumbers.Length; i++)
        {
            if (_selectionNumbers[i] >= 0)
            {
                GameObject player = Instantiate(_characters[_selectionNumbers[i]], new Vector2(_spawns[i].position.x, _spawns[i].position.y), Quaternion.identity);
                player.GetComponent<PersonController>().PlayerNumber = i;
                player.GetComponent<Commander>().PlayerNumber = i;
            }

        }
    }
}
