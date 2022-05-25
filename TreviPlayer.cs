using System.Drawing;

public class TreviPlayer : Player
{
    public TreviPlayer(PointF location) : base(location) 
    {
        PrimaryColor = Color.Green;
        SecundaryColor = Color.Purple;
    }

    int i = 0;
    PointF? enemy = null;

    protected override void loop()
    {
        if (enemy == null && Energy > 10)
            InfraRedSensor(5f * i++);
        else if (enemy != null && Energy > 10)
        {
            InfraRedSensor(enemy.Value);
            Shoot(enemy.Value);
        }
        if (EnemiesInInfraRed.Count > 0)
        {
            enemy = EnemiesInInfraRed[0];
        }
    }
}