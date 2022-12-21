using System;
using System.Drawing;

public class JirayaPlayer : Player
{
    public JirayaPlayer(PointF location) :
        base(location, Color.Red, Color.White, "Jiraya")
    { }

    int frame = 0;
    int searchindex = 0;
    int points = 0;
    int i = 0;
    PointF? enemy = null;
    bool isloading = false;

    protected override void loop()
    {

        if(Energy > 40)
        {
            if (EnemiesInInfraRed.Count > 0)
        {
            enemy = EnemiesInInfraRed[0];
        }
        else
        {
            enemy = null;
        }
        if (Energy < 10)
        {
            StopMove();
            isloading = true;
            enemy = null;
        }
        if (isloading)
        {
            if (Energy > 60)
                isloading = false;
            else return;
        }
        if (enemy == null && Energy > 10)
            InfraRedSensor(5f * i++);
        else if (enemy != null && Energy > 10)
        {
            InfraRedSensor(enemy.Value);
            float dx = enemy.Value.X - this.Location.X,
                  dy = enemy.Value.Y - this.Location.Y;
            if (dx * dx + dy * dy >= 1000f * 1000f)
                {
                    frame++;
                    if (Energy < 10 || frame % 10 == 0)
                        return;
                    if (EntitiesInStrongSonar == 0)
                    {
                        StrongSonar();
                        points = Points;
                        return;
                    }
                    else if (EntitiesInAccurateSonar.Count == 0)
                    {
                        AccurateSonar();
                        return;
                    }
                    else if (FoodsInInfraRed.Count == 0)
                    {
                     
                        InfraRedSensor(EntitiesInAccurateSonar[searchindex++ % EntitiesInAccurateSonar.Count]);
                        return;
                    }
                    else
                    {
                        StartMove(FoodsInInfraRed[0]);
                        return;
                    }
                }
            else
            {
                StopMove();
                Shoot(enemy.Value);
                return;
            }
        }
        }

        else
        {
            frame++;
        if (Energy < 10 || frame % 10 == 0)
            return;
        if (EntitiesInStrongSonar == 0)
        {
            StrongSonar();
            points = Points;
            return;
        }
        else if (EntitiesInAccurateSonar.Count == 0)
        {
            AccurateSonar();
            return;
        }
        else if (FoodsInInfraRed.Count == 0)
        {
            InfraRedSensor(EntitiesInAccurateSonar[searchindex++ % EntitiesInAccurateSonar.Count]);
            return;
        }
        else
        {
            StartMove(FoodsInInfraRed[0]);
            return;
        }

        }
    }

}