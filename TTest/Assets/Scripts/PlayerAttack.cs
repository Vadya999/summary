using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerImpulse _playerImpulse;
    [SerializeField] private float invulnerabilityTime;

    private TextMeshProUGUI _scoreText;
    private Material _material;
    private const string PlayerTag = "Player";
    private float score;

    private void Start()
    {
        _scoreText = FindObjectOfType<TextMeshProUGUI>();
        score = 0;
        _material = GetComponent<Material>();
        _scoreText.text = score.ToString();
    }

    public IEnumerator TakeDamage()
    {
        _material.color = Color.red;
        yield return new WaitForSeconds(invulnerabilityTime);
        _material.color = Color.white;
        yield break;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_playerImpulse.isDamage)
        {
            if (other.gameObject.CompareTag(PlayerTag))
            {
                PlayerAttack playerAttack = other.gameObject.GetComponentInChildren<PlayerAttack>();
                playerAttack.TakeDamage();
                UpdateScore();
            }
        }
    }
    private void UpdateScore()
    {
        score++;
        _scoreText.text = score.ToString();
        if (score == 3)
        {
            YouWin();
        }
    }

    private void YouWin()
    {
        var win = FindObjectOfType<Win>();
        win.Victory();
    }
}