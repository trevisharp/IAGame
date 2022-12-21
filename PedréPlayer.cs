using System.Drawing;

public class Pedre : Player
{
    public Pedre(PointF location) : 
        base(location, Color.Black, Color.Purple, "Pedré") { }

    int searchindex = 0;
    int frame = 0;
    int points = 0;
    int i = 0;
    PointF? enemy = null;

    bool looking = false;

    protected override void loop()
    {
        frame++;
        if (Energy < 10 || frame % 10 == 0)
            return;
        if (EntitiesInStrongSonar == 0)
        {
            if(looking == false)
            {
                looking = true;
            }

            if (looking == true)
            {   
                if (EnemiesInInfraRed.Count > 0)
                {
                    enemy = EnemiesInInfraRed[0];
                }
                else
                {
                    enemy = null;
                }

                if(frame % 20 == 0)
                {
                    StrongSonar();
                }


                if (enemy == null && Energy > 10)
                    InfraRedSensor(5f * i++);
                else if (enemy != null && Energy > 10)
                {
                    InfraRedSensor(enemy.Value);
                    float dx = enemy.Value.X - this.Location.X,
                        dy = enemy.Value.Y - this.Location.Y;
                    if (dx*dx + dy*dy >= 300f*300f)
                        StartMove(enemy.Value);
                    else
                    {
                        StopMove();
                        if (i++ % 5 == 0)
                            Shoot(enemy.Value);
                    }
                }





            }
            points = Points;
        }


        else if (EntitiesInAccurateSonar.Count == 0)
        {
            AccurateSonar();
        }
        else if (FoodsInInfraRed.Count == 0)
        {
            InfraRedSensor(EntitiesInAccurateSonar[searchindex++ % EntitiesInAccurateSonar.Count]);
        }
        else
        {
            looking = false;
            StartMove(FoodsInInfraRed[0]);
            if (Points != points)
            {
                StartTurbo();
                StrongSonar();
                StopMove();
                ResetInfraRed();
                ResetSonar();
            }
        }
    }
}