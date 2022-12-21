using System.Drawing;

public class Sla : Player
{
    public Sla(PointF location) : 
        base(location, Color.Purple, Color.Pink, "Sla") { }
    int height = 1280;
    int whight = 800;
    int frame = 0;
    int countShoot = 0;
    private PointF? target = null;
    PointF ponto = new PointF();
    PointF? flagDamage = null;
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

    private bool checkPosition(float range = 5f)
        => this.Location.X >= this.target.Value.X - range
        && this.Location.X <= this.target.Value.X + range
        && this.Location.Y <= this.target.Value.Y - range
        && this.Location.Y <= this.target.Value.Y + range;
}