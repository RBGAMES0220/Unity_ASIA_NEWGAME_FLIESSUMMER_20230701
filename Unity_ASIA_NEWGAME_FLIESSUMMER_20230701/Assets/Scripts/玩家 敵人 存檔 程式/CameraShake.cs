using System.Collections;
using UnityEngine;

namespace GLORY
{
    public class CameraShake : MonoBehaviour
    {
        public float shakeDuration = 0.1f; // 震動持續時間
        public float shakeMagnitude = 0.1f; // 震動幅度

        private Vector3 originalPosition; // 攝像機原始位置

        private void Start()
        {
            originalPosition = transform.localPosition;
        }

        public void Shake()
        {
            StartCoroutine(ShakeCamera());
        }

        private IEnumerator ShakeCamera()
        {
            float elapsedTime = 0f;

            while (elapsedTime < shakeDuration)
            {
                // 隨機計算震動的偏移量
                float x = Random.Range(-1f, 1f) * shakeMagnitude;
                float y = Random.Range(-1f, 1f) * shakeMagnitude;

                // 設置新的攝像機位置
                transform.localPosition = originalPosition + new Vector3(x, y, 0f);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            // 震動結束後，將攝像機位置重置回原始位置
            transform.localPosition = originalPosition;
        }
    }
}
