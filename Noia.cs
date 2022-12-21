using System;
using System.Drawing;

public class Noia : Player
{
    public Noia(PointF location) :
        base(location, Color.Green, Color.Red, "Nóia")
    { }

    int i = 0;
    int count = 0;
    PointF? enemy = null;
    bool isloading = false;
    int points = 0;
    int frame = 0;
    int searchindex = 0;

    bool searchingFood = true;
    bool turretMode = true;

    protected override void loop()
    {   
        if(Energy > 50)
            StartTurbo();
        else
        {
            enemy = null;
            StopTurbo();
        }
        
        if (Energy < 10)
        {
            StopMove();
            isloading = true;
        }
        if (isloading)
        {   
            if(Location.X < 10 && Location.Y < 10)
            {
                turretMode = true;
                StopMove();
            }
            else
                StartMove(new PointF(0, 0));


            if (Energy > 50)
                isloading = false;
            else return;
        }


        if(!turretMode)
        {
            if (EnemiesInInfraRed.Count == 0)
            {
                InfraRedSensor(1f * i++);
            }
            else
            {
                enemy = EnemiesInInfraRed[0];
                ResetInfraRed();
            }
        }

        if (enemy != null)
        {
            StartMove(new PointF(enemy.Value.X + 200, enemy.Value.Y + 200));
        }

        if(enemy == null)
        {
            StopMove();
        }

        if (Energy > 50)
        {
            if (i++ % 1 == 0)
            {

                SizeF direction = new SizeF(
                    (float)Math.Cos((1f * i++) * (2 * Math.PI) / 360f),
                    (float)Math.Sin((1f * i++) * (2 * Math.PI) / 360f)
                );

                Shoot(Location + direction);
            }
        }
    }
}
