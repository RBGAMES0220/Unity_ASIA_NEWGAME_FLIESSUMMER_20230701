using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GLORY
{
    public class FinalManager : MonoBehaviour
    {
        public static FinalManager instance;

        CanvasGroup groupFinal;

        TextMeshProUGUI textTitle;

        Button btnReplay;

        private void Awake()
        {
            instance = this;

            groupFinal = GameObject.Find("結束畫布").GetComponent<CanvasGroup>();
            textTitle = GameObject.Find("結束標題").GetComponent<TextMeshProUGUI>();
            btnReplay = GameObject.Find("重新遊戲").GetComponent <Button>();
            btnReplay.onClick.AddListener(Replay);
        }

        public void GameOver(string title)
        {
            textTitle.text = title;
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            for (int i = 0; i < 10; i++)
            {
                groupFinal.alpha += 0.1f;
                yield return new WaitForSeconds(0.02f);
            }

            groupFinal.interactable = true;
            groupFinal.blocksRaycasts = true;
        }

        private void Replay()
        {
            SceneManager.LoadScene("遊戲開始選單");
        }
    }
}
