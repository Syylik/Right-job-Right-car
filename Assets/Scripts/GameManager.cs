using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { PLAY, WIN, LOSE };
    [HideInInspector] public GameState gameState;

    [SerializeField] private Car[] cars;
    private Car curCar;
    [SerializeField] private Transform carArrow;

    [HideInInspector] public bool isChosing;
    [HideInInspector] public bool isMoving;
    public enum Proffesions { MEDIC, COP, ROBBER, SANTA, FIREMAN, TAXI }

    [SerializeField] private GameObject winPanel, losePanel;

    public static GameManager instance;
    private void Awake() => instance = this;
    private void Start() => StartCoroutine(Gameplay());
    private IEnumerator Gameplay()
    {
        for(int i = 0; i < cars.Length; i++)
        {
            isMoving = false;
            curCar = cars[i];
            SetArrowPos(curCar.transform);
            isChosing = true;
            yield return new WaitUntil(() => !isChosing);
        }
        Win();
    }
    public void ClickOnPeople(People people)
    {
        if(isChosing && !isMoving)
        {
            isMoving = true;
            people.target = curCar.transform;
            people.moving = true;
        }
    }
    private void SetArrowPos(Transform car)
    {
        carArrow.position = new Vector3(car.position.x, carArrow.position.y, car.position.z);
    }
    public void Win()
    {
        if(gameState != GameState.PLAY) return;
        gameState = GameState.WIN;
        winPanel.SetActive(true);
    }
    public void Lose()
    {
        if(gameState != GameState.PLAY) return;
        gameState = GameState.LOSE;
        losePanel.SetActive(true);
    }
    public void Menu() => SceneManager.LoadScene(0);
    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void NextLvl() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
}
