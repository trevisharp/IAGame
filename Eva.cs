using System;
using System.Drawing;
using System.Linq;

public class Atirante : Player
{
    public Random rand { get; set; } = new Random();
    public PointF dest { get; private set; }
    public int frame { get; set; }
    public bool isLoading { get; set; } = false;
    public Atirante(PointF location) :
        base(location, Color.Purple, Color.White, "Malvadão")
    { this.dest = new PointF((int)rand.NextInt64(0, 1200),(int)rand.NextInt64(0, 800)); }

    protected override void loop()
    {
        if (isLoading)
        {
            if (this.Energy > 30)
                isLoading = false;
            return;
        }

        PointF? enemy = null;
        if (EnemiesInInfraRed.Count > 0)
            enemy = EnemiesInInfraRed[0];
        else
            enemy = null;
        
        if (this.Location.X == dest.X && this.Location.Y == dest.Y)
            dest = new PointF((int)rand.NextInt64(0, 1200),(int)rand.NextInt64(0, 800));
        
        if (this.Energy > 5)
        {
            if (enemy == null)
                InfraRedSensor((int)rand.NextInt64(0, 306));
            if (EnemiesInInfraRed.Count > 0)
            {
                Shoot(EnemiesInInfraRed[0]);
                enemy = EnemiesInInfraRed[0];
            }

            StartMove(dest);
            PointF ponto = new PointF((int)rand.NextInt64(), (int)rand.NextInt64());
            Shoot(ponto);
        }
        else
        {
            StopMove();
            isLoading = true;
        }
        frame++;
    }

    private double hip(double X, double Y)
    {
        double difX = this.Location.X - X;
        double difY = this.Location.Y - Y;

        double catetosAoQuadrado = Math.Pow(difX, 2) + Math.Pow(difY, 2); 

        return Math.Pow(catetosAoQuadrado, 0.5);
    }
}
