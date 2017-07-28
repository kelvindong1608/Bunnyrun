using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bunnycontroller : MonoBehaviour {
	private Rigidbody2D myRigidBody;
	private Animator myAnim;
	public float bunnyJumpForce =500f;
	private float bunnyHurtTime = -1;
	private Collider2D myCollider;
	public Text scroreText;
	private float startTime;
	private int jumpLeft =2;
	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		myCollider = GetComponent<Collider2D> ();
		startTime= Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (bunnyHurtTime == -1) {
			if (Input.GetButtonUp ("Jump") && jumpLeft>0) 
			{	
				if (myRigidBody.velocity.y < 0) 
				{
					myRigidBody.velocity = Vector2.zero;
				}
				if (jumpLeft == 1) {
					myRigidBody.AddForce (transform.up * bunnyJumpForce * 0.75f);
				} 
				else
				{
					myRigidBody.AddForce (transform.up * bunnyJumpForce);
				}

				jumpLeft--;
			}
			myAnim.SetFloat ("vVelocity", myRigidBody.velocity.y);
			scroreText.text=(Time.time - startTime).ToString("0.0");
		} 
		else
		{
			if (Time.time > bunnyHurtTime + 2)
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene ("gamescreen");
			}
		}
	}
	void OnCollisionEnter2D(Collision2D collision)
	{	
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {	
			foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>()) {
				spawner.enabled = false;
			}

			foreach (Moveleft moveLefter in FindObjectsOfType<Moveleft>()) {
				moveLefter.enabled = false;
			}
				
			bunnyHurtTime = Time.time;
			myAnim.SetBool ("bunnyHurt", true);
			myRigidBody.velocity = Vector2.zero;
			myRigidBody.AddForce (transform.up * bunnyJumpForce);
			myCollider.enabled = false;
		}
		else if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Ground"))
		{
			jumpLeft = 2;
		}
	}
}
