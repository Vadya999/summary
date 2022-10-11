using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemyController : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private float snoozeInterval;

	private float snoozeIntervalPassed;

	private void Start()
	{
		snoozeIntervalPassed = snoozeInterval;
	}

	private void Update()
	{
		snoozeIntervalPassed -= Time.deltaTime;

		if (snoozeIntervalPassed < 0)
		{
			SpawnEnemy();
			snoozeIntervalPassed = snoozeInterval;
		}
	}

	private void SpawnEnemy()
	{
		var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity, this.transform);
	}
}
