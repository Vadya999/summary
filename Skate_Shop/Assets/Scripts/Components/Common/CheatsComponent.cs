using Kuhpik;
using UnityEngine;

public class CheatsComponent : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            GameData.walletModel.moneyCount += 20;
        }
    }
}