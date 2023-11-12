using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Footsteps : MonoBehaviour
{
    public AudioClip playerMediumSteps;
    public AudioClip playerFastSteps;

    public AudioClip monsterSlowSteps;
    public AudioClip monsterMediumSteps;
    public AudioClip monsterFastSteps;

    private Vector3 lastPosition;

    public FirstPersonController firstPersonScript;

    public AudioSource stepAudioSource;

    UnityEngine.AI.NavMeshAgent agent;
    void Start()
    {
        lastPosition = transform.position;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (stepAudioSource == null)
        {
            Debug.LogError("AudioSource needs to be attached to Footsteps script.");
        }
        stepAudioSource.loop = true;
    }
    private void Update()
    {
        if (gameObject.CompareTag("Player"))
        {
            float playerSpeed = firstPersonScript.MoveSpeed;
            Debug.Log("Player speed: " + playerSpeed);
            if (Input.GetKey(KeyCode.LeftShift) && playerSpeed > 1 && transform.position != lastPosition)
            {
                PlayFootSteps(playerFastSteps);
            }
            else if (playerSpeed > 1 && transform.position != lastPosition)
            {
                PlayFootSteps(playerMediumSteps);
            }
            else
            {
                stepAudioSource.Stop();
            }
            lastPosition = transform.position;
        }
        if (gameObject.CompareTag("Monster"))
        {
            if (agent.speed > 0 && agent.speed < 1 && transform.position != lastPosition)
            {
                PlayFootSteps(monsterSlowSteps);
            }
            else if (agent.speed >= 1 && agent.speed < 3 && transform.position != lastPosition)
            {
                PlayFootSteps(monsterMediumSteps);
            }
            else if (agent.speed >= 3 && transform.position != lastPosition)
            {
                PlayFootSteps(monsterFastSteps);
            }
            else
            {
                stepAudioSource.Stop();
            }
            lastPosition = transform.position;
        }
    }
    private void PlayFootSteps(AudioClip footsteps)
    {
        if (!stepAudioSource.isPlaying || stepAudioSource.clip != footsteps)
        {
            stepAudioSource.clip = footsteps;
            stepAudioSource.Play();
            Debug.Log("Playing: " + stepAudioSource.clip);
        }
    }
}
