using System;
using System.Collections.Generic;
using System.Drawing;

public abstract class Player
{
    public Color Color { get; set; } = Color.White;
    public bool IsBroked { get; private set; } = false;
    public double Energy { get; private set; } = 100;
    public double MaxEnergy { get; private set; } = 100;
    public double EnergyRegeneration { get; private set; } = 1;
    public double Life { get; private set; } = 100;
    public double MaxLife { get; private set; } = 100;
    public double LifeRegeneration { get; private set; } = 1;
    public List<PointF> EntitiesInAccurateSonar { get; private set; } = new List<PointF>();
    public int EntitiesInStrongSonar { get; private set; } = 0;
    public List<PointF> EnemiesInInfraRed { get; private set; } = new List<PointF>();
    public List<PointF> FoodsInInfraRed { get; private set; } = new List<PointF>();
    public PointF Location { get; private set; }
    public SizeF Velocity { get; private set; } = SizeF.Empty;

    public void Broke()
    {
        this.IsBroked = true;
    }

    public void AccurateSonar()
    {
        
    }

    public void StrongSonar()
    {

    }

    public void InfraRedSensor(Point p)
    {

    }

    public void InfraRedSensor(float angle)
    {
        
    }

    public void Shoot(Point p)
    {

    }

    public void Shoot(float angle)
    {

    }

    public void StartMove(PointF p)
    {
        p = new PointF(p.X - Location.X, p.Y - Location.Y);
        float mod = (float)Math.Sqrt(p.X * p.X + p.Y * p.Y);
        Velocity = new SizeF(p.X / mod,p.Y / mod);
    }

    public void StartMove(float angle)
    {
        
    }

    public void StopMove(float angle)
    {
        Velocity = SizeF.Empty;
    }

    public void Draw(Graphics g)
    {
        
    }

    public void Loop(Graphics g, float dt)
    {
        loop();
        Draw(g);
        Location += Velocity * dt;
        Energy += EnergyRegeneration;
        Energy -= Velocity.Width * Velocity.Width + Velocity.Height * Velocity.Height;
        if (Energy > MaxEnergy)
            Energy = MaxEnergy;
        if (Energy < 0 || Life < 0)
            Broke();
    }
    protected abstract void loop();
}