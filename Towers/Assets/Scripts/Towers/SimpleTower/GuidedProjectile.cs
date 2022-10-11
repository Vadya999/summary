using System;
using UnityEngine;
using System.Collections;

public class GuidedProjectile : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float lifeTimeIfYouLose;
	[SerializeField] private float damage;
	[SerializeField] private string enemytag;

	private void Start()
	{
		Destroy(gameObject, lifeTimeIfYouLose);
	}

	private void Update()
	{
		transform.position = Vector3.forward * (speed * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(enemytag))
		{
			other.gameObject.TryGetComponent(out Enemy enemy);
			enemy.TakeDamage(damage);
		}
	}
}
