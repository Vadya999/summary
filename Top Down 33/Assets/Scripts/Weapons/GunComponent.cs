using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunComponent : MonoBehaviour
{
    [SerializeField] private float _numBullsSec;

    [SerializeField] private GameObject _bullet;

    [SerializeField] private Transform _spawnPos;

    public void ShootPistolAndGun()
    {
        StartCoroutine("ShootPistolAndGunCor");
    }
    
    public void ShootShotgun()
    {
        StartCoroutine("ShootShotgunCor");
    }
    
    public void ShootGrenadeGun()
    {
        StartCoroutine("ShootGrenadeCor");
    }

    private IEnumerator ShootShotgunCor()
    {
        for (int i = 0; i < _numBullsSec; i++)
        {
            Instantiate(_bullet, _spawnPos.position, Quaternion.identity);
        }
        
        yield return new WaitForSeconds(1.5f);
        yield break;
    }
    
    private IEnumerator ShootGrenadeCor()
    {
        for (int i = 0; i < _numBullsSec; i++)
        {
            Instantiate(_bullet, _spawnPos.position, Quaternion.identity);
        }
        
        yield return new WaitForSeconds(0.66f);
        yield break;
    }
    
    private IEnumerator ShootPistolAndGunCor()
    {
        for (int i = 0; i < _numBullsSec; i++)
        {
            Instantiate(_bullet, _spawnPos.position, Quaternion.identity);
        }
        
        yield return new WaitForSeconds(1f);
        yield break;
    }
}
