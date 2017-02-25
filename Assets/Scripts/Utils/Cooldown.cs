public class Cooldown
{
    private float cooldown;

    public bool IsOver
    {
        get; private set;
    }

    private float timeSinceLastCooldown = 0;

    public Cooldown(float cooldown)
    {
        this.cooldown = cooldown;
        IsOver = true;
    }

    public void Update(float passedTime)
    {
        if (!IsOver)
        {
            timeSinceLastCooldown += passedTime;
            if (timeSinceLastCooldown >= cooldown)
            {
                IsOver = true;
            }
        }
    }

    public void Reset()
    {
        IsOver = false;
        timeSinceLastCooldown = 0;
    }
}