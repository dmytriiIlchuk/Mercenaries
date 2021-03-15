using Godot;
public class Random
{
    private static Random instance = null;

    public static Random Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Random();
            }
            return instance;
        }
    }

    private RandomNumberGenerator rng = new RandomNumberGenerator();

    public float GetRandomNumber(float from, float to)
    {
        rng.Randomize();
        return rng.RandfRange(from, to);
    }

    public Vector2 GetRandomVector(float fromX, float toX, float fromY, float toY)
    {
        rng.Randomize();
        return new Vector2(GetRandomNumber(fromX, toX), GetRandomNumber(fromY, toY));
    }
}