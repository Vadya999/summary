using System;
using UnityEngine;
using System.Collections;

public class CannonProjectile : MonoBehaviour 
{
	[SerializeField] private float projectileDamage = 10;
	[SerializeField] private string enemyTag;
	[SerializeField] private float lifeTimeIfNotHit;
	[SerializeField] private float speed;
	[SerializeField] private Rigidbody projectileRigidbody;

	private void Start()
	{
		Destroy(gameObject, lifeTimeIfNotHit);
	}

	private void FixedUpdate()
	{
		MoveProjectile();
	}

	private void MoveProjectile()
	{
		projectileRigidbody.velocity = Vector3.forward * speed;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(enemyTag))
		{
			other.gameObject.TryGetComponent(out Enemy enemyCs);
			enemyCs.TakeDamage(projectileDamage);
		}
	}
}
