using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour {

	// Settings
	[SerializeField]
	public float speed = 1f;

	[SerializeField]
	public float dashSpeed = 10f;

	[SerializeField]
	public float dashTime = 1f;

	[SerializeField]
	private float inairtime = 1f;

	[SerializeField]
	private float jumpheight = 1f;

	[SerializeField]
	private float instantJumpSpeedPercent = 0.7f;

    [SerializeField] private Animator _anim;

    [SerializeField] private int _playerNumber;

    // What to do next frame?
    private Vector2 moveDir;
	private bool jump;
	private float jumpStartTime=-100f;
	private Vector3 lastPosition = Vector3.zero;
	public Vector3 runningAvgSpeed = Vector3.zero;

	// Private components
	private Rigidbody2D rigidBody;
	private Transform child;
	private Vector3 childDelta;
	private bool jumping;

	private float startDashTime= -1000;

	private Vector3 debugJumpStartPos;

	// People that are following me should update this!!!
	// KEEP THIS UP TO DATE
	public List<Follower> areFollowingMe;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D> ();
		child = transform.Find ("Graphics");
		childDelta = child.localPosition;
		lastPosition = transform.position;
		areFollowingMe = new List<Follower> ();
	}

	public int GetFollowerGroupCount() {
		int count = 0;
		foreach(Follower follower in areFollowingMe) {
			count += follower.myController.GetFollowerGroupCount ();
		}
		return 1 + count;
	}


    public int PlayerNumber
    {
        get { return _playerNumber; }
        set { _playerNumber = value; }
    }

	/**
	 * MESSAGE: Move in a certain direction
	 */
	public void Move(Vector2 dir) {
		moveDir = dir;
		if (moveDir.x > 0.05f) child.localScale = new Vector3(-1,1,1);
		if(moveDir.x < 0.05f) child.localScale = new Vector3(1, 1, 1);
        _anim.SetFloat("Speed", 1);
    }

	/**
	 * MESSAGE: Jump!
	 */
	public void Jump() {
		jump = true;
	}

	public bool IsDashing() {
		return Time.time - startDashTime < dashTime;
	}

	public void Dash() {
		if (!IsDashing ()) {
			if (moveDir.magnitude > .5f) {
				startDashTime = Time.time;
			}
		}
	}

	/**
	 * PUBLIC: Are we jumping?
	 */
	public bool IsJumping() {
		return jumping;
	}

	/**
	 * MESSAGE: Hit something that kills me on the ground
	 */
	public void GotHitGround() {
		if (!IsJumping ()) {
			KillMyself ();
		}
	}

	/**
	 * MESSAGE: Hit something that kills me
	 */
	public void GotHit() {
		KillMyself ();
	}

	/**
	 * All actions needed to kill myself...
	 */
	public void KillMyself() {
		foreach (Follower follower in areFollowingMe) {
			follower.ClosestGuyDied ();
		}
		SendMessage ("FinalizeDeath");
		Destroy (gameObject);
	}

	/**
	 * Physics
	 */
	void FixedUpdate() {

        //set idle animation when not walking
	    _anim.SetFloat("Speed", 0);

		// Update runningAvgSpeed
		if (!jumping && !IsDashing()) {
			Vector3 lastSpeed = (transform.position - lastPosition) / Time.fixedDeltaTime;
			runningAvgSpeed = lastSpeed * instantJumpSpeedPercent + runningAvgSpeed * (1 - instantJumpSpeedPercent);
			if (runningAvgSpeed.magnitude > speed) {
				runningAvgSpeed = runningAvgSpeed.normalized * speed;
			}
		}
		lastPosition = transform.position;

        // Currently just set the velocity
		if (!jumping && !IsDashing()) {
			// Do not allow changing direction mid flight
			rigidBody.velocity = moveDir * speed;
		} else {
			if (jumping) {
				rigidBody.velocity = runningAvgSpeed;
			} else {
				rigidBody.velocity = runningAvgSpeed.normalized * dashSpeed;
			}
		}

		if (jump) {
			if (jumping && jumpStartTime < Time.time - inairtime) {
				// end of jump
				jump = false;
				jumping = false;
				Debug.DrawLine(debugJumpStartPos, transform.position, Color.blue, 20f);
			} else {
				if (!jumping) {
					// start jumping
					jumping = true;
					jumpStartTime = Time.time;
					debugJumpStartPos = transform.position;
				}
			}
			if (jumping) {
				// we are in the air!
				float t = (Time.time-jumpStartTime)/inairtime;
				float tsquared = t*t;
				child.localPosition = childDelta + new Vector3(0f, -4*jumpheight*tsquared+4*jumpheight*t);
			}
		}

		// Reset move dir
		moveDir = new Vector2 ();
	}


}
