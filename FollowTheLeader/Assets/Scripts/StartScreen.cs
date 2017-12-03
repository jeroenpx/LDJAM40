using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

    //VARIABLES
    [SerializeField] private GameObject[] _selected;

    [SerializeField] private float _scrollDelay;

    [SerializeField] private GameObject[] _screens;
    [SerializeField] private GameObject _startPrompt;
    [SerializeField] private float _blinkDelay;

    private float _scrollCounter;
    private float _blinkCounter;

    private int _currentSelected;

    private bool _startPressed;

    private bool _startBlink;
    //METHODS

    private void Start()
    {
        _startPressed = false;
        foreach (var gameMode in _selected) gameMode.SetActive(false);
        _selected[_currentSelected].SetActive(true);
        _screens[1].SetActive(false);
        _startBlink = true;
    }

    private void Update()
	{
		if (_startPressed) {
			Scroll ();
			StartGame ();
		}

        if(!_startPressed) FrontPage();
    }

    private void StartGame()
    {
		bool isTouching = false;
		Touch[] ts = Input.touches;
		foreach (Touch t in ts) {
			if (t.phase == TouchPhase.Began) {
				isTouching = true;
				if (t.position.x < Screen.width / 2) {
					// Left
					_currentSelected = 0;
				} else {
					_currentSelected = 1;
				}
			}
		}

		if (Input.GetButtonDown("Jump_p0") || isTouching)
        {
            switch (_currentSelected)
            {
                case 0:
                    SceneManager.LoadScene(3);
                    break;

                case 1:
                    SceneManager.LoadScene(1);
                    break;

            }
        }

    }

    private void Scroll()
    {
        if (Input.GetAxis("Horizontal_p0") != 0)
        {

            foreach (var gameMode in _selected) gameMode.SetActive(false);
            Debug.Log(_currentSelected);
            if (Input.GetAxis("Horizontal_p0") < -0.5)
            {
                _scrollCounter += Time.deltaTime;
                if (_scrollCounter >= _scrollDelay)
                {
                    ++_currentSelected;
                    if (_currentSelected > _selected.Length - 1) _currentSelected = 0;
                    _scrollCounter = 0;
                }
            }

            if (Input.GetAxis("Horizontal_p0") > 0.5)
            {

                _scrollCounter += Time.deltaTime;
                if (_scrollCounter >= _scrollDelay)
                {
                    --_currentSelected;
                    if (_currentSelected < 0) _currentSelected = _selected.Length - 1;
                    _scrollCounter = 0;
                }
            }

        }
        _selected[_currentSelected].SetActive(true);
    
    }

    private void FrontPage()
    {
        _blinkCounter += Time.deltaTime;
        if (_blinkCounter >= _blinkDelay)
        {
            _startBlink = !_startBlink;
            _startPrompt.SetActive(_startBlink);
            _blinkCounter = 0;
        }

		bool isTouching = false;
		Touch[] ts = Input.touches;
		foreach (Touch t in ts) {
			if (t.phase == TouchPhase.Began) {
				isTouching = true;
			}
		}

		if (Input.GetButtonDown("Start_p0") || isTouching)
        {
            _screens[0].SetActive(false);
            _screens[1].SetActive(true);
            _startPressed = true;
        }
    }
}
