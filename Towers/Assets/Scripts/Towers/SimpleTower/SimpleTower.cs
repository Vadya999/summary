using System;
using UnityEngine;
using System.Collections;

public class SimpleTower : MonoBehaviour
{
	[SerializeField] private float shootInterval;
	[SerializeField] private GameObject projectilePrefab;
	
	private float intrvalLast;

	private void Start()
	{
		intrvalLast = shootInterval;
	}

	private void Update()
	{
		intrvalLast -= Time.deltaTime;
		if (shootInterval < 0)
		{
			Shoot();

			intrvalLast = shootInterval;
		}
	}

	private void Shoot()
	{
		Instantiate(projectilePrefab, transform.position, Quaternion.identity, this.transform);
	}
} 
