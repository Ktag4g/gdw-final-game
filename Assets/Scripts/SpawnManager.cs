using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Target variables
    public GameObject target;

    //Stealth Zone variables
    public GameObject[] stealthZones;
    public int numStealthZones;
    public int stealthTimeDuration;
    public int stealthPreTimeDuration;
    public int stealthTimeRate;

    private GameManager gameManager;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        spawnTarget();
        StartCoroutine(startStealthTime());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnTarget()
    {
        Vector3 spawnSpot = new Vector3(Random.Range(-9, 9), Random.Range(-3, 6), 0);

        Instantiate(target, spawnSpot, transform.rotation);
    }

    //Warns of the start of stealth time, enabling the stealth zones
    IEnumerator startStealthTime()
    {
        Debug.Log("Stealth Time is about to start!");
        for (int i = 0; i < numStealthZones; i++)
        {
            stealthZones[Random.Range(0, stealthZones.Length)].SetActive(true);
        }
        yield return new WaitForSeconds(stealthPreTimeDuration);
        StartCoroutine(stealthTime());
    }

    //Starts stealth time
    IEnumerator stealthTime()
    {
        Debug.Log("Stealth Time Start!");
        gameManager.isStealthTime = true;
        yield return new WaitForSeconds(stealthTimeDuration);
        StartCoroutine(stealthTimeEnd());
    }
    
    //Ends the stealth time, disabling the stealth zones
    IEnumerator stealthTimeEnd()
    {
        Debug.Log("Stealth Time End!");
        gameManager.isStealthTime = false;
        player.isHiding = false;
        for (int i = 0; i < stealthZones.Length; i++)
        {
            stealthZones[i].SetActive(false);
        }
        yield return new WaitForSeconds(stealthTimeRate);
        StartCoroutine(startStealthTime());
    }


}
