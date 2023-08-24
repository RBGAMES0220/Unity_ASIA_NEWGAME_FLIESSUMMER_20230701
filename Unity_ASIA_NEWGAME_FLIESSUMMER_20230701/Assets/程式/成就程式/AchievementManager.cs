using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public List<Achievement> achievements;
    public bool enemySlayerAchieved;

    public int integer;
    public float floting_point;

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
        InitializeAchievements();
    }
    private void InitializeAchievements()
    {
        if (achievements != null)
            return;

        achievements = new List<Achievement>();
        achievements.Add(new Achievement("Step By Step", "Set your integer to or over 100", (object o) => integer >= 100));
        achievements.Add(new Achievement("Not So Precise", "Set your floating point to or over 50", (object o) => floting_point >= 50F));
        achievements.Add(new Achievement("Enemy Slayer", "Kill your first enemy", (object o) => enemySlayerAchieved));

    }

    private void Update()
    {
        CheckAchievementCompletion();
    }

    private void CheckAchievementCompletion()
    {
        if (achievements == null)
            return;

        foreach (var achievement in achievements)
        {
            achievement.UpdateCompletion();
        }
    }

}


public class Achievement
{
    public string title;
    public string description;
    public Predicate<object> requirement;
    public bool achieved;
    public bool enemySlayerAchieved; // New variable for tracking the "Enemy Slayer" achievement

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
            Debug.Log($"{title}: {description} - Achievement Unlocked!");
            achieved = true;
        }
    }

    public bool RequirementsMet()
    {
        return requirement.Invoke(null);
    }
}
