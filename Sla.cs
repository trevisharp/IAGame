using System;
using System.Drawing;

public class Sla : Player
{
    public Sla(PointF location) : 
        base(location, Color.Purple, Color.Pink, "Sla") { }
    
    int frame = 0;
    int countShoot = 0;
    int stop = 0;
    private PointF? target = null;
    PointF ponto = new PointF();
    protected override void loop()
    {
        if (target == null)
        {
            if (frame % 5 == 0)
                AccurateSonar();
            if (EntitiesInAccurateSonar.Count != 0)
                this.target = EntitiesInAccurateSonar[0];
        }
        else
        {
            stop = 0;
            if (checkPosition())
            {
                target = null;
                countShoot = 0;
            }
            else
            {
                if(countShoot <= 6)
                {
                    if (frame % 3 == 0)
                    {
                        Shoot(target.Value);
                        countShoot++;
                    }
                }
                else
                {
                    StartTurbo();
                    StartMove(target.Value);
                }
            }

        }
        frame++;
    }

    private bool checkPosition(float range = 4f)
        => this.Location.X >= this.target.Value.X - range
        && this.Location.X <= this.target.Value.X + range
        && this.Location.Y <= this.target.Value.Y + range
        && this.Location.Y <= this.target.Value.Y + range;
}