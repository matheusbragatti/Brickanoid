using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class PaddleMovementControl : MonoBehaviour
{
    public GameObject paddleLaser;
    public int paddleSpeed;
    public BoxCollider2D rightScreen;
    public BoxCollider2D leftScreen;
    public GameObject ball;
    private BallScript ballScript;
    private Rigidbody2D ballRigidBody;
    private float rightScreenBounds;
    private float leftScreenBounds;
    private GameManager gameManager;
    PolygonCollider2D paddleCollider;
    private Vector3 currentScale;
    private PaddleLaserScript paddleLaserInstance;
    public GameObject shield;


    void Start()
    {
        rightScreenBounds = rightScreen.bounds.extents.x;
        leftScreenBounds = leftScreen.bounds.extents.x;
        ballScript = ball.GetComponent<BallScript>();
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        paddleCollider = this.GetComponent<PolygonCollider2D>();
    }


    // Update is called once per frame
    void Update()
    {
       
       
        //Player Movement and bounds.
        float horizontalMovement = Input.GetAxis("Horizontal");
        

        transform.Translate(Vector2.right * horizontalMovement * Time.deltaTime * paddleSpeed);
        if (this.transform.position.x > 8.8f - paddleCollider.bounds.extents.x)
        {
            transform.position = new Vector2(8.8f - paddleCollider.bounds.extents.x, transform.position.y);
        }
        if (this.transform.position.x < -8.8f + paddleCollider.bounds.extents.x)
        {
            transform.position = new Vector2(-8.8f + paddleCollider.bounds.extents.x, transform.position.y);
        }

        if (Input.GetButtonDown("Jump") && ballScript.initialPosition == true )
        {

            ballScript.initialPosition = false;
            ballRigidBody.AddForce(Vector2.up * ballScript.initialSpeed);
        }


    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Regex.IsMatch(other.name, "^one\\s?up.*$", RegexOptions.IgnoreCase))
        {
            FindObjectOfType<AudioManager>().playSound("one_up");
            gameManager.updateLives(1);
            Destroy(other.gameObject);
        }
        if (Regex.IsMatch(other.name, "^one\\s?down.*$", RegexOptions.IgnoreCase))
        {
            gameManager.updateLives(-1);
            FindObjectOfType<AudioManager>().playSound("one_down");
            Destroy(other.gameObject);
        }
        if (Regex.IsMatch(other.name, "^scale\\s?up.*$", RegexOptions.IgnoreCase))
        {
            FindObjectOfType<AudioManager>().playSound("scale_up");
            if (this.gameObject.transform.localScale.x >= 2.5f)
            {

            }
            else
            {
                gameManager.coroutineQueue.Enqueue(ScaleUP());
                
            }
            Destroy(other.gameObject);
        }
        if(Regex.IsMatch(other.name, "^scale\\s?down.*$", RegexOptions.IgnoreCase))
        {
            FindObjectOfType<AudioManager>().playSound("scale_down");
            if (this.gameObject.transform.localScale.x <= 0.5f)
            {

            }
            else
            {
                gameManager.coroutineQueue.Enqueue(ScaleDOWN());
            }
            Destroy(other.gameObject);
        }

        if (Regex.IsMatch(other.name, "Paddle\\s?Laser.*$", RegexOptions.IgnoreCase))
        {
            FindObjectOfType<AudioManager>().playSound("laser_pickup");
            if (GameObject.FindWithTag("PaddleLaser"))
            {
                GameObject.FindWithTag("PaddleLaser").GetComponent<PaddleLaserScript>().refreshTimer();
                
            }
            else
            {
                var laserPaddle = Instantiate(paddleLaser, new Vector3(this.transform.position.x, this.transform.position.y + 0.3f, this.transform.position.z), Quaternion.identity);
            }
            Destroy(other.gameObject);
        }

        if (Regex.IsMatch(other.name, "Shield.*$", RegexOptions.IgnoreCase))
        {
            FindObjectOfType<AudioManager>().playSound("shield");
            if (GameObject.FindWithTag("Shield"))
            {
            }
            else
            {
                Instantiate(shield, new Vector3(-0.86f, -4.68f, this.transform.position.z), Quaternion.identity);
            }
            Destroy(other.gameObject);

        }
    }

    IEnumerator ScaleUP()
    {
        currentScale = this.gameObject.transform.localScale;
        
        for (int i = 0; i < 100; i++)
        {
            transform.localScale = Vector3.Lerp(this.gameObject.transform.localScale, new Vector3(this.gameObject.transform.localScale.x + 0.5f, currentScale.y, currentScale.z),  Time.deltaTime);

            if (this.gameObject.transform.localScale.x >  currentScale.x + 0.5f)
            {
                this.gameObject.transform.localScale = new Vector3(currentScale.x + 0.5f, currentScale.y, currentScale.z);
                break;
            }

            yield return null;

        }
        if (this.gameObject.transform.localScale.x != currentScale.x + 0.5f)
        {
            this.gameObject.transform.localScale = new Vector3(currentScale.x + 0.5f, currentScale.y, currentScale.z);
        }

        yield return null;
    }

    IEnumerator ScaleDOWN()
    {
        currentScale = this.gameObject.transform.localScale;

        for (int i = 0; i < 100; i++)
        {
            transform.localScale = Vector3.Lerp(this.gameObject.transform.localScale, new Vector3(this.gameObject.transform.localScale.x - 0.5f, currentScale.y, currentScale.z), Time.deltaTime);

            if (this.gameObject.transform.localScale.x < currentScale.x - 0.5f)
            {
                this.gameObject.transform.localScale = new Vector3(currentScale.x - 0.5f, currentScale.y, currentScale.z);
                break;
            }

            yield return null;

        }
        if (this.gameObject.transform.localScale.x != currentScale.x - 0.5f)
        {
            this.gameObject.transform.localScale = new Vector3(currentScale.x - 0.5f, currentScale.y, currentScale.z);
        }


        yield return null;
    }


}
