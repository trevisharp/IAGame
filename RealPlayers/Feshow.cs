using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

[Ignore]
public class Feshow : Player
{
    public Feshow(PointF location) : 
        base(location, Color.DarkSalmon, Color.DarkRed, "In(fernal)") { }

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
    int ind = 0;
    protected override void loop()
    {
        frame++;
    
        List<Point> pontos = new List<Point>();

        Point point1 = new Point(0, 0);
        Point point2 = new Point(0, 800);
        Point point3 = new Point(1280, 0);
        Point point4 = new Point(1280, 800);

        Point point5 = new Point(0, 400);
        Point point6 = new Point(640, 800);
        Point point7 = new Point(1280, 400);
        Point point8 = new Point(640, 0);

        Point point9 = new Point(0, 200);
        Point point10 = new Point(0, 600);
        Point point11 = new Point(320, 800);
        Point point12 = new Point(960, 800);
        Point point13 = new Point(1280, 600);
        Point point14 = new Point(1280, 200);
        Point point15 = new Point(960, 0);
        Point point16 = new Point(320, 0);

        pontos.Add(point1);
        pontos.Add(point2);
        pontos.Add(point3);
        pontos.Add(point4);
        pontos.Add(point5);
        pontos.Add(point6);
        pontos.Add(point7);
        pontos.Add(point8);
        pontos.Add(point9);
        pontos.Add(point10);
        pontos.Add(point11);
        pontos.Add(point12);
        pontos.Add(point13);
        pontos.Add(point14);
        pontos.Add(point15);
        pontos.Add(point16);
        

        ind += 1;
        if (ind == pontos.Count())
        {
            ind = 0;
        }

        if (Energy > 5)
        {
            Shoot(pontos[ind]);
        }
        
    }
}