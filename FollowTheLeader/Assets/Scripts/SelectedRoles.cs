using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedRoles : MonoBehaviour {


    //VARIABLES
    [SerializeField] private GameObject[] _players;
    private int[] _characterNumbers { get; set; }

    private bool _playingGame { get; set; }
    private int _joinedCount { get; set; }
    private int _readyCount { get; set; }

    //METHODS
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _characterNumbers = new int[_players.Length];
    }

    private void Update()
    {
        Debug.Log(JoinedCount);
        Debug.Log(ReadyCount);

        if (!PlayingGame && _readyCount == _joinedCount && _readyCount >= 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            PlayingGame = true;
        }

    }

    public int JoinedCount
    {
        get { return _joinedCount; }
        set { _joinedCount = value; }
    }

    public int ReadyCount
    {
        get { return _readyCount; }
        set { _readyCount = value; }
    }

    public bool PlayingGame
    {
        get { return _playingGame; }
        set { _playingGame = value; }
    }

    public int[] CharacterNumbers
    {
        get { return _characterNumbers; }
        set { _characterNumbers = value; }
    }

    public void LockCharacters(int number, int playerNumber)
    {
            CharacterNumbers[playerNumber] = number;
    }

    public void UnlockCharacters(int number, int playernumber)
    {
        CharacterNumbers[playernumber] = number;
    }



}
