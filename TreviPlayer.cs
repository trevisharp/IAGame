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
        StartTurbo();
        if (enemy == null && Energy > 10)
            InfraRedSensor(5f * i++);
        else if (enemy != null && Energy > 10)
        {
            InfraRedSensor(enemy.Value);
            if (enemy.Value.X - this.Location.X > 600 ||
                enemy.Value.Y - this.Location.Y > 600)
                StartMove(enemy.Value);
            else
            {
                StopMove();
                if (i++ % 5 == 0)
                Shoot(enemy.Value);
            }
        }

        if (EnemiesInInfraRed.Count > 0)
        {
            enemy = EnemiesInInfraRed[0];
        }
        else
        {
            enemy = null;
        }
    }
}