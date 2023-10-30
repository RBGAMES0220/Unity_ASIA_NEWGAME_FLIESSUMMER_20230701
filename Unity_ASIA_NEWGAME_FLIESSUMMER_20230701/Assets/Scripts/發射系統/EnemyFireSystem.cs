using UnityEngine;

namespace GLORY
{
    public class EnemyFireSystem :FireSystemBase
    {
        [SerializeField, Header("生成間隔"), Range(0, 3)]
        private float interval = 1.5f;

        private void Awake()
        {
            // 呼叫自訂方法
            //SpawnBullet();

            // 延遲重複呼叫方法(方法名稱.延遲時間.間隔)
            InvokeRepeating("SpawnBullet", 0, interval);
        }
    }
}

