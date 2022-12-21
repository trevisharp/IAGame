using System;
using System.Drawing;

public class PedrePlayer : Player
{
    public PedrePlayer(PointF location) : 
        base(location, Color.Green, Color.Purple, "Pedré") { }

    int i = 0;
    PointF? enemy = null;
    bool isloading = false;

    protected override void loop()
    {
        if (EnemiesInInfraRed.Count > 0)
        {
            enemy = EnemiesInInfraRed[0];
        }
        else
        {
            enemy = null;
        }
        if (Energy < 20)
        {
            StopMove();
            isloading = true;
            enemy = null;
        }
        if (isloading)
        {
            if (Energy > 50)
                isloading = false;
            else return;
        }
        if (enemy == null && Energy > 20)
            InfraRedSensor(5f * i++);
        else if (enemy != null && Energy > 20)
        {
            InfraRedSensor(enemy.Value);
            float dx = enemy.Value.X - this.Location.X,
                  dy = enemy.Value.Y - this.Location.Y;
            if (dx*dx + dy*dy >= 500f*500f)
                StartMove(enemy.Value);
            else
            {
                StopMove();
                for (int i = 0; i < 10; i++)
                {
                    Shoot(enemy.Value);
                }
            }
        }
    }
}