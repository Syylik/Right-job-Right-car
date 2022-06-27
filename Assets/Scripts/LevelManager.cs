using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void OpenLevel(LevelData levelData) => SceneManager.LoadScene(levelData.levelId);
    public void Menu() => SceneManager.LoadScene(0);
}
