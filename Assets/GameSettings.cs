public static class GameSettings
{
    public enum Difficulty { Easy, Medium, Hard }

    public enum Level { One, Two, Three }
    public static Difficulty CurrentDifficulty = Difficulty.Medium;

    public static Level CurrentLevel = Level.One;

    public static int GetLevel()
    {
        return CurrentLevel switch
        {
            Level.One => 1,
            Level.Two => 2,
            Level.Three => 3,
            _ => 1
        };
    }

    // QTE timing values for each difficulty
    public static float GetMashTime()
    {
        return CurrentDifficulty switch
        {
            Difficulty.Easy   => 5f,
            Difficulty.Medium => 3f,
            Difficulty.Hard   => 2f,
            _ => 3f
        };
    }

    public static float GetMultiTime()
    {
        return CurrentDifficulty switch
        {
            Difficulty.Easy   => 6f,
            Difficulty.Medium => 4f,
            Difficulty.Hard   => 2.5f,
            _ => 4f
        };
    }

    public static int GetScoreThreshold()
    {
        return CurrentDifficulty switch
        {
            Difficulty.Easy   => 100,
            Difficulty.Medium => 200,
            Difficulty.Hard   => 350,
            _ => 200
        };
    }

    public static int GetLevelThreshold()
    {
        return CurrentLevel switch
        {
            Level.One => 0,
            Level.Two => 150,
            Level.Three => 300,
            _ => 0
        };
    }
}
