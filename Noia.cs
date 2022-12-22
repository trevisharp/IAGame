using System;
using System.Drawing;
using System.Collections.Generic;

public class Noia : Player
{
    public Noia(PointF location) :
        base(location, Color.Green, Color.Red, "Nóia")
    { }

    int i = 0;

    const float screenWidth = 1280;
    const float screenHeigth = 800;

    bool isloading = false;
    bool findWay = false;
    bool turretMode = true;

    PointF topLeft = new PointF(0, 0);
    PointF topRight = new PointF(screenWidth, 0);
    PointF botLeft = new PointF(0, screenHeigth);
    PointF botRight = new PointF(screenWidth, screenHeigth);

    int offset = 10;

    // List<PointF> pontos = new List<PointF> { new PointF(0, 0), new PointF(screenWidth, 0), new PointF(0, screenHeigth), new PointF(screenWidth, screenHeigth)};

    PointF pontoSelecionado;

    protected override void loop()
    {

        // StopMove();
        StartTurbo();

        if (Energy < 10)
        {
            StopMove();
            isloading = true;
        }

        if (isloading)
        {
            if (Energy > 30)
                isloading = false;
            else return;
        }


        if ((Location.X >= pontoSelecionado.X - 10 && Location.X <= pontoSelecionado.X + 10)
            && (Location.Y >= pontoSelecionado.Y - 10 && Location.Y <= pontoSelecionado.Y + 10))
        {
            StopMove();
            turretMode = true;
        }

        if (!findWay)
        {
            if (Location.X < screenWidth / 2 && Location.Y < screenHeigth / 2)
            {
                StartMove(topLeft);
                pontoSelecionado = topLeft;
            }

            else if (Location.X >= screenWidth / 2 && Location.Y < screenHeigth / 2)
            {
                StartMove(topRight);
                pontoSelecionado = topRight;
            }

            else if (Location.X >= screenWidth / 2 && Location.Y >= screenHeigth / 2)
            {
                StartMove(botRight);
                pontoSelecionado = botRight;
            }

            else if (Location.X < screenWidth / 2 && Location.Y >= screenHeigth / 2)
            {
                StartMove(botLeft);
                pontoSelecionado = botLeft;
            }

            findWay = true;
        }


        if (Energy > 60 && turretMode)
        {
            SizeF direction = new SizeF(
                (float)Math.Cos((3f * i++) * (2 * Math.PI) / 360),
                (float)Math.Sin((3f * i++) * (2 * Math.PI) / 360)
            );

            if(pontoSelecionado == botRight)
            {

                if(direction.Height > 0)
                {
                    direction.Height *= -1;
                } 

                if(direction.Width > 0)
                {
                    direction.Width *= -1;
                }

                Shoot(Location + direction);
            } 
            else if(pontoSelecionado == topLeft)
            {
                if(direction.Height < 0)
                    direction.Height *= -1;

                if(direction.Width < 0)
                    direction.Width *= -1;

                Shoot(Location + direction);
            }
            else if(pontoSelecionado == topRight)
            {
                if(direction.Height < 0)
                    direction.Height *= -1;
            
                if(direction.Width > 0)
                    direction.Width *= -1;

                Shoot(Location + direction);
            }
            else if(pontoSelecionado == botLeft)
            {
                if(direction.Height > 0)
                    direction.Height *= -1;
            
                if(direction.Width < 0)
                    direction.Width *= -1;

                Shoot(Location + direction);
            }
        }
    }
}
