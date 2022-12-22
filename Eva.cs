using System;
using System.Drawing;

public class Eva : Player
{
    public Eva(PointF location) :
        base(location, Color.Blue, Color.Red, "Autobosts")
    { 
        int width = 1280;
        int height = 800;

        isEsq = this.Location.X <= width / 2;
        isTop = this.Location.Y <= height / 2;
        PointF destFinal;
        int corrX = 0;
        int corrY = 0;
        

        if (isEsq && isTop)
        {
            destFinal = new PointF(0, 0);
            corrX = 20;
            corrY = 20;
            quadShoot = 0;
        }
        else if (isEsq && !isTop)
        {
            destFinal = new PointF(0, height);
            corrX = 20;
            corrY = 20;
            quadShoot = 3;
        }
        else if (!isEsq && isTop)
        {
            destFinal = new PointF(width, 0);
            corrX = -20;
            corrY = 20;
            quadShoot = 1;
        }
        else
        {
            destFinal = new PointF(width, height);
            corrX = -20;
            corrY = -20;
            quadShoot = 2;
        }

        destStart = new PointF(destFinal.X, destFinal.Y);
        angles[0] = new int[2] {-3, 93};
        angles[1] = new int[2] {91, 183};
        angles[2] = new int[2] {179, 273}; 
        angles[3] = new int[2] {272, 363};
    }
    int mod = 1;
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
    double lastLife = 0;
    SizeF? runDirection = null;
    int runCount = 0;
    protected override void loop()
    {
        if (runCount > 0)
            runCount++;
        
        if (runCount == 20)
        {
            runDirection = new SizeF(runDirection.Value.Height, -runDirection.Value.Width);
            StartMove(runDirection.Value);
        }
        else if (runCount > 40)
        {
            runDirection = null;
            StopMove();
            StopTurbo();
            runCount = 0;
            mod = 0;
        }
        else if (lastLife > Life)
        {
            StartTurbo();
            runCount++;
            float dx = this.Location.X - LastDamage.Value.X;
            float dy = this.Location.Y - LastDamage.Value.Y;
            runDirection = new SizeF(dx,dy);

            StartMove(runDirection.Value);
        }
        lastLife = Life;

        if (this.Energy <= 10)
            mod = 2;
       

        if (mod == 1)
        {
            if (angles[quadShoot][0] + index * 2 >= angles[quadShoot][1])
                index = 0;
            
            if (frame % 2 == 0)
                InfraRedSensor(angles[quadShoot][0] + index * 2);
            index++;

            if (EnemiesInInfraRed.Count >= 1 && enemy == null)
                enemy = EnemiesInInfraRed[0];
            

            if (enemy != null && tiro < 5)
            {
                Shoot(enemy.Value);
                tiro++;
            }
            if (tiro == 5)
            {
                enemy = null;
                tiro = 0;
            }
        }

        frame++;

        if (frame >= 20*22)
        {
            StartMove(new PointF(1280/2, 800/2));
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
