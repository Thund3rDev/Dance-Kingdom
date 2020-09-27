using System.Collections;
using UnityEngine;

public class TroopManager : MonoBehaviour
{
    public bool allyOrEnemy; //true: ally || false: enemy
    public int type;
    public int street;
    private bool dead;

    //On Start, flip sprite if enemy.
    void Start()
    {
        if (!allyOrEnemy)
            GetComponent<SpriteRenderer>().flipX = true;
    }

    //On update, move the troop and check if has arrived to the other castle.
    void Update()
    {
        if (allyOrEnemy)
        {
            if(!dead)
                transform.position += new Vector3(0.6f * Time.deltaTime, 0f, 0f);
            else
                transform.position += new Vector3(0.0f * Time.deltaTime, 0f, 0f);
            if (street ==  0 && transform.position.x >= 1f)
            {
                soundDependingOfType();
                AudioManager.instance.ManageAudio("Rocks", "sound", "play");
                StartCoroutine("DieAnimation");
                dead = true;
                transform.position += new Vector3(-1.0f * Time.deltaTime, 0f, 0f);
                GameManager.instance.enemyHP -= GameManager.instance.damageOnHit;
                if(GameManager.instance.enemyHP < 0)
                    GameManager.instance.enemyHP = 0;
                GameManager.instance.setBarSize((GameManager.instance.enemyHP) / 100.0f, "enemyBar");
            }
            else if (street == 1 && transform.position.x >= 1.7f)
            {
                soundDependingOfType();
                AudioManager.instance.ManageAudio("Rocks", "sound", "play");
                StartCoroutine("DieAnimation");
                dead = true;
                transform.position += new Vector3(-1.0f * Time.deltaTime, 0f, 0f);
                GameManager.instance.enemyHP -= GameManager.instance.damageOnHit;
                if (GameManager.instance.enemyHP < 0)
                    GameManager.instance.enemyHP = 0;
                GameManager.instance.setBarSize((GameManager.instance.enemyHP) / 100.0f, "enemyBar");
            }
            else if (street == 2 && transform.position.x >= 2.5f)
            {
                soundDependingOfType();
                AudioManager.instance.ManageAudio("Rocks", "sound", "play");
                StartCoroutine("DieAnimation");
                dead = true;
                transform.position += new Vector3(-1.0f * Time.deltaTime, 0f, 0f);
                GameManager.instance.enemyHP -= GameManager.instance.damageOnHit;
                if (GameManager.instance.enemyHP < 0)
                    GameManager.instance.enemyHP = 0;
                GameManager.instance.setBarSize((GameManager.instance.enemyHP) / 100.0f, "enemyBar");
            }
        }
        else
        {
            if (!dead)
                transform.position += new Vector3(-0.6f * Time.deltaTime, 0f, 0f);
            else
                transform.position += new Vector3(0.0f * Time.deltaTime, 0f, 0f);
            if (street == 0 && transform.position.x <= -5.5f)
            {
                soundDependingOfType();
                AudioManager.instance.ManageAudio("Rocks", "sound", "play");
                StartCoroutine("DieAnimation");
                dead = true;
                transform.position += new Vector3(1.0f * Time.deltaTime, 0f, 0f);
                GameManager.instance.allyHP -= GameManager.instance.damageOnHit;
                if (GameManager.instance.allyHP < 0)
                    GameManager.instance.allyHP = 0;
                GameManager.instance.setBarSize((GameManager.instance.allyHP) / 100.0f, "allyBar");
            }
            else if (street == 1 && transform.position.x <= -6.1f)
            {
                soundDependingOfType();
                AudioManager.instance.ManageAudio("Rocks", "sound", "play");
                StartCoroutine("DieAnimation");
                dead = true;
                transform.position += new Vector3(1.0f * Time.deltaTime, 0f, 0f);
                GameManager.instance.allyHP -= GameManager.instance.damageOnHit;
                if (GameManager.instance.allyHP < 0)
                    GameManager.instance.allyHP = 0;
                GameManager.instance.setBarSize((GameManager.instance.allyHP) / 100.0f, "allyBar");
            }
            else if (street == 2 && transform.position.x <= -6.8f)
            {
                soundDependingOfType();
                AudioManager.instance.ManageAudio("Rocks", "sound", "play");
                StartCoroutine("DieAnimation");
                dead = true;
                transform.position += new Vector3(1.0f * Time.deltaTime, 0f, 0f);
                GameManager.instance.allyHP -= GameManager.instance.damageOnHit;
                if (GameManager.instance.allyHP < 0)
                    GameManager.instance.allyHP = 0;
                GameManager.instance.setBarSize((GameManager.instance.allyHP) / 100.0f, "allyBar");
            }
        }
    }

    //Checks the collision betweetn troops.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject.transform.tag == "AllySquire" && other.transform.tag == "EnemyKnight")
        {
            AudioManager.instance.ManageAudio("KnightHit", "sound", "play");
            StartCoroutine("DieAnimation");
            dead = true;
        }
        else if (gameObject.transform.tag == "AllyArcher" && other.transform.tag == "EnemySquire")
        {
            AudioManager.instance.ManageAudio("SquireHit", "sound", "play");
            StartCoroutine("DieAnimation");
            dead = true;
        }
        else if (gameObject.transform.tag == "AllyKnight" && other.transform.tag == "EnemyArcher")
        {
            AudioManager.instance.ManageAudio("ArcherHit", "sound", "play");
            StartCoroutine("DieAnimation");
            dead = true;
        }
        else if (gameObject.transform.tag == "EnemySquire" && other.transform.tag == "AllyKnight")
        {
            AudioManager.instance.ManageAudio("KnightHit", "sound", "play");
            StartCoroutine("DieAnimation");
            dead = true;
        }
        else if (gameObject.transform.tag == "EnemyArcher" && other.transform.tag == "AllySquire")
        {
            AudioManager.instance.ManageAudio("SquireHit", "sound", "play");
            StartCoroutine("DieAnimation");
            dead = true;
        }
        else if (gameObject.transform.tag == "EnemyKnight" && other.transform.tag == "AllyArcher")
        {
            AudioManager.instance.ManageAudio("ArcherHit", "sound", "play");
            StartCoroutine("DieAnimation");
            dead = true;
        }
        else if (gameObject.transform.tag == "EnemySquire" && other.transform.tag == "AllySquire")
        {
            AudioManager.instance.ManageAudio("SquireHit", "sound", "play");
            StartCoroutine("DieAnimation");
            dead = true;
        }
        else if (gameObject.transform.tag == "EnemyArcher" && other.transform.tag == "AllyArcher")
        {
            AudioManager.instance.ManageAudio("ArcherHit", "sound", "play");
            StartCoroutine("DieAnimation");
            dead = true; 
        }
        else if (gameObject.transform.tag == "EnemyKnight" && other.transform.tag == "AllyKnight")
        {
            AudioManager.instance.ManageAudio("KnightHit", "sound", "play");
            StartCoroutine("DieAnimation");
            dead = true;
        }
        else if (gameObject.transform.tag == "AllySquire" && other.transform.tag == "EnemySquire")
        {
            AudioManager.instance.ManageAudio("SquireHit", "sound", "play");
            StartCoroutine("DieAnimation");
            dead = true;
        }
        else if (gameObject.transform.tag == "AllyArcher" && other.transform.tag == "EnemyArcher")
        {
            AudioManager.instance.ManageAudio("ArcherHit", "sound", "play");
            StartCoroutine("DieAnimation");
            dead = true;
        }
        else if (gameObject.transform.tag == "AllyKnight" && other.transform.tag == "EnemyKnight")
        {
            AudioManager.instance.ManageAudio("KnightHit", "sound", "play");
            StartCoroutine("DieAnimation");
            dead = true;
        }
    }

    //Animation of troop losing its alpha.
    private IEnumerator DieAnimation()
    {
        GetComponent<BoxCollider2D>().enabled = false;

        for (int i = 0; i < 10; i++)
        {
            float intervalTime = Time.realtimeSinceStartup + 0.05f;
            while (Time.realtimeSinceStartup < intervalTime)
            {
                yield return 0;
            }
            GetComponent<SpriteRenderer>().color -= new Color(0f, 0f, 0f, 0.1f);
        }
        Destroy(gameObject);
    }

    //Plays sound depending on troopType.
    private void soundDependingOfType()
    {
        if (type == 0)
        {
            AudioManager.instance.ManageAudio("SquireHit", "sound", "play");
        }
        else if (type == 1)
        {
            AudioManager.instance.ManageAudio("ArcherHit", "sound", "play");
        }
        else if (type == 2)
        {
            AudioManager.instance.ManageAudio("KnightHit", "sound", "play");
        }
    }
}
