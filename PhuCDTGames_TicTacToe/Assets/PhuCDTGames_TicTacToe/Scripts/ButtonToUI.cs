using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameAdd_TicTacToe
{
    public class ButtonToUI : MonoBehaviour
    {
        Button button;
        public Text text;
        public GameObject X;
        public GameObject O;

        public bool isClear;

        void Start()
        {
            button = GetComponent<Button>();
            text = GetComponentInChildren<Text>();
            button.image.color = Color.clear;
            text.text = "";
            text.color = Color.clear;
        }

        void Update()
        {
            if (text.text == "")
            {
                isClear = true;
                text.color = Color.clear;
                X.SetActive(false);
                O.SetActive(false);
            }
            else if (text.text == "O")
            {
                isClear = false;
                text.color = Color.clear;
                O.SetActive(true);
                X.SetActive(false);
            }
            else if (text.text == "X")
            {
                isClear = false;
                text.color = Color.clear;
                X.SetActive(true);
                O.SetActive(false);
            }
            else
            {
                isClear = false;
                text.color = Color.clear;
                O.SetActive(false);
                X.SetActive(false);
            }
        }
    }
}
