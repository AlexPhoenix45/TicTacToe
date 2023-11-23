using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace GameAdd_TicTacToe
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        //Variables
        public int currentRound = 0;
        public TextMeshProUGUI currentRoundText;
        [SerializeField]
        private TextMeshProUGUI gameOverText;

        public GameObject StartMenu;
        public GameObject AreYouSure;

        public bool dummyAI;

        public TextMeshProUGUI p1Total;
        public TextMeshProUGUI p2Total;
        public TextMeshProUGUI whoWin;
        public GameObject totalScore;

        public int p1Score = 0;
        public int p2Score = 0;

        public bool isBotPlaying;

        public GameObject p1Indicator;
        public GameObject p2Indicator;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void NextRound()
        {
            currentRound++;
            currentRoundText.text = "Round " + currentRound;

            int random = Random.Range(0, 2);
            if (random == 1)
            {
                dummyAI = true;
            }
            else
            {
                dummyAI = false;
            }
        }

        public void SetGameOverText(string text)
        {
            StopAllCoroutines();
            IEnumerator SetTextTime()
            {
                gameOverText.text = text;
                yield return new WaitForSeconds(3);
                gameOverText.text = "";
            }

            StartCoroutine(SetTextTime());
            FinalWin();
        }

        public void OnClick_PlayerVSPlayer()
        {
            TicTacToeSettings.Instance.OnP1AiToggled(false);
            TicTacToeSettings.Instance.OnP2AiToggled(false);
            isBotPlaying = false;
            StartMenu.SetActive(false);
        }

        public void OnClick_PlayerVSComputer()
        {
            TicTacToeSettings.Instance.OnP1AiToggled(true);
            TicTacToeSettings.Instance.OnP2AiToggled(false);
            isBotPlaying = true;
            StartMenu.SetActive(false);
        }

        public void OnClick_AreYouSure()
        {
            AreYouSure.SetActive(true);
        }

        public void OnClick_NoRestart()
        {
            AreYouSure.SetActive(false);
        }

        public void OnClick_Restart()
        {
            AreYouSure.SetActive(false);
            TicTacToeSettings.Instance.p1Score = 0;
            TicTacToeSettings.Instance.p2Score = 0;
            currentRound = 1;
            TicTacToeSettings.Instance.p1WinsText.text = TicTacToeSettings.Instance.p1Score.ToString();
            TicTacToeSettings.Instance.p2WinsText.text = TicTacToeSettings.Instance.p2Score.ToString();
            currentRoundText.text = "Round " + currentRound;
            SetGameOverText("");
        }

        public void FinalWin()
        {
            if (TicTacToeSettings.Instance.p1Score == 5)
            {
                totalScore.SetActive(true);
                p1Total.text = TicTacToeSettings.Instance.p1Score.ToString();
                p2Total.text = TicTacToeSettings.Instance.p2Score.ToString();
                whoWin.text = "Player X Win!";
            }
            else if (TicTacToeSettings.Instance.p2Score == 5)
            {
                totalScore.SetActive(true);
                p1Total.text = TicTacToeSettings.Instance.p1Score.ToString();
                p2Total.text = TicTacToeSettings.Instance.p2Score.ToString();
                whoWin.text = "Player O Win!";
            }
        }

        public void OnClick_RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void WhoseTurn(bool value)
        {
            //value = false => P1, value = true => p2
            if (value == false)
            {

            }
        }
    }
}
