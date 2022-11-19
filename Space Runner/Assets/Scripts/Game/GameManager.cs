using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] int targetFPS;
    public GameObject lose;
    public GameObject win;
    public TextMeshProUGUI TEXT;
    public TextMeshProUGUI timerTEXT;
    public  int timeToSurviveInSeconds;
    private int timeToSurviveMinutes;
    private float timeToSurviveSeconds;
    public static bool gameFinished;
    float elapsed = 0f;
    int workingSpaceships;
    int goals;
    [SerializeField] TextMeshProUGUI goalsText;
    [SerializeField]SpaceshipForRepair[] spaceshipsForRepair;

    #region singleton
    private static GameManager instance;
    public static GameManager GetInstance => instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }
    #endregion


    void Start()
    {
        Application.targetFrameRate = targetFPS;
        workingSpaceships = 0;
        gameFinished = false;
        lose.SetActive(false);
        win.SetActive(false);
        TEXT.SetText("LOST");

        timeToSurviveMinutes = timeToSurviveInSeconds / 60;
        timeToSurviveSeconds = timeToSurviveInSeconds % 60;
        timerTEXT.SetText((timeToSurviveMinutes.ToString() + ":" + timeToSurviveSeconds.ToString()));
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            if (gameFinished == false)
            {
                UpdateTimer();
            }
        }

        for(int i =0; i<spaceshipsForRepair.Length; i++)
        {
            if(spaceshipsForRepair[i].spaceshipDurability > 0f && timeToSurviveInSeconds <= -1 && gameFinished != true)
            {
                workingSpaceships++;
                if(workingSpaceships == spaceshipsForRepair.Length)
                {
                    // Win
                    gameFinished = true;
                    win.SetActive(true);
                }
            }
            else if (spaceshipsForRepair[i].spaceshipDurability <= 0f && timeToSurviveInSeconds <= -1 && gameFinished != true)
            {
                // Lose
                gameFinished = true;
                lose.SetActive(true);
            }
        }
    }

    void UpdateTimer()
    {
        timeToSurviveInSeconds -= 1;
        timeToSurviveMinutes = timeToSurviveInSeconds / 60;
        timeToSurviveSeconds = timeToSurviveInSeconds % 60;
        if (timeToSurviveSeconds >= 10)
            timerTEXT.SetText((timeToSurviveMinutes.ToString() + ":" + timeToSurviveSeconds.ToString()));
        else
            timerTEXT.SetText((timeToSurviveMinutes.ToString() + ":" + "0" + timeToSurviveSeconds.ToString()));
    }

    public void addGoal(bool isGoal)
    {
        if (isGoal)
        {
            goals++;
            goalsText.SetText(goals.ToString());
        }
        else
        {
            goals--;
            goalsText.SetText(goals.ToString());
        }

    }
}
