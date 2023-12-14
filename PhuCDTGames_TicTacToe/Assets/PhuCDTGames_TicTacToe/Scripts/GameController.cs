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

        public int p1Score = 0;
        public int p2Score = 0;

        public GameObject finalWin;

        public bool isBotPlaying;

        public GameObject p1Indicator;
        public GameObject p2Indicator;

        public GameObject blurBackground;
        public GameObject roundWin;
        public GameObject p1Ava;
        public GameObject p2Ava;
        public GameObject drawAva;
        public TextMeshProUGUI roundWinText;

        //Sound Parameter
        public AudioClip placingAudio;
        public AudioClip uiPopup;
        public AudioSource speaker;

        //final Win Fixed Parameter
        public GameObject board;
        public TextMeshProUGUI finalWinP1Score;
        public TextMeshProUGUI finalWinP2Score;

        //fix player identifical problems\
        public TextMeshProUGUI p1Name;
        public TextMeshProUGUI p2Name;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            StartMenu.SetActive(true);
            StartMenu.transform.localScale = Vector2.zero;
            blurBackground.SetActive(true);
            StartMenu.transform.LeanScale(new Vector2(.5f, .5f), 0.5f).setEaseOutBounce();
            PlayUIPopup();
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

        public void SetGameOverText(string text, int whoWin) // 0 = draw, -1 = p2 (bot), 1 = p1
        {
            PlayUIPopup();
            if (p1Score < 5 && p2Score < 5)
            {
                roundWin.SetActive(true);
                roundWin.transform.localScale = Vector2.zero;
                blurBackground.SetActive(true);
                roundWin.transform.LeanScale(new Vector2(1f, 1f ), 0.5f).setEaseOutBounce();
            }

            if (whoWin == -1)
            {
                if (isBotPlaying)
                {
                    p1Ava.SetActive(false);
                    p2Ava.SetActive(true);
                    drawAva.SetActive(false);
                    roundWinText.text = "Bot Won at Round " + currentRound;
                }
                else
                {
                    p1Ava.SetActive(false);
                    p2Ava.SetActive(true);
                    drawAva.SetActive(false);
                    roundWinText.text = "Player 2 Won at Round " + currentRound;
                }
            }
            else if (whoWin == 1)
            {
                if (isBotPlaying)
                {
                    p1Ava.SetActive(true);
                    p2Ava.SetActive(false);
                    drawAva.SetActive(false);
                    roundWinText.text = "Player Won at Round " + currentRound;
                }
                else
                {
                    p1Ava.SetActive(true);
                    p2Ava.SetActive(false);
                    drawAva.SetActive(false);
                    roundWinText.text = "Player 1 Won at Round " + currentRound;
                }
            }
            else
            {
                p1Ava.SetActive(false);
                p2Ava.SetActive(false);
                drawAva.SetActive(true);
                roundWinText.text = "It's a Draw at Round " + currentRound;
            }

            //StopAllCoroutines();
            //IEnumerator SetTextTime()
            //{
            //    gameOverText.text = text;
            //    yield return new WaitForSeconds(3);
            //    gameOverText.text = "";
            //}

            //StartCoroutine(SetTextTime());
            FinalWin();
        }

        public void OnClick_PlayerVSPlayer()
        {
            TicTacToeSettings.Instance.OnP1AiToggled(false);
            TicTacToeSettings.Instance.OnP2AiToggled(false);
            isBotPlaying = false;
            IEnumerator ClosingPanel()

            {
                StartMenu.transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
                yield return new WaitForSeconds(0.5f);
                StartMenu.SetActive(false);
                blurBackground.SetActive(false);
                p1Name.text = "Player 1";
                p2Name.text = "Player 2";
            }

            StartCoroutine(ClosingPanel());
        }

        public void OnClick_PlayerVSComputer()
        {
            TicTacToeSettings.Instance.OnP1AiToggled(true);
            TicTacToeSettings.Instance.OnP2AiToggled(false);
            isBotPlaying = true;

            IEnumerator ClosingPanel()
            {
                StartMenu.transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
                yield return new WaitForSeconds(0.5f);
                StartMenu.SetActive(false);
                blurBackground.SetActive(false);
                p1Name.text = "Player";
                p2Name.text = "AI";
            }

            StartCoroutine(ClosingPanel());
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
            finalWin.SetActive(false);
            board.SetActive(true);
            p1Score = 0;
            p2Score = 0;
            currentRound = 1;
            TicTacToeSettings.Instance.p1WinsText.text = p1Score.ToString();
            TicTacToeSettings.Instance.p2WinsText.text = p2Score.ToString();
            finalWinP1Score.text = p1Score.ToString();
            finalWinP2Score.text = p2Score.ToString();
            currentRoundText.text = "Round " + currentRound;
            //SetGameOverText("");
        }

        public void OnClick_CloseRoundPanel()
        {
            roundWin.transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
            roundWin.SetActive(false);
            blurBackground.SetActive(false);
        }

        public void FinalWin()
        {
            if (p1Score == 5)
            {
                board.SetActive(false);
                finalWin.SetActive(true);
                print("Player Win");
                print("Change the Final Win Panel in here");
            }
            else if (p2Score == 5)
            {
                board.SetActive(false);
                finalWin.SetActive(true);
                print("Bot Win");
                print("Change the Final Win Panel in here");
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
                p1Indicator.SetActive(true);
                p2Indicator.SetActive(false);
            }
            else
            {
                p1Indicator.SetActive(false);
                p2Indicator.SetActive(true);
            }
        }

        public void PlayPlacingSound()
        {
            speaker.PlayOneShot(placingAudio);
        }

        public void PlayUIPopup()
        {
            speaker.PlayOneShot(uiPopup);
        }
    }
}
