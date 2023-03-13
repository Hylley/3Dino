using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public static playerMove instance;

    [Header("Calibration")]
    public float jumpForce;
    public float jumpDelayTime;
    [HideInInspector]public bool canJump = true;

    [Header("Controls")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Audio")]
    public AudioSource stepsAudio;
    public AudioSource fallAudio;
    public AudioSource jumpAudio;
    public AudioSource gameOverSound;

    Rigidbody rb;
    Animator anim;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        stepsAudio.Play();
    }

    void Update()
    {
        if(Input.GetKey(jumpKey) & canJump)
        {
            Jump();
        }
    }

    void Jump()
    {
        anim.SetBool("jumping", true);
        //anim.enabled = false;
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        canJump = false;

        stepsAudio.loop = false;
        jumpAudio.Play();

        StartCoroutine(jumpTimer());
    }

    IEnumerator jumpTimer()
    {
        yield return new WaitForSeconds(jumpDelayTime);
        canJump = true;
        anim.SetBool("jumping", false);

        stepsAudio.loop = true;
        stepsAudio.Play();
    }

    void OnCollisionEnter(Collision hit)
    {
        transform.position = new Vector3(0, transform.position.y, 0);

        if(!canJump) playerLook.shakeDuration = .2f;

        if(!hit.gameObject.CompareTag("Ground")) return;
        //canJump = true;
        //anim.enabled = true;
        anim.SetBool("jumping", false);
        fallAudio.Play();
    }

    public void Die()
    {
        gameOverSound.Play();
        Time.timeScale = 0;

        UIManager.instance.gameOverScreen.SetActive(true);
        UIManager.instance.gameOver = true;
        UIManager.instance.SaveScore();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        stepsAudio.Stop();
    }
}
