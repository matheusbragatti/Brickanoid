using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class BallScript : MonoBehaviour
{
    public float initialSpeed;
    public bool initialPosition;
    public Transform paddlePosition;
    public Rigidbody2D thisRigidBody;
    public int lives;
    public Vector2 ballCorrectionY = new Vector2(0f, 0.1f);
    public Vector2 ballCorrectionX = new Vector2(0.1f, 0f);
    public GameObject explosionVFX;
    public GameObject explosionVFXGameObject;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D ballRigidBody = GetComponent<Rigidbody2D>();

        initialPosition = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (initialPosition)
        {
            thisRigidBody.velocity = Vector2.zero;
            this.transform.position = new Vector2(paddlePosition.transform.position.x, paddlePosition.position.y + 0.5f);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BottomWall"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().updateLives(-1);
            FindObjectOfType<AudioManager>().playSound("playerDeath");
            explosionVFXGameObject = Instantiate(explosionVFX, this.transform.position, Quaternion.identity);
            Destroy(explosionVFXGameObject, 3);
            initialPosition = true;
            paddlePosition.localScale = new Vector3(1.5f,2f,1f);
            Debug.Log("Hit bottom");
            

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {

            if(thisRigidBody.velocity.x > 0)
            {
                thisRigidBody.AddRelativeForce(ballCorrectionX,ForceMode2D.Impulse);
            }
            else
            {
                thisRigidBody.AddRelativeForce(-ballCorrectionX, ForceMode2D.Impulse);
            }

            if(thisRigidBody.velocity.y > 0)
            {
                thisRigidBody.AddRelativeForce(ballCorrectionY, ForceMode2D.Impulse);
            }
            else
            {
                thisRigidBody.AddRelativeForce(-ballCorrectionY, ForceMode2D.Impulse);
            }

            if(thisRigidBody.velocity.y == 0 || thisRigidBody.velocity.x == 0)
            {

                thisRigidBody.AddRelativeForce(new Vector2(Random.Range(-0.5f,0.5f), -0.5f), ForceMode2D.Impulse);
            }
        }
    }
}
