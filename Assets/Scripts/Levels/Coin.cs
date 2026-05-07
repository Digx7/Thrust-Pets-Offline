using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Digx7.Zygote;

public class Coin : MonoBehaviour
{
    public IntChannel request_IncreaseCoins_Channel;

    void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);

        PlayerCharacter player = other.transform.root.GetComponent<PlayerCharacter>();

        if (player != null)
        {
            request_IncreaseCoins_Channel.Raise(1);
        }
    }
}
