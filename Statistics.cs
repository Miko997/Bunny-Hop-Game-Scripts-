using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int PersonalBest
    {
        get => PlayerPrefs.GetInt("PersonalBest", 0);
        set => PlayerPrefs.SetInt("PersonalBest", value);
    }

    public static int GamesPlayed
    {
        get => PlayerPrefs.GetInt("GamesPlayed", 0);
        set => PlayerPrefs.SetInt("GamesPlayed", value);
    }

    public static void UpdatePersonalBest(int newScore)
    {
        if (newScore > PersonalBest)
        {
            PersonalBest = newScore;
        }
    }

    public static void IncrementGamesPlayed()
    {
        GamesPlayed++;
    }
}
