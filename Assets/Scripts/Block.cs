using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : BlockBase
{
    //Propiedades de los bloques y variables
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
    private GameObject DestroyedBrick;

    //Recogemos las propiedades de los bloques
    private void Update()
    {
        bigMario = GameObject.Find("Mario").GetComponent<Player>().bigPowerUp;
        flowerMario = GameObject.Find("Mario").GetComponent<Player>().flowerPowerUp;
        dead = GameObject.Find("Mario").GetComponent<Player>().dead;
    }

    //Método Hit cuando golpeams un bloque
    public void Hit()
    {
        if (dead == false)//Si mario NO está muerto
        {
            base.Hit();//Llamamos al método Hit de BlockBase de donde heredamos

            if (Coin != null && coins > 0 && !powerUp)//Existe el objecto Moneda, las monedas son mayor que 0 y no tenemos ningún powerUp en el Bloque
            {
                GameManager.Instance.AddPoints();//Añadimos puntos
                var coinSpawn = Instantiate(Coin, this.transform.position, Quaternion.identity);//Instanciamos una Moneda en la posición del Bloque
                GameManager.Instance.Coin();//Llamamos al Método Coin del GameManager
            }

            if (Mushroom != null && bigMario == false && powerUp && coins == 0)//Existe el objeto Seta Normal, Mario NO es grande, tenemos powerUp pero ninguna Moneda en el Bloque
            {
                var mushSpawn = Instantiate(Mushroom, this.transform.position, Quaternion.identity);//Instanciamos una Seta Normal en la posición del Bloque
                powerUp = false;//Ya no tenemos powerUp
            }
            
            if (Flower != null && bigMario == true && powerUp && coins == 0)//Existe el objeto Flor, Mario ES grande, tenemos powerUp pero ninguna Moneda en el Bloque
            {
                var flowerSpawn = Instantiate(Flower, this.transform.position, Quaternion.identity);//Instanciamos una Flor en la posición del Bloque
                powerUp = false;//Ya no tenemos powerUp
            }

            if (LifeMushroom != null && lifemushroom && coins == 0)//Existe el objeto Seta de Vida, tenemos Seta de Vida pero ninguna Moneda en el Bloque
            {
                var mushSpawn = Instantiate(LifeMushroom, this.transform.position, Quaternion.identity);//Instanciamos una Seta de Vida en la posición del Bloque
                lifemushroom = false;//Ya no tenemos Seta de Vida
            }

            if (blockType == 1 && (bigMario == true || flowerMario == true) && !powerUp)//Si el Tipo de Bloque es Ladrillo y somos Mario Grande o Mario Flor y el Bloque no tiene powerUp
            {
                var brickSpawn = Instantiate(DestroyedBrick, this.transform.position, Quaternion.identity); ;//Instanciamos un Ladrillo Destruido en la posición del Bloque
                Destroy(this.gameObject);//Destruimos el Bloque Original
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
