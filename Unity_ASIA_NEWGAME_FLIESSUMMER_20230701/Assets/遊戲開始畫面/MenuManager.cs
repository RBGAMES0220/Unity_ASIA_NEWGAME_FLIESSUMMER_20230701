using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GLORY
{
    public class MenuManager : MonoBehaviour
    {
        private Button btnPlay;

        private void Awake()
        {
            btnPlay = GameObject.Find("開始遊戲").GetComponent<Button>();
            btnPlay.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            SceneManager.LoadScene("01_FLIES_森林 _new");
        }
    }
}

