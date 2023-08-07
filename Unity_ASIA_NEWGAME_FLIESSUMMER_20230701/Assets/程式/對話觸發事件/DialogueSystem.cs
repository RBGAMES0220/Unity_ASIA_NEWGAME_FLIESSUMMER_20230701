using UnityEngine;
using TMPro;
using System.Collections;

using UnityEngine.Events;

namespace Justin
{
    /// <summary>
    /// 對話系統
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        #region 資料區
        [SerializeField, Header("對話間隔"), Range(0, 0.5f)]
        private float dialogueIntervalTime = 0.1f;
        [SerializeField, Header("開頭對話")]
        private DialogueData dialogueOpening;
        [SerializeField, Header("對話按鍵")]
        private KeyCode dialogueKey = KeyCode.Space;

        private WaitForSeconds dialogueInterva => new WaitForSeconds(dialogueIntervalTime);
        private CanvasGroup groupDialogue;
        private TextMeshProUGUI textName;
        private TextMeshProUGUI textContent;
        private GameObject goTriangle;
        #endregion

        private PlayerInput PlayerInput;

        private UnityEvent onDialogueFinish;

        #region 事件
        private void Awake()
        {
            groupDialogue = GameObject.Find("畫布對話系統").GetComponent<CanvasGroup>();
            textName = GameObject.Find("對話者名稱").GetComponent<TextMeshProUGUI>();
            textContent = GameObject.Find("對話內容").GetComponent<TextMeshProUGUI>();
            goTriangle = GameObject.Find("對話完成圖示");
            goTriangle.SetActive(false);

            PlayerInput = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>();

            StartDialogue(dialogueOpening);
        }
        #endregion
        public void StartDialogue(DialogueData data, UnityEvent _onDialogueFinish = null)
        {
            PlayerInput.enabled = false;
            StartCoroutine(FadeGroup());
            StartCoroutine(TypeEffect(data));
            onDialogueFinish = _onDialogueFinish;
        }

        private IEnumerator FadeGroup(bool fadeIn = true)
        {

            float increase = fadeIn ? +0.1f : -0.1f;

            for (int i = 0; i < 10; i++)
            {
                groupDialogue.alpha += increase;
                yield return new WaitForSeconds(0.04f);
            }
        }

        private IEnumerator TypeEffect(DialogueData data)
        {
            textName.text = data.dialogueName;

            for (int j = 0; j < data.dialogueContents.Length; j++)
            {
                textContent.text = "";
                goTriangle.SetActive(false);



                string dialogue = data.dialogueContents[j];

                for (int i = 0; i < dialogue.Length; i++)
                {
                    textContent.text += dialogue[i];
                    yield return dialogueInterva;
                }
                goTriangle.SetActive(true);

                while (!Input.GetKeyDown(dialogueKey))
                {
                    yield return null;
                }

                print("<color=#993300>玩家按下按鍵!</color");
            }

            StartCoroutine(FadeGroup(false));

            PlayerInput.enabled = true;
            onDialogueFinish?.Invoke();
        }
    }

    internal class PlayerInput
    {
        internal bool enabled;
    }
}
