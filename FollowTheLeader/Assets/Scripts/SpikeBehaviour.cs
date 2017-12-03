using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour {

	//VARIABLES
    [SerializeField] private Animator _anim;

    [SerializeField] private BoxCollider2D _killZone;

    [SerializeField] private float _minCooldown;
    [SerializeField] private float _maxCooldown;

    private float _cooldown { get; set; }
    private bool _spikeTrapTrigger { get; set; }

    private float _stabTime;
    //METHODS


    public float Cooldown
    {
        get { return _cooldown; }
        set { _cooldown = value; }
    }

    public bool SpikeTrapTrigger
    {
        get { return _spikeTrapTrigger; }
        set { _spikeTrapTrigger = value; }
    }

    private void Awake()
    {
       _stabTime = Random.Range(_minCooldown, _maxCooldown);
        _killZone.enabled = false;
    }


    private void Update()
    {

        Cooldown += Time.deltaTime;

        if (Cooldown >= _stabTime || SpikeTrapTrigger)
        {
            _anim.SetTrigger("SpikeTrap");
            _stabTime = Random.Range(_minCooldown, _maxCooldown);
            Cooldown = 0;
            SpikeTrapTrigger = false;
        }
    }


    public void Kill()
    {
        _killZone.enabled = true;
    }

    public void Safe()
    {
        _killZone.enabled = false;
    }



}
