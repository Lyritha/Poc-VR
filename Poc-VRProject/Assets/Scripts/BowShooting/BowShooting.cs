using MyBox;
using Oculus.Interaction.Editor;
using System.Collections;
using TMPro;
using UnityEngine;

public class BowShooting : MonoBehaviour
{
    [Foldout("Values", true), SerializeField]
    private float gameDuration = 60f;
    [SerializeField]
    private float startDelay = 5f;
    [SerializeField]
    private int arrowCount = 10;

    [Foldout("Scene References", true) ,SerializeField]
    private Board board;
    [SerializeField]
    private Bow bow;
    [SerializeField]
    private RectTransform uiStartPanel;
    [SerializeField]
    private TMP_Text uiStartPanelTimer;
    [SerializeField]
    private TMP_Text uiTimer;
    [SerializeField]
    private TMP_Text uiScore;
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private RectTransform uiStartButton;

    [Foldout("Prefab References", true), SerializeField]
    private Bow bowPrefab;

    private int roundScore = 0;

    private Vector3 bowStartPos = Vector3.zero;
    private Quaternion bowStartRot = Quaternion.identity;

    private bool isPlaying = false;
    private float timer = 0f;


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

        uiTimer.text = $"Time left: {Mathf.CeilToInt(timer)}";

        if (timer <= 0f)
            EndGame();

    }

    public void StartGame()
    {
        roundScore = 0;
        timer = gameDuration;

        startButton.SetActive(false);
        uiStartButton.gameObject.SetActive(false);

        uiScore.text = $"Score: {roundScore}";
        Destroy(bow.gameObject);
        bow = Instantiate(bowPrefab, bowStartPos, bowStartRot);

        StartCoroutine(StartDelay());

        bow.Initialize(this, 0);
    }

    IEnumerator StartDelay()
    {
        float delay = startDelay;
        while (delay > 0f)
        {
            uiStartPanelTimer.text = $"Get ready:\n{Mathf.CeilToInt(delay)}";
            delay -= Time.deltaTime;
            yield return null;
        }

        isPlaying = true;
        bow.Initialize(this, arrowCount);
        uiStartPanel.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        if (!isPlaying) return;

        isPlaying = false;

        Destroy(bow.gameObject);
        bow = Instantiate(bowPrefab, bowStartPos, bowStartRot);

        startButton.SetActive(true);
        uiStartButton.gameObject.SetActive(true);

        TicketManager.Instance.AddTicket(roundScore);
        uiScore.text = $"Score: {0}";
        uiStartPanel.gameObject.SetActive(true);
    }


    public void AddScore(int count)
    {
        roundScore += count;
        uiScore.text = $"Score: {roundScore}";
    }
}