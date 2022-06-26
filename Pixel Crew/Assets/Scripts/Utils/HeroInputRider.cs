using System.Collections;
using System.Collections.Generic;
using PixelCrew.Creatures;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew
{
    public class HeroInputRider : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
    
    
        public void OnHorizontalMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }
    
        public void OnSaySomething(InputAction.CallbackContext context)
        {
           
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.Interact();
            }
        }
        
        public void OnDooAttack(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.Attack();
            }
        }

        public void OnTrow(InputAction.CallbackContext context)
        {
            if (context.performed)//touch
            {
                _hero.Trow();
            }
        }
    }
}

