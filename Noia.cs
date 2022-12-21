using System;
using System.Drawing;

public class Noia : Player
{
    public Noia(PointF location) :
        base(location, Color.Black, Color.Red, "Nóia")
    { }

    int i = 0;
    int count = 0;
    PointF? enemy = null;
    bool isloading = false;
    int points = 0;
    int frame = 0;
    int searchindex = 0;
    bool nuncaMais = false;
    int limitTime = 0;

    protected override void loop()
    {
        if (Energy < 10)
        {
            nuncaMais = true;
            StopMove();
            isloading = true;
        }
        if (isloading)
        {
            if (Energy > 50)
                isloading = false;
            else return;
        }


        if (EnergyRegeneration > 100 || limitTime > 50)
        {

            if (i++ % 5 == 0)
            {
                SizeF direction = new SizeF(
                    (float)Math.Cos((5f * i++) * (2 * Math.PI) / 360f),
                    (float)Math.Sin((5f * i++) * (2 * Math.PI) / 360f)
                );

                Shoot(Location + direction);

            }
            StopMove();
            return;
        }

        if (!nuncaMais)
        {
            if (EntitiesInStrongSonar == 0)
            {
                StrongSonar();
                points = Points;

                limitTime++;
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
                StartMove(FoodsInInfraRed[0]);
                if (Points != points)
                {
                    StrongSonar();
                    StopMove();
                    ResetInfraRed();
                    ResetSonar();
                }
            }
        }



        if (Energy > 20)
        {
            if (i++ % 5 == 0)
            {

                SizeF direction = new SizeF(
                    (float)Math.Cos((5f * i++) * (2 * Math.PI) / 360f),
                    (float)Math.Sin((5f * i++) * (2 * Math.PI) / 360f)
                );

                Shoot(Location + direction);
            }
        }


        // StartTurbo();
        // if (EnemiesInInfraRed.Count > 0)
        // {
        //     enemy = EnemiesInInfraRed[0];
        // }
        // else
        // {
        //     enemy = null;
        // }
        // if (Energy < 10)
        // {
        //     StopMove();
        //     isloading = true;
        //     enemy = null;
        // }
        // if (isloading)
        // {
        //     if (Energy > 60)
        //         isloading = false;
        //     else return;
        // }
        // if (enemy == null && Energy > 10)
        //     InfraRedSensor(5f * i++);
        // else if (enemy != null && Energy > 10)
        // {
        //     InfraRedSensor(enemy.Value);
        //     float dx = enemy.Value.X - this.Location.X,
        //           dy = enemy.Value.Y - this.Location.Y;
        //     if (dx*dx + dy*dy >= 300f*300f)
        //         StartMove(enemy.Value);
        //     else
        //     {
        //         StopMove();
        //         if (i++ % 5 == 0)
        //             Shoot(enemy.Value);
        //     }
        // }
    }
}