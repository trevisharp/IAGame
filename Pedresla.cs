using System;
using System.Drawing;

public class PedreslaPlayer : Player
{
    public PedreslaPlayer(PointF location) : 
        base(location, Color.Purple, Color.Pink, "Pedrésla") { }
    int height = 1280;
    int whight = 800;
    int frame = 0;
    int countShoot = 0;
    private PointF? target = null;
    PointF ponto = new PointF();
    PointF? flagDamage = null;

    int searchindex = 0;
    int points = 0;
    bool reset = true;

    protected override void loop()
    {

        frame++;


    //     if (reset)
    //     {
            
    //     }

    //     if (EntitiesInStrongSonar == 0 || reset)
    //     {
    //         StrongSonar();
    //         points = Points;
    //     }
    //     else if (EntitiesInAccurateSonar.Count == 0)
    //     {
    //         AccurateSonar();
    //     }
    //     else if (FoodsInInfraRed.Count == 0)
    //     {
    //         InfraRedSensor(EntitiesInAccurateSonar[searchindex++ % EntitiesInAccurateSonar.Count]);
    //     }
    //     else
    //     {
    //         StartMove(FoodsInInfraRed[0]);
    //         if (Points != points)
    //         {
    //             StartTurbo();
    //             StrongSonar();
    //             StopMove();
    //             ResetInfraRed();
    //             ResetSonar();
    //         }
    //     }




    // }

        


        if (target == null)
        {
            if (frame % 5 == 0)
                AccurateSonar();
            if (EntitiesInAccurateSonar.Count != 0)
                this.target = EntitiesInAccurateSonar[0];
        }
        else
        {
 
                if(countShoot <= 10)
                {
                    if (frame % 3 == 0)
                    {
                        Shoot(target.Value);
                        countShoot++;
                    }
                }
                else
                {

                    if (checkPosition())
                    {
                        target = null;
                        countShoot = 0;
                        StopMove();
                        AccurateSonar();
                    }

                    else
                    {
                        //StartTurbo();
                        StartMove(target.Value);

                    }

                }
            }


        }

        
        

    private bool checkPosition(float range = 5f)
    {
        if((this.Location.X <= this.target.Value.X + range && this.Location.X >= this.target.Value.X - range) && (this.Location.Y <= this.target.Value.Y - range && this.Location.Y <= this.target.Value.Y + range))
        {
            return true;
        }
        return false;
    }

}