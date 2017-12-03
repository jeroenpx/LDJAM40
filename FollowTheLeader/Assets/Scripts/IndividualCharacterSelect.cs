using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndividualCharacterSelect : MonoBehaviour
{

    //VARIABLES
    [SerializeField] private int _playerNumber;
    [SerializeField] private float _scrollDelay;

    [SerializeField] private GameObject _joinLayout;
    [SerializeField] private GameObject _selectLayout;
    [SerializeField] private GameObject _readyLayout;
    [SerializeField] private Image _Selection;
    [SerializeField] private Sprite[] _roles;
    [SerializeField] private SelectedRoles _roleLock;

    private bool _joined;
    private bool _ready;

    private int _disabledNumber = -1;
    private int _currentRole;

    private float _scrollCounter;

    
    //METHODS
    public int PlayerNumber
    {
        get { return _playerNumber; }
        set { _playerNumber = value; }
    }

    public int CurrentRole
    {
        get { return _currentRole; }
        set { _currentRole = value; }
    }


    private void Start()
    {
        _selectLayout.SetActive(false);
        _readyLayout.SetActive(false);
        _Selection.gameObject.SetActive(false);
        _joined = false;
        _ready = false;
        CurrentRole = 0;
        _Selection.sprite = _roles[CurrentRole];
        _roleLock.UnlockCharacters(_disabledNumber, PlayerNumber);
    }

    private void Update()
    {
     if (!_joined) Join();
     if(_joined && !_ready) Select();
     if(_ready) Ready();
    }

    private void Join()
    {

        if (Input.GetButtonDown("Jump_p" + PlayerNumber))
        {
            _roleLock.JoinedCount++;
            _joinLayout.SetActive(false);
            _selectLayout.SetActive(true);
            _Selection.gameObject.SetActive(true);
            _joined = true;
        }

    }

    private void Select()
    {
        if (Input.GetButtonDown("Back_p" + PlayerNumber))
        {
            _roleLock.JoinedCount--;
            _Selection.gameObject.SetActive(false);
            _selectLayout.SetActive(false);
            _joinLayout.SetActive(true);
            _joined = false;
        }

        Scroll();

        if (Input.GetButtonDown("Start_p" + PlayerNumber))
        {
            _roleLock.LockCharacters(CurrentRole, PlayerNumber);
            _roleLock.ReadyCount++;
            _Selection.gameObject.SetActive(false);
            _selectLayout.SetActive(false);
            _readyLayout.SetActive(true);
            _ready = true;
        }

        
    }

    private void Scroll()
    {
            
        if (Input.GetAxis("Vertical_p" + PlayerNumber) < 0)
        {
            _scrollCounter += Time.deltaTime;
            if (_scrollCounter >= _scrollDelay)
            {
                ++CurrentRole;
                if (CurrentRole > _roles.Length - 1) CurrentRole = 0;
                _scrollCounter = 0;
            }
        }

        if (Input.GetAxis("Vertical_p" + PlayerNumber) > 0)
        {
            _scrollCounter += Time.deltaTime;
            if (_scrollCounter >= _scrollDelay)
            {
                --CurrentRole;
                if (CurrentRole < 0) CurrentRole = _roles.Length - 1;
                _scrollCounter = 0;
            } 
        }
        _Selection.sprite = _roles[CurrentRole];
    }

    private void Ready()
    {
        if (Input.GetButtonDown("Back_p" + PlayerNumber))
        {
            _roleLock.UnlockCharacters(_disabledNumber, PlayerNumber);
            _roleLock.ReadyCount--;
            _Selection.gameObject.SetActive(true);
            _selectLayout.SetActive(true);
            _readyLayout.SetActive(false);
           _ready = false;
        }
    }
}
