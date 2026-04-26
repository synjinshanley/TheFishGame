public static class GameSettings
{
    public enum Difficulty { Easy, Medium, Hard }
    public static Difficulty CurrentDifficulty = Difficulty.Medium;

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
}
