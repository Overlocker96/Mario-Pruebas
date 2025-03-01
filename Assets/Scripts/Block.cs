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
    public bool powerUp;
    [SerializeField]
    private GameObject Mushroom;
    [SerializeField]
    public bool lifemushroom;
    [SerializeField]
    private GameObject LifeMushroom;
    [SerializeField]
    private GameObject Flower;
    [SerializeField]
    public bool bigMario;
    [SerializeField]
    public bool flowerMario;
    [SerializeField]
    public bool dead;
    [SerializeField]
    private GameObject Brick;

    private void Update()
    {
        bigMario = GameObject.Find("Mario").GetComponent<Player>().bigPowerUp;
        flowerMario = GameObject.Find("Mario").GetComponent<Player>().flowerPowerUp;
        dead = GameObject.Find("Mario").GetComponent<Player>().dead;
    }

    public void Hit()
    {
        base.Hit();

        if (!dead)
        {
            if (Coin != null && coins > 0 && !powerUp)
            {
                GameManager.Instance.AddPoints();
                var coinSpawn = Instantiate(Coin, this.transform.position, Quaternion.identity);
                GameManager.Instance.Coin();
            }

            if (Mushroom != null && bigMario == false && powerUp && coins == 0)
            {
                var mushSpawn = Instantiate(Mushroom, this.transform.position, Quaternion.identity);
                powerUp = false;
            }
            
            if (Flower != null && bigMario == true && powerUp && coins == 0)
            {
                var flowerSpawn = Instantiate(Flower, this.transform.position, Quaternion.identity);
                powerUp = false;
            }

            if (LifeMushroom != null && lifemushroom && coins == 0)
            {
                var mushSpawn = Instantiate(LifeMushroom, this.transform.position, Quaternion.identity);
                lifemushroom = false;
            }

            if (blockType == 1 && (bigMario == true || flowerMario == true) && !powerUp)
            {
                var brickSpawn = Instantiate(Brick, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
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
