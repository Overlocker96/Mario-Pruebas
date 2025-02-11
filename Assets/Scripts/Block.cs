using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : BlockBase
{
    [SerializeField]
    public int coins;
    [SerializeField]
    private GameObject Coin;

    public void Hit()
    {
        base.Hit();

        if (Coin != null && coins > 0)
        {
            var coinSpawn = Instantiate(Coin, this.transform.position, Quaternion.identity);
        }

        //Voy restando coins hasta 0
        if (coins > 0)
        {
            coins--;
        }

        //Bloqueo el bloque
        if (coins == 0)
        {
            this.GetComponent<Animator>().enabled = false;
            this.SetSprite();
        }
    }
}
