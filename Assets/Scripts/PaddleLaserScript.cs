using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleLaserScript : MonoBehaviour
{
    public GameObject laserBullet;
    private GameObject paddle;
    public float timer = 20f;
    private float laserPositionY;
    private PolygonCollider2D paddleCollider;


    // Start is called before the first frame update
    void Start()
    {
        paddle = GameObject.FindGameObjectWithTag("Paddle");
        paddleCollider = paddle.GetComponent<PolygonCollider2D>();
        laserPositionY = paddle.transform.position.y + 0.3f;


    }

    // Update is called once per frame
    void Update()
    {
        while(this.transform.position.x != paddle.transform.position.x)
        {
            this.transform.position = new Vector3 (paddle.transform.position.x, laserPositionY , this.transform.position.z);
        }


        while(this.transform.localScale != paddle.transform.localScale)
        {
            this.transform.localScale = paddle.transform.localScale;
        }
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            Destroy(this.gameObject);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (Time.timeScale != 0)
            {
                Instantiate(laserBullet, new Vector3(paddleCollider.bounds.center.x + (paddleCollider.bounds.extents.x * 0.75f), laserPositionY + 0.2f, this.transform.position.z), Quaternion.identity);
                Instantiate(laserBullet, new Vector3(paddleCollider.bounds.center.x - (paddleCollider.bounds.extents.x * 0.75f), laserPositionY + 0.2f, this.transform.position.z), Quaternion.identity);
                FindObjectOfType<AudioManager>().findAudioClip("laser").Play();
            }
       
        }

    }

    public void refreshTimer()
    {
        this.timer = 20f;
    }
}
