using System;
using System.Collections;
using UnityEngine;

    public class PlayerImpulse : MonoBehaviour
    {
        public bool isDamage;
        
        [SerializeField] private float impulseDistance;
        [SerializeField] private float impulseDamageTime;

        private CharacterController _controller;

        private void Awake() => 
            _controller = GetComponent<CharacterController>();

        private void Update() => 
            CheckLkmDown();

        private void CheckLkmDown()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                MoveImpulse();
                DamageTime();
            }
        }

        private void MoveImpulse()
        {
            _controller.Move(transform.forward * impulseDistance);
        }

        private IEnumerator DamageTime()
        {
            isDamage = true;
            yield return new WaitForSeconds(impulseDamageTime);
            isDamage = false;
            yield break;
        }
    }