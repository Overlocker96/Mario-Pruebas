using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : BlockBase
{
    [SerializeField]
    public int coins;
    [SerializeField]
    private GameObject Coin;
    [SerializeField]
    public bool mushroom;
    [SerializeField]
    private GameObject Mushroom;
    [SerializeField]
    public bool bigMario;
    [SerializeField]
    private GameObject Brick;

    private void Update()
    {
        bigMario = GameObject.Find("Mario").GetComponent<Player>().bigPowerUp;
    }

    public void Hit()
    {
        base.Hit();

        if (Coin != null && coins > 0 && !mushroom)
        {
            var coinSpawn = Instantiate(Coin, this.transform.position, Quaternion.identity);
        }

        if (Mushroom != null && mushroom && coins == 0)
        {
            var mushSpawn = Instantiate(Mushroom, this.transform.position, Quaternion.identity);
            mushroom = false;
        }

        if (blockType == 1 && bigMario == true)
        {
            var brickSpawn = Instantiate(Brick, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
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
