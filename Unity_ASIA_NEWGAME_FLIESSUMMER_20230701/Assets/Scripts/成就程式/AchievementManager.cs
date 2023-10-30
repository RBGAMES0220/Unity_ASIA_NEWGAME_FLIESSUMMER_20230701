using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public List<Achievement> achievements; // 存放成就的列表
    public bool enemySlayerAchieved; // 標記是否達成「敵人殺手」成就

    public int integer; // 整數
    public float floting_point; // 浮點數

    public bool AchievementUnlocked(string achievementName)
    {
        bool result = false;

        if (achievements == null)
            return false;

        Achievement[] achievementsArrry = achievements.ToArray();
        Achievement a = Array.Find(achievementsArrry, ach => achievementName == ach.title);

        if (a == null)
            return false;

        result = a.achieved;

        return result;
    }

    private void Start()
    {
        InitializeAchievements(); // 初始化成就
    }

    private void InitializeAchievements()
    {
        //if (achievements != null)
            //return;

        achievements = new List<Achievement>();
        achievements.Add(new Achievement("Step By Step", "設定你的整數為100或以上", (object o) => integer >= 100));
        achievements.Add(new Achievement("Not So Precise", "設定你的浮點數為50或以上", (object o) => floting_point >= 50F));
        achievements.Add(new Achievement("Enemy Slayer", "殺死你的第一個敵人", (object o) => enemySlayerAchieved));

        // 添加更多成就，如：
        // achievements.Add(new Achievement("Another Achievement", "描述", (object o) => /* 條件判斷 */));
    }

    private void Update()
    {
        CheckAchievementCompletion(); // 檢查成就是否達成
    }

    private void CheckAchievementCompletion()
    {
        if (achievements == null)
            return;

        foreach (var achievement in achievements)
        {
            achievement.UpdateCompletion(); // 更新成就狀態
        }
    }
}

[System.Serializable]
public class Achievement
{
    public string title; // 成就標題
    public string description; // 成就描述
    public Predicate<object> requirement; // 達成成就的條件
    public bool achieved; // 是否已經達成成就
    public bool enemySlayerAchieved; // 用來追蹤「敵人殺手」成就的變數

    public Achievement(string title, string description, Predicate<object> requirement)
    {
        this.title = title;
        this.description = description;
        this.requirement = requirement;
    }

    public void UpdateCompletion()
    {
        if (achieved)
            return;

        if (RequirementsMet())
        {
            Debug.Log($"{title}: {description} - 成就解鎖！"); // 輸出已解鎖的成就
            achieved = true;
        }
    }

    public bool RequirementsMet()
    {
        return requirement.Invoke(null);
    }

    public void EnemyDeath() // 假設這是敵人死亡的事件處理函數
    {
        // 處理敵人死亡邏輯

        // 當玩家殺死第一個敵人時，設定「敵人殺手」成就為已解鎖
        enemySlayerAchieved = true;

        // 其他處理代碼...
    }
}
