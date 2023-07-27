using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Create CharacterConfiguration", fileName = "CharacterConfiguration", order = 0)]
public class CharacterConfiguration : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;

    public float Speed => speed;

    public float RotateSpeed => rotateSpeed;
}