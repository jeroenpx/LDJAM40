using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour {

	//VARIABLES
    [SerializeField] private Transform[] _Stops;

    [SerializeField] private float _safeZone;

    [SerializeField] private float _speed;

    private int _currentStop;

    //METHODS

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }



    private void Update()
    {
     
        if (Vector2.Distance(_Stops[_currentStop].position, transform.position) <= _safeZone) _currentStop++;

        if (_currentStop >= _Stops.Length) _currentStop = 0;

        Vector2 direction = _Stops[_currentStop].position - transform.position;

        transform.position += Vector3.Normalize(new Vector3(direction.x, direction.y, 0)) *_speed * Time.deltaTime;
        

        //Debug.Log("Dist" + Vector2.Distance(_Stops[_currentStop].position, transform.position));
        //Debug.Log("Stop" +_currentStop);


        

    }
}
