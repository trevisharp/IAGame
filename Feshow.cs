using System;
using System.Drawing;

public class Feshow : Player
{
    public Feshow(PointF location) : 
        base(location, Color.White, Color.Red, "Feshow") { }

    int i = 0;
    PointF? enemy = null;
    bool isloading = false;

    Point point = new Point();

    bool atack = false;
    bool move = true;
    double currentLife = 100;

    protected override void loop()
    {
    
        
        if (move)
        {
            if(Location.X > 640 && Location.Y > 400)
            {
                point.X = 1260;
                point.Y = 780;
            } else if(Location.X > 640 && Location.Y < 400)
            {
                point.X = 1260;
                point.Y = 20;
            } else if(Location.X < 640 && Location.Y > 400)
            {
                point.X = 20;
                point.Y = 780;
            } else if(Location.X < 640 && Location.Y < 400)
            {
                point.X = 20;
                point.Y = 20;
            }

            StartTurbo();
            StartMove(point);
            float dx = Location.X - point.X;
            dx = dx < 0 ? -dx : dx;
            float dy = Location.Y - point.Y;
            dy = dy < 0 ? -dy : dy;
            
            PointF pt = new PointF(dx, dy);

            if (pt.X <= 2 && pt.Y <= 2)
            {
                move = false;
                StopTurbo();
                StopMove();
                atack = true;
            }
        }

        if (atack)
        {
            if (EnemiesInInfraRed.Count > 0)
            {
                enemy = EnemiesInInfraRed[0];
            }
            if (Energy < 10)
            {
                isloading = true;
                enemy = null;
            }
            if (isloading)
            {
                if (Energy > 20)
                    isloading = false;
                else return;
            }
            if (enemy == null && Energy > 10)
                InfraRedSensor(5f * i++);
            else if (enemy != null && Energy > 10)
            {
                InfraRedSensor(enemy.Value);

                Shoot(enemy.Value);
            }
        }
        
    }
}
