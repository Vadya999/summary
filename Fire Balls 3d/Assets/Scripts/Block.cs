using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    public event UnityAction<Block> BulletHit;//<> то что передает событие block this в данном случае
    [SerializeField] private ParticleSystem _destroyEffect;

    public void SetColor(Color color)
    {
        
    }
    public void Break()
    {
        BulletHit?.Invoke(this);//событие совершилось
        ParticleSystemRenderer renderer =  Instantiate(_destroyEffect, transform.position, Quaternion.identity).GetComponent<ParticleSystemRenderer>();
        renderer.material.color = GetComponent<MeshRenderer>().material.color;
        Destroy(gameObject);
    }
}
