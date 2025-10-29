using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BowShooting : MonoBehaviour
{
    [SerializeField]
    private float gameDuration = 60f;

    [SerializeField]
    private Board board;
    [SerializeField]
    private Bow bow;
    [SerializeField]
    private int arrowCount;

    [SerializeField]
    private Bow bowPrefab;

    [SerializeField]
    private int roundScore = 0;


    private Vector3 bowStartPos = Vector3.zero;
    private Quaternion bowStartRot = Quaternion.identity;

    bool isPlaying = false;
    float timer = 0f;


    private void Awake() => board.Initialize(this);
    private void Start()
    {
        if (bow != null)
        {
            bow.Initialize(this, 0);
            bowStartPos = bow.transform.position;
            bowStartRot = bow.transform.rotation;
        }
    }

    private void Update()
    {
        if (!isPlaying) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
            EndGame();

    }

    public void StartGame()
    {
        roundScore = 0;
        timer = gameDuration;
        isPlaying = true;


        Destroy(bow.gameObject);
        bow = Instantiate(bowPrefab, bowStartPos, bowStartRot);
        bow.Initialize(this, arrowCount);
    }

    public void EndGame()
    {
        isPlaying = false;

        Destroy(bow.gameObject);
        bow = Instantiate(bowPrefab, bowStartPos, bowStartRot);

        TicketManager.Instance.AddTicket(roundScore);
    }


    public void AddScore(int count) => roundScore += count;
}