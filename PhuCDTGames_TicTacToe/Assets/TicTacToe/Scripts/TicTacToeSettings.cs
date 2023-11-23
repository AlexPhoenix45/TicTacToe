using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace GameAdd_TicTacToe
{
    public class TicTacToeSettings : MonoBehaviour {

        public static TicTacToeSettings Instance;

        [SerializeField]
        private TicTacToeController ticTacToeController;
        [SerializeField]
        private RectTransform canvasRect;
        [SerializeField]
        public TextMeshProUGUI p1WinsText;
        [SerializeField]
        private Toggle p1AiToggle;
        [SerializeField]
        public TextMeshProUGUI p2WinsText;
        [SerializeField]
        private Toggle p2AiToggle;
        [SerializeField]
        private Transform settingsPanel;
        [SerializeField]
        private GameObject settingsAiPanel;
        [SerializeField]
        private float animationSpeed = 1;
        [SerializeField]
        private Text speedSliderText;
        [SerializeField]
        private Slider speedSlider;
        [SerializeField]
        private Button startButton;
        [SerializeField]
        private Button hideButton;
        [SerializeField]
        private Button showButton;
        //[SerializeField]
        //private float buttonBlinkSpeed = 4;
        //[SerializeField]
        //private float buttonBlinkDuration = 2.3f;

        public int p1Score = 0;
        public int p2Score = 0;

        private Coroutine hideSettingsCoroutine = null;
        private Coroutine showSettingsCoroutine = null;

        private bool showEndGame; //This is to fix the EndGame Bugs

        private void Start() {
            ticTacToeController.onGameOverDelegate = OnGameOver;
            //StartCoroutine(StartButtonBlinkCoroutine());
            p1AiToggle.isOn = true;
            p2AiToggle.isOn = false;

            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void CheckIfAnyAiIsActive() {
            settingsAiPanel.SetActive(p1AiToggle.isOn || p2AiToggle.isOn);
        }

        public void OnP1AiToggled(bool active) {
            // Debug.Log("OnP1AiToggled " + active);
            ticTacToeController.p1Ai = active;
        }
        public void OnP2AiToggled(bool active) {
            // Debug.Log("OnP2AiToggled " + active);
            ticTacToeController.p2Ai = active;
        }
        public void OnShortcutsToggled(bool active) {
            // Debug.Log("OnShortcutsToggled " + active);
            ticTacToeController.useShortcuts = active;
        }
        public void OnVisualizeToggled(bool active) {
            // Debug.Log("OnVisualizeToggled " + active);
            ticTacToeController.visualizeAI = active;
            speedSliderText.gameObject.SetActive(active);
            speedSlider.gameObject.SetActive(active);
        }
        public void OnSpeedChanged(float value) {
            // Debug.Log("OnSpeedChanged " + value);
            ticTacToeController.algorithmStepDuration = value;
            speedSliderText.text = "Step Duration: " + System.Math.Round(value, 2) + "s";
        }
        public void OnStartClicked() {
            // Debug.Log("OnStartClicked");
            startButton.interactable = false;
            startButton.gameObject.SetActive(false);
            OnHideClicked();
            ticTacToeController.StartGame();
            GameController.Instance.NextRound();

            showEndGame = true;
        }
        public void OnHideClicked() {
            StopAnimationCoroutines();
            hideSettingsCoroutine = StartCoroutine(HideSettingsCoroutine());
        }
        public void OnShowClicked() {
            StopAnimationCoroutines();
            showSettingsCoroutine = StartCoroutine(ShowSettingsCoroutine());
        }

        private void StopAnimationCoroutines() {
            if (hideSettingsCoroutine != null) {
                StopCoroutine(hideSettingsCoroutine);
                hideSettingsCoroutine = null;
            }
            if (showSettingsCoroutine != null) {
                StopCoroutine(showSettingsCoroutine);
                showSettingsCoroutine = null;
            }
        }

        private IEnumerator HideSettingsCoroutine() {
            hideButton.interactable = false;
            while (true) {
                settingsPanel.localScale = new Vector3(settingsPanel.localScale.x - animationSpeed * Time.deltaTime,
                                                       settingsPanel.localScale.y, settingsPanel.localScale.z);
                if (settingsPanel.localScale.x <= 0.1f) {
                    settingsPanel.localScale = new Vector3(0.1f, settingsPanel.localScale.y, settingsPanel.localScale.z);
                    showButton.gameObject.SetActive(true);
                    LayoutRebuilder.ForceRebuildLayoutImmediate(canvasRect);
                    yield break;
                }
                LayoutRebuilder.ForceRebuildLayoutImmediate(canvasRect);
                hideSettingsCoroutine = null;
                yield return null;
            }
        }
        private IEnumerator ShowSettingsCoroutine() {
            showButton.gameObject.SetActive(false);
            while (true) {
                settingsPanel.localScale = new Vector3(settingsPanel.localScale.x + animationSpeed * Time.deltaTime,
                                                       settingsPanel.localScale.y, settingsPanel.localScale.z);
                if (settingsPanel.localScale.x >= 1) {
                    settingsPanel.localScale = new Vector3(1, settingsPanel.localScale.y, settingsPanel.localScale.z);
                    hideButton.interactable = true;
                    LayoutRebuilder.ForceRebuildLayoutImmediate(canvasRect);
                    showSettingsCoroutine = null;
                    yield break;
                }
                LayoutRebuilder.ForceRebuildLayoutImmediate(canvasRect);
                yield return null;
            }
        }

        //private IEnumerator StartButtonBlinkCoroutine() {
            //float animationCounter = 0;

            //float hue = 0.35f;
            //float saturation = 1;
            //float value;

            //while (true) {
            //    ColorBlock colors = startButton.colors;
            //    if (animationCounter >= buttonBlinkDuration) {
            //        colors.normalColor = Color.HSVToRGB(hue, saturation, 0);
            //        startButton.colors = colors;
            //        gameOverText.text = "";
            //        yield break;
            //    }
            //    animationCounter += Time.deltaTime;
            //    value = Mathf.Abs(Mathf.Sin(animationCounter * buttonBlinkSpeed) * 0.5f);
            //    colors.normalColor = Color.HSVToRGB(hue, saturation, value);
            //    startButton.colors = colors;
            //    yield return null;
            //}
        //}

        public void OnGameOver(int win) {
            //Debug.Log("--------------OnGameOver: " + win);
            if (showEndGame)
            {
                if (win == -1) {
                   GameController.Instance.SetGameOverText("Draw at Round " + GameController.Instance.currentRound);
                } else if (win == 0) {
                    if (GameController.Instance.isBotPlaying)
                    {
                        GameController.Instance.SetGameOverText("Bot Won at Round " + GameController.Instance.currentRound);
                        GameController.Instance.p2Score++;
                    }
                    else
                    {
                        GameController.Instance.SetGameOverText("Player 2 Won at Round " + GameController.Instance.currentRound);
                        GameController.Instance.p2Score++;
                    }
                    p1WinsText.text = GameController.Instance.p1Score.ToString();
                    p2WinsText.text = GameController.Instance.p2Score.ToString();
                }
                else
                {
                    if (GameController.Instance.isBotPlaying)
                    {
                        GameController.Instance.SetGameOverText("Player Won at Round " + GameController.Instance.currentRound);
                        GameController.Instance.p1Score++;
                    }
                    else
                    {
                        GameController.Instance.SetGameOverText("Player 1 Won at Round " + GameController.Instance.currentRound);
                        GameController.Instance.p1Score++;
                    }
                    p1WinsText.text = GameController.Instance.p1Score.ToString();
                    p2WinsText.text = GameController.Instance.p2Score.ToString();
                }
                showEndGame = false;
            }

            OnShowClicked();
            startButton.interactable = true;
            startButton.gameObject.SetActive(true);
            //StartCoroutine(StartButtonBlinkCoroutine());
            GameController.Instance.FinalWin();
        }
    }
}
