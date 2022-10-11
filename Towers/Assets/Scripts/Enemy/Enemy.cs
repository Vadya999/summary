using System;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float hp;
	[SerializeField] private float lifeTime;
	
	[SerializeField] private GameObject targetPosition;
	[SerializeField] private string targetGameObjectTag;
	[SerializeField] private Rigidbody enemyRigidbody;
    
	public float speed;
	private void Start()
	{
		Destroy(gameObject, lifeTime);
		targetPosition = GameObject.FindWithTag(targetGameObjectTag);
	}

	private void FixedUpdate()
	{
		MoveToTarget();
	}

	private void MoveToTarget()
	{
		Vector3 targetVector = targetPosition.transform.position - transform.position;
		
		enemyRigidbody.velocity = targetVector.normalized * speed;
	}
	
	public void TakeDamage(float damage)
	{
		hp -= damage;

		if (hp <= 0)
		{
			Destroy(gameObject);
		}
	}
}
