using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralyzetheenemy : MonoBehaviour
{
    // 其他成員變數和方法...

    public void ApplyParalyze(float duration)
    {
        StartCoroutine(ParalyzeCoroutine(duration));
    }

    public void TakeDamage(int amount)
    {
        // 執行敵人受傷害的相關邏輯
    }

    IEnumerator ParalyzeCoroutine(float duration)
    {
        // 麻痺狀態的實現...
        yield return new WaitForSeconds(duration);
        // 恢復正常狀態的操作...
    }
}
