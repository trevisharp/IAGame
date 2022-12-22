using System;
using System.Drawing;

public class Feshow : Player
{
    public Feshow(PointF location) : 
        base(location, Color.White, Color.Red, "Feshow") { }

    int i = 0;
    int frame = 0;
    int searchindex = 0;
    int points = 0;
    int currentFrame = 0;
    PointF? lastshot = new PointF();
    PointF? enemy = new PointF();
    PointF? enemyLast = new PointF();
    bool isloading = false;
    Point point = new Point();

    bool atack = false;
    bool move = true;
    bool search = false;
    bool wait = false;
    double currentLife = 100;

    protected override void loop()
    {
        if (wait)
        {
            currentFrame++;
            frame++;
            
            if ((currentFrame % 3) == 0)
            {
                wait = false;
                search = true;
            }   


            return;
        }
        
        if (move)
        {
            if(Location.X > 640 && Location.Y > 400)
            {
                point.X = 1260;
                point.Y = 780;
            } else if(Location.X > 640 && Location.Y < 400)
            {
                point.X = 1260;
                point.Y = 20;
            } else if(Location.X < 640 && Location.Y > 400)
            {
                point.X = 20;
                point.Y = 780;
            } else if(Location.X < 640 && Location.Y < 400)
            {
                point.X = 20;
                point.Y = 20;
            }

            StartTurbo();
            StartMove(point);
            float dx = Location.X - point.X;
            dx = dx < 0 ? -dx : dx;
            float dy = Location.Y - point.Y;
            dy = dy < 0 ? -dy : dy;
            
            PointF pt = new PointF(dx, dy);

            if (pt.X <= 2 && pt.Y <= 2)
            {
                move = false;
                StopTurbo();
                StopMove();
                atack = true;
            }
        }

        if (search)
        {
                if (EnemiesInInfraRed.Count > 0)
                {
                    enemy = EnemiesInInfraRed[0];
                }
                else
                {
                    enemy = null;
                }
                if (Energy < 10)
                {
                    isloading = true;
                    enemy = null;
                }
                if (isloading)
                {
                    if (Energy > 60)
                        isloading = false;
                    else return;
                }
                if (enemy == null && Energy > 10)
                    InfraRedSensor(5f * i++);
                if (enemy != null && Energy > 10)
                {
                    search = false;
                    atack = true;
                }
        }

        if (atack)
        {
            if(Energy > 40)
            {
                if (EnemiesInInfraRed.Count > 0)
                {
                    enemy = EnemiesInInfraRed[0];
                }
                else
                {
                    enemy = null;
                }
                if (Energy < 10)
                {
                    isloading = true;
                    enemy = null;
                }
                if (isloading)
                {
                    if (Energy > 60)
                        isloading = false;
                    else return;
                }
                if (enemy == null && Energy > 10)
                    InfraRedSensor(5f * i++);
                else if (enemy != null && Energy > 10)
                {
                    InfraRedSensor(enemy.Value);
                    float dx = enemy.Value.X - this.Location.X,
                        dy = enemy.Value.Y - this.Location.Y;
                    if (dx * dx + dy * dy >= 2000f * 2000f)
                        {
                            frame++;
                            if (Energy < 10 || frame % 10 == 0)
                                return;
                            if (EntitiesInStrongSonar == 0)
                            {
                                StrongSonar();
                                points = Points;
                                return;
                            }
                            else if (EntitiesInAccurateSonar.Count == 0)
                            {
                                AccurateSonar();
                                return;
                            }
                            else if (FoodsInInfraRed.Count == 0)
                            {
                            
                                InfraRedSensor(EntitiesInAccurateSonar[searchindex++ % EntitiesInAccurateSonar.Count]);
                                return;
                            }
                        }
                    else
                    {
                        PointF alvo = new PointF();

                        if ((enemyLast.Value != enemy.Value) && (enemyLast != null))
                        {
                            alvo.X = (enemy.Value.X + (enemy.Value.X - enemyLast.Value.X )*25);
                            alvo.Y = (enemy.Value.Y + (enemy.Value.Y - enemyLast.Value.Y )*25); 
                        }
                        else
                        {
                            alvo.X = enemy.Value.X;
                            alvo.Y = enemy.Value.Y;
                        }
                        Shoot(alvo);

                        enemyLast = enemy.Value;

                        atack = false;
                        search = false;
                        wait = true;
                        currentFrame = 0;
                        return;
                    }
                }
            }

        }
        
    }
}