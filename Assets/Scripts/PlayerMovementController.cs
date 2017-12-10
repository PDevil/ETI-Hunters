using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
	public float maxSpeed = 4.0F;
	public float jumpForce = 12.0F;
	public LayerMask groundLayer;

	//private Animator m_anim;
	private Rigidbody2D m_rigidbody;
	private bool m_updateX = true;

	void Start ()
	{
		//m_anim = GetComponent<Animator>();
		m_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{

	}

	void OnCollisionStay2D(Collision2D collision)
	{
		//Debug.Log(collision);
		if (isGrounded() == false)
		{
			m_rigidbody.velocity.Set(0, m_rigidbody.velocity.y);
			m_updateX = false;
		}
		else
			m_updateX = true;
	}

	void FixedUpdate()
	{
		float horizontalMove = Input.GetAxis("Horizontal");
		float verticalMove = Input.GetAxis("Vertical");

		if (verticalMove > 0.0F && isGrounded() == true)
		{
			m_rigidbody.AddForce(jumpForce * Vector2.up);
		}

		if (!m_updateX)
			horizontalMove = 0.0F;

		m_rigidbody.velocity = new Vector2(horizontalMove * maxSpeed, m_rigidbody.velocity.y);
	}

	bool isGrounded()
	{
		return Physics2D.Raycast(this.transform.position, Vector2.down, 0.7F * this.transform.localScale.y, groundLayer).collider != null;
	}
}
