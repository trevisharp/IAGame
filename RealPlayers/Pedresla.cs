using System;
using System.Drawing;

[Ignore]
public class PedreslaPlayer : Player
{
    public PedreslaPlayer(PointF location) : 
        base(location, Color.Purple, Color.Pink, "Pedrésla") { }
    int height = 1280;
    int whight = 800;
    int frame = 0;
    int countShoot = 0;
    private PointF? target = null;
    private PointF? temptarget = null;
    PointF ponto = new PointF();
    PointF? flagDamage = null;

    public PointF? valor = null;

    int points = 0;

    int timestopped = 0;

    int searchindex = 0;
    bool reset = true;
    bool sonarrunning = false;
    bool isfood = false;
    bool foodpcrl = false;
    bool attack = false;


    int lastlife;

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



    // points = this.Points;



    // if (points % 3 == 0 && points !=0)
    // {
    //     attack = true;
    // }
    // // }

    if(sonarrunning)
    {
        if (EnemiesInInfraRed.Count > 0)
        {
            target = EnemiesInInfraRed[0];
        }
        else
            target = null;
        

        if (target == null)
            InfraRedSensor(4f * frame++);
        
        else
        {
            sonarrunning = false;
            timestopped = 0;
            attack = false;
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
            {
                InfraRedSensor(EntitiesInAccurateSonar[searchindex++ % EntitiesInAccurateSonar.Count]);
                isfood = true;
            }
                //this.temptarget = EntitiesInAccurateSonar;
                //this.target = EntitiesInAccurateSonar[0];

            if (timestopped == 2)
            {
                timestopped = 0;
                sonarrunning = true;
            }

        }
        else
        {


                if(countShoot <= 20 && !foodpcrl)
                {
                    StopTurbo();
                    if (frame % 4 == 0)
                    {
                        StopTurbo();
                        Shoot(target.Value);
                        countShoot++;
                    }
                }

                if(countShoot >= 20)
                {
                    StopTurbo();
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