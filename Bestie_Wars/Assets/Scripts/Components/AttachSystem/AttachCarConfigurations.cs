using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Create AttachCarConfigurations", fileName = "AttachCarConfigurations", order = 0)]
public class AttachCarConfigurations : ScriptableObject
{
    [SerializeField] private int sellPrice;
    [SerializeField] private int destroyPrice;
    [SerializeField] private int destroySecondPreccPrice;
    [SerializeField] private float waitingSellTime;
    [SerializeField] private float speedLerp;
    [SerializeField] private float speedRotate;
    [SerializeField] private Vector3 destroyScale;
    [SerializeField] private float startScaleTime;

    [SerializeField] private Vector3 destroySecondScale;
    [SerializeField] private float startScaleSecondTime;

    public Vector3 DestroySecondScale => destroySecondScale;

    public int DestroySecondPreccPrice => destroySecondPreccPrice;

    public float StartScaleSecondTime => startScaleSecondTime;

    public Vector3 DestroyScale => destroyScale;

    public float StartScaleTime => startScaleTime;

    public float WaitingSellTime => waitingSellTime;

    public int SellPrice => sellPrice;

    public int DestroyPrice => destroyPrice;

    public float SpeedLerp => speedLerp;

    public float SpeedRotate => speedRotate;
}