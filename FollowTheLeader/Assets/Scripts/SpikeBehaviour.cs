using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour {

	//VARIABLES
    [SerializeField] private Animator _anim;

    [SerializeField] private float _minCooldown;
    [SerializeField] private float _maxCooldown;

    private float _cooldown { get; set; }

    private float _stabTime;
    //METHODS


    public float Cooldown
    {
        get { return _cooldown; }
        set { _cooldown = value; }
    }

    private void Awake()
    {
       _stabTime = Random.Range(_minCooldown, _maxCooldown);
    }


    private void Update()
    {

        Cooldown += Time.deltaTime;

        if (Cooldown >= _stabTime)
        {
            _anim.SetTrigger("SpikeTrap");
            Cooldown = 0;
            _stabTime = Random.Range(_minCooldown, _maxCooldown);
        }
    }



}
