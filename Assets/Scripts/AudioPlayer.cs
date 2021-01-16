using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    private static AudioPlayer _instance;
    private static AudioPlayer Instance { get { return _instance; } }
    public AudioManager audioManager;
    public int currentLevelMusic;




    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += changeSong;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.playSound("level" + SceneManager.GetActiveScene().buildIndex + "_music");
        currentLevelMusic = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(currentLevelMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeSong(Scene current, Scene next)
    {
        Debug.Log(next.buildIndex);
        audioManager.findAudioClip("level" + (currentLevelMusic) + "_music").Stop();
        audioManager.playSound("level" + SceneManager.GetActiveScene().buildIndex + "_music");
        currentLevelMusic = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Loaded scenes:" + SceneManager.sceneCount);
    }


}
