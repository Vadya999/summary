using UnityEngine;

public class SkateComponent : MonoBehaviour, IStackableObject
{
    public SkateData data { get; private set; }
    public int level { get; private set; }
    public SkateSkinComponent skateSkin { get; private set; }

    public float height => data.height;
    public Outline outline => skateSkin.outline;

    public void SetSkin(SkateData data, int level)
    {
        this.data = data;
        this.level = level;
        SetSkin(data.skateVariants[level]);
    }

    public void SetLevel(int level)
    {
        this.level = level;
        SetSkin(data.skateVariants[level]);
    }

    private void SetSkin(SkateSkinComponent newSkin)
    {
        if (skateSkin != null) Destroy(skateSkin.gameObject);
        skateSkin = Instantiate(newSkin, transform);
        skateSkin.transform.localPosition = Vector3.zero;
        skateSkin.transform.localRotation = Quaternion.identity;
    }
}