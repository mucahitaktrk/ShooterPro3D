using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<GameObject> _enemys;
    [SerializeField] private GameObject[] _panel;
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private TextMeshProUGUI _levelText;
    private int _highScore = 0;
    public bool isWin;

    [SerializeField] private GameObject[] _levels;
    private int _level;
    

    private void Awake()
    {
        instance = this;
        _scoreText.gameObject.SetActive(true);
        _highScoreText.gameObject.SetActive(false);
        _highScore = PlayerPrefs.GetInt("HighScore");
        _level = PlayerPrefs.GetInt("Level");
        Instantiate(_levels[_level]);
    }

    private void Start()
    {
        _enemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    private void Update()
    {
        _levelText.text = "LEVEL : " + (_level + 1);
        _scoreText.text = "SCORE : " + CharacterManager.score.ToString();
        _highScoreText.text = "HIGH SCORE : " + _highScore;
        if (_highScore < CharacterManager.score)
        {
            _highScore = CharacterManager.score;
            PlayerPrefs.SetInt("HighScore", _highScore);
        }
        GameWin();
    }

    public void GameLose()
    {
        _panel[1].SetActive(true);
        _scoreText.gameObject.SetActive(false);
        _highScoreText.gameObject.SetActive(true);
    }

    private void GameWin()
    {
        for (int i = 0; i < _enemys.Count; i++)
        {
            if (!_enemys[i])
            {
                _enemys.RemoveAt(i);
            }
        }
        if (_enemys.Count < 1)
        {
            isWin = true;
            _panel[0].SetActive(true);
            _scoreText.gameObject.SetActive(false);
            _highScoreText.gameObject.SetActive(true);
        }
    }

    public void LoseGameButton()
    {
        Destroy(_levels[_level]);
        SceneManager.LoadScene(0);
    }

    public void WinLevel()
    {
        Destroy(_levels[_level]);
        _level++;
        if (_level > 2)
        {
            _level = 0;
        }
        PlayerPrefs.SetInt("Level", _level);
        SceneManager.LoadScene(0);
    }
}
