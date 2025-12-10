using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChristmasDudeAI : MonoBehaviour
{
    NavMeshAgent enemy;

    [SerializeField]
    GameObject player;

    public AudioSource walk;

    public float madvalue = 3;

    public AudioClip jumpscare;

    public Sprite spritee;

    public Sprite spriteInvert;

    public SpriteRenderer spriteItself;

    private bool timerActive, cooldownTimerActive;

    private float timer, cooldownTimer;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        cooldownTimerActive = true;
        cooldownTimer = madvalue;
        timer = 0.5f;
        enemy.enabled = false;
    }

    public void Angrier()
    {
        madvalue -= 0.5f;//you can input any value here
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.enabled == true)
        {
            enemy.SetDestination(player.transform.position);
        }

        if (cooldownTimerActive)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (cooldownTimer < 0)
        {
            cooldownTimer = madvalue;
            cooldownTimerActive = false;
            timerActive = true;
            enemy.enabled = true;
            spriteItself.sprite = spriteInvert;
            walk.Play();
        }

        if (timerActive)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            timer = 0.5f;
            timerActive = false;
            cooldownTimerActive = true;
            enemy.enabled = false;
            spriteItself.sprite = spritee;
        }
    }

    public void Jumpscared()
    {
        enemy.enabled = false;
        walk.PlayOneShot(jumpscare);
    }
}
