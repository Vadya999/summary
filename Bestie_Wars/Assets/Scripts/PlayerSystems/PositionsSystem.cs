using Kuhpik;
using UnityEngine;

public class PositionsSystem : GameSystem
{
    [SerializeField] private Transform spawnMiniCharacterPosition;
    [SerializeField] private Transform carMovePositionFirst;
    [SerializeField] private Transform carMovePositionSecond;
    [SerializeField] private Transform carMovePositionThird;

    public Transform CarMovePositionFirst => carMovePositionFirst;

    public Transform CarMovePositionSecond => carMovePositionSecond;

    public Transform CarMovePositionThird => carMovePositionThird;

    public Transform SpawnMiniCharacterPosition => spawnMiniCharacterPosition;
}