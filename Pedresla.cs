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

    int timestopped = 0;

    int searchindex = 0;
    int points = 0;
    bool reset = true;
    bool sonarrunning = false;
    bool isfood = false;
    bool foodpcrl = false;

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

    if(sonarrunning)
    {
        if (EnemiesInInfraRed.Count > 0)
        {
            target = EnemiesInInfraRed[0];
        }
        else
            target = null;
        

        if (target == null)
            InfraRedSensor(5f * frame++);
        
        else
        {
            sonarrunning = false;
            timestopped = 0;

        }
    }
        


    if (isfood)
    {
        if (FoodsInInfraRed.Count == 0)
        {
            foodpcrl = false;
            InfraRedSensor(EntitiesInAccurateSonar[searchindex++ % EntitiesInAccurateSonar.Count]);
            //this.target = EnemiesInInfraRed[0];
        }

        else
        {
            isfood = false;
            this.target = FoodsInInfraRed[0];
            foodpcrl = true;
        }
    }
    
    

    if (!sonarrunning)
    {

        if (target == null)
        {
            if (frame % 5 == 0)
            {
                AccurateSonar();
                timestopped++;
            }

            if (EntitiesInAccurateSonar.Count != 0)
                isfood = true;
                //this.temptarget = EntitiesInAccurateSonar;
                //this.target = EntitiesInAccurateSonar[0];

            if (timestopped == 10)
            {
                timestopped = 0;
                sonarrunning = true;
            }

        }
        else
        {


                if(countShoot <= 10 && !foodpcrl)
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
                        foodpcrl = false;
                        isfood = false;
                        sonarrunning = false;
                        //ResetInfraRed();
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

    }
        

    private bool checkPosition(float range = 5f)
        => (this.Location.X <= this.target.Value.X + range
        && this.Location.X >= this.target.Value.X - range)
        && (this.Location.Y >= this.target.Value.Y - range
        && this.Location.Y <= this.target.Value.Y + range);
}