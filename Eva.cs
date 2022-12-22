using System;
using System.Drawing;

public class Eva : Player
{
    public Eva(PointF location) :
        base(location, Color.Blue, Color.Red, "Autobosts")
    { 
        int width = 1500;
        int height = 200;

        isEsq = this.Location.X <= width / 2;
        isTop = this.Location.Y <= height / 2;
        PointF destFinal;
        int corrX = 0;
        int corrY = 0;
        

        if (isEsq && isTop)
        {
            destFinal = new PointF(0,0);
            corrX = 100;
            corrY = 100;
            quadShoot = 0;
        }
        else if (isEsq && !isTop)
        {
            destFinal = new PointF(0,height);
            corrX = 100;
            corrY = 100;
            quadShoot = 3;
        }
        else if (!isEsq && isTop)
        {
            destFinal = new PointF(width, 0);
            corrX = -100;
            corrY = 100;
            quadShoot = 1;
        }
        else
        {
            destFinal = new PointF(width, height);
            corrX = -100;
            corrY = -100;
            quadShoot = 2;
        }

        destStart = new PointF(destFinal.X + corrX, destFinal.Y + corrY);
        angles[0] = new int[2] {0, 90};
        angles[1] = new int[2] {91, 180};
        angles[2] = new int[2] {181, 270}; 
        angles[3] = new int[2] {271, 359};

        Corr = new PointF((int)rand.NextInt64(50), (int)rand.NextInt64(50));
    }
    int mod = 0;
    int tiro = 0;
    int index = 0;
    int frame = 0;
    PointF? enemy = null;
    public PointF Corr { get; set; }
    public int quadShoot { get; private set; }
    public bool isEsq { get; private set; }
    public bool isTop { get; private set; }
    public PointF destStart { get; private set; }
    public int[][] angles { get; private set; } = new int[4][];
    Random rand = new Random();
    protected override void loop()
    {
        if (this.Energy <= 10)
            mod = 2;
        if (mod == 0)
        {
            StartTurbo();
            StartMove(destStart);
            if (this.Location
            .isInside (
                    new PointF (
                        destStart.X - 75, destStart.Y - 75
                    ),
                    new PointF (
                        destStart.X + 70, destStart.Y + 70
                    )
                ))
            {
                mod = 1;
                StopMove();
                StopTurbo();
            }
            return;
        }

        if (mod == 1)
        {
            if (angles[quadShoot][0] + index * 2 >= angles[quadShoot][1])
                index = 0;
            InfraRedSensor(angles[quadShoot][0] + index * 2);
            index++;

            if (EnemiesInInfraRed.Count >= 1 && enemy == null)
            {
                enemy = EnemiesInInfraRed[0];
            }

            if (enemy != null && tiro < 10)
            {
                Shoot(enemy.Value);
                tiro++;
            }
            if (tiro == 10)
            {
                enemy = null;
                tiro = 0;
            }
        }

        frame++;
        if (frame % 40 == 0)
        {
            PointF x = new PointF(this.destStart.X + (int)rand.NextInt64(50 + (int)Corr.X), this.destStart.Y + (int)rand.NextInt64(50 + (int)Corr.Y));
            StartMove(x);
        }

        else if (frame >= 20*60)
        {
            StartMove(new PointF(1200/2, 800/2));
        }
        

        if (mod == 2) {
            StopTurbo();
            if (this.Energy >= 30)
                mod = 1;
        }
        

    }
}

public static class Extensions {
    public static bool isInside(this PointF myLocation, PointF start, PointF end)
    {
        if (!(start.X < myLocation.X))
            return false;
        
        if (!(myLocation.X <= end.X))
            return false;


        if (!(start.Y < myLocation.Y))
            return false;
        
        if (!(myLocation.Y <= end.Y))
            return false;
        
        return true;
    }

}
