using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CannonTower : MonoBehaviour 
{
	[SerializeField] private float bulletSpeed;
	[SerializeField] private GameObject projectilePrefab;

	private float shootIntervalPassed;
	private float speedEnemy;
	private List<Transform> enemiesTransformsList;
	private void Start()
	{
		enemiesTransformsList = new List<Transform>();
	}
	private void Update()
	{
		if (enemiesTransformsList.Count > 0 && enemiesTransformsList.First() != null)
		{
			var offset = GetAngleOffset(enemiesTransformsList.First(), bulletSpeed, speedEnemy);
			
			RotateTower(enemiesTransformsList.First(),offset);
			
			Hit();
		}
	}
	private void Hit()
	{
		var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, this.transform);
		enemiesTransformsList.Remove(enemiesTransformsList.First());
	}
	private float GetAngleOffset(Transform target, float speedBullet, float targetSpeed)
	{
		var rCrossV = target.position.x * target.position.y  - target.position.y * target.position.x * targetSpeed;
		var magR = Mathf.Sqrt(target.position.x * target.position.x + target.position.y * target.position.y);
		var angleOffset = Mathf.Asin(rCrossV / (speedBullet * magR));

		return angleOffset + Mathf.Atan2(target.position.y, target.position.x);
	}

	private void RotateTower(Transform target, float angleOffset)
	{
		Vector3 fromTo = target.position - transform.position;
		Vector3 fromToCos = fromTo;
		fromTo = new Vector3(fromTo.x + angleOffset,fromTo.y,fromTo.z);
		
		transform.rotation = Quaternion.LookRotation(fromTo);
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			other.gameObject.TryGetComponent(out Enemy enemy);
			speedEnemy = enemy.speed;
			enemiesTransformsList.Add(enemy.transform);
		}
	}
}
