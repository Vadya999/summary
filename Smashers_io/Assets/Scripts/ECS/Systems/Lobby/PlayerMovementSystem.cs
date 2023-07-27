using Kuhpik;
using UnityEngine;

public class PlayerMovementSystem : GameSystem
{
    private PlayerMovementComponent playerMovement => game.player.movement;

    private PlayerInput _input = new PlayerInput();

    private PlayerMovementConfig _config;

    public bool isMoving
    {
        get
        {
            _input.Update();
            return _input.movementDelta.magnitude > 0;
        }
    }

    public override void OnUpdate()
    {
        _input.Update();
        Rotate();
        Move();
        UpdateAnimations();
    }

    private void Move()
    {
        var delta = _input.movementDelta * _config.speed;
        if (!PlayerInput.isEnabled) delta = Vector3.zero;
        playerMovement.rb.velocity = (delta);
    }

    private void Rotate()
    {
        if (_input.movementDelta != Vector3.zero)
        {
            var targetDirection = Quaternion.LookRotation(_input.movementDelta, Vector3.up);
            var direction = Quaternion.RotateTowards(playerMovement.transform.rotation, targetDirection, _config.rotationSpeed * Time.deltaTime);
            playerMovement.transform.rotation = direction;
        }
    }

    private void UpdateAnimations()
    {
        game.player.aniamtion.isWalking = isMoving;
        game.player.aniamtion.isHoldingItem = game.player.activeItem != null;
    }
}
