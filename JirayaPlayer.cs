using System;
using System.Drawing;

public class JirayaPlayer : Player
{
    public Random rand { get; set; } = new Random();
    public PointF dest { get; private set; }
    public JirayaPlayer(PointF location) :
        base(location, Color.DeepPink, Color.White, "Nova Era")
    { this.dest = new PointF((int)rand.NextInt64(0, 1200), (int)rand.NextInt64(0, 800)); }


    int parado = 0;
    int frame = 0;
    int searchindex = 0;
    int points = 0;
    int i = 0;
    int firecount = 0;
    int contador = 0;
    PointF? enemy = null;
    bool isloading = false;
    int giro = 0;
    PointF topLeft = new PointF(50, 50);

    protected override void loop()
    {
        StartMove(topLeft);
        var local = Location;
        if (Energy > 10)
        {
            

            // if (FoodsInInfraRed.Count > 0 && EnemiesInInfraRed.Count > 0) 
            // {
            //     StartMove(FoodsInInfraRed[0]);
            // }

            if (EnemiesInInfraRed.Count > 0)
            {
                enemy = EnemiesInInfraRed[0];
            }
            else
            {
                enemy = null;
                firecount = 0;
            }

            // if (Energy < 10)
            // {
            //     StopMove();
            //     isloading = true;
            //     enemy = null;
            //     firecount = 0;
            // }

            if (isloading)
            {
                if (Energy > 60)
                    isloading = false;
                else return;
            }

            if (enemy == null && Energy > 10)
            {
                if (giro % 2 == 0)
                {
                    InfraRedSensor(5f * i++);
                }

                else
                {
                    InfraRedSensor(5f * i--);
                }

            }
            else if (enemy != null && Energy > 10)
            {
                float dx = enemy.Value.X - this.Location.X,
                      dy = enemy.Value.Y - this.Location.Y;
                if (dx * dx + dy * dy >= 3000f * 3000f)
                {

                }
                else if (firecount < 8)
                {
                    Shoot(enemy.Value);
                    firecount++;
                }
                contador++;
                i++;
                if (contador == 15)
                {
                    enemy = null;
                    firecount = 0;
                    contador = 0;
                    giro++;
                    ResetInfraRed();
                }
            }
        }

        else if (Energy < 20 || Location == topLeft)
        {
            StopMove();
            if (Energy > 10)
            {
                if (enemy == null && Energy > 10)
                {
                    if (giro % 2 == 0)
                    {
                        InfraRedSensor(5f * i++);
                    }

                    else
                    {
                        InfraRedSensor(5f * i--);
                    }

                }


                if (EnemiesInInfraRed.Count > 0)
                {
                    enemy = EnemiesInInfraRed[0];
                }

                else if (enemy != null && Energy > 10 || Location == topLeft)
                {
                    float dx = enemy.Value.X - this.Location.X,
                          dy = enemy.Value.Y - this.Location.Y;
                    if (dx * dx + dy * dy >= 3000f * 3000f)
                    {

                    }
                    else if (firecount < 5)
                    {
                        Shoot(enemy.Value);
                        firecount++;
                    }
                    contador++;
                    i++;
                    local = Location;
                    if (contador == 1)
                    {
                        enemy = null;
                        firecount = 0;
                        contador = 0;
                        giro++;
                        ResetInfraRed();
                    }
                }

            }
        }
        else if ((local.X - Location.X) * (local.X - Location.X) + (local.Y - Location.Y) * (local.Y - Location.Y) < 15f)
        {
            parado++;

            if (parado > 10)
            {
                StartMove(FoodsInInfraRed[0]);
                parado = 0;
            }
                
        }
        
    }

}
