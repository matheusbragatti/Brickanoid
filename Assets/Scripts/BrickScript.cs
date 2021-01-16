using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{

    public GameObject powerUp;
    public int health;
    public int points;
    public GameManager gameManager;
    public AudioSource audioSource;
    public BoxCollider2D boxCollider;
    public Sprite damagedBlock;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        boxCollider = this.gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Ball"))
        {
            health--;
            if (health <= 0)
            {
                gameManager.updateScore(points);
                gameManager.updateNumberOfBricks(-1);
                FindObjectOfType<AudioManager>().playSound("blockDeath");
                if (powerUp.name != "none")
                {
                    Instantiate(powerUp, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                }
                boxCollider.enabled = false;
                gameObject.AddComponent<BrickFade>();
                Debug.Log(powerUp.name);
            }
            else
            {
                FindObjectOfType<AudioManager>().playSound("hit");
                this.gameObject.GetComponent<SpriteRenderer>().sprite = damagedBlock;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
            health--;
            if (health <= 0)
            {
                gameManager.updateScore(points);
                gameManager.updateNumberOfBricks(-1);
                FindObjectOfType<AudioManager>().playSound("blockDeath");
                if (powerUp.name != "none") { 
                Instantiate(powerUp, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                    }
                boxCollider.enabled = false;
                Debug.Log(powerUp.name);
                gameObject.AddComponent<BrickFade>();
            }
            else
            {
                FindObjectOfType<AudioManager>().playSound("hit");
                this.gameObject.GetComponent<SpriteRenderer>().sprite = damagedBlock;
            }
        }
    }
}
