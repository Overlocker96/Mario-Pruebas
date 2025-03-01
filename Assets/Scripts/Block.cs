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
    public bool lifemushroom;
    [SerializeField]
    private GameObject LifeMushroom;
    [SerializeField]
    public bool bigMario;
    [SerializeField]
    public bool dead;
    [SerializeField]
    private GameObject Brick;

    private void Update()
    {
        bigMario = GameObject.Find("Mario").GetComponent<Player>().bigPowerUp;
        dead = GameObject.Find("Mario").GetComponent<Player>().dead;
    }

    public void Hit()
    {
        base.Hit();

        if (Coin != null && coins > 0 && !mushroom)
        {
            GameManager.Instance.AddPoints();
            var coinSpawn = Instantiate(Coin, this.transform.position, Quaternion.identity);
            GameManager.Instance.Coin();
        }

        if (Mushroom != null && mushroom && coins == 0)
        {
            var mushSpawn = Instantiate(Mushroom, this.transform.position, Quaternion.identity);
            mushroom = false;
        }

        if (Mushroom != null && lifemushroom && coins == 0)
        {
            var mushSpawn = Instantiate(LifeMushroom, this.transform.position, Quaternion.identity);
            lifemushroom = false;
        }

        if (blockType == 1 && bigMario == true && dead == false)
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
