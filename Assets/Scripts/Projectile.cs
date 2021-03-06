
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	//Keep this constant
	// Just change the mass of the enemy
	public float knockbackForce = 500;
	public float damage;
	void Awake()
	{
		AudioSource audioData = GetComponent<AudioSource>();
		audioData.Play(0);

	}

	void OnCollisionEnter2D(Collision2D collider)
	{
		Object.Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Enemy")
		{
			Object.Destroy(this.gameObject);
			GameObject target = collider.gameObject;
			Health health = collider.gameObject.GetComponent<Health>();
			if (health == null) {
				target = collider.gameObject.transform.parent.gameObject;
				health = target.GetComponent<Health>();
			}
			health.TakeDamage(damage);


			Vector2 thisPosition = transform.position;
			Vector2 enemyPosition = target.transform.position;

			Vector2 dir = (enemyPosition - thisPosition) .normalized;

			Rigidbody2D rigidbody = target.GetComponent<Rigidbody2D>();
			rigidbody.AddForce(dir * knockbackForce, ForceMode2D.Impulse);
		}
	}
}
