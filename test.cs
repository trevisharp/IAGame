// using System.Drawing;

// public class Sla : Player
// {
//     public Sla(PointF location) : 
//         base(location, Color.Purple, Color.Pink, "Sla") { }
//     int screenHeight = 1280;
//     int screenWidth = 800;
//     int frame = 0;
//     PointF? target = null;
//     bool shootNow = false;
//     bool moveNow = false;
//     int countShoot = 0;
//     protected override void loop()
//     {
//         if (target == null)
//         {
//             if (frame % 5 == 0)
//                 AccurateSonar();
//             if (EntitiesInAccurateSonar.Count != 0)
//                 target = EntitiesInAccurateSonar[0];
//         }
//         else
//         {
//             if (EnemiesInInfraRed.Count == 0 && FoodsInInfraRed.Count == 0)
//                 InfraRedSensor(target.Value);
//             else
//             {
//                 if (checkPosition())
//                 {
//                     StopTurbo();
//                     StopMove();
//                     moveNow = false;
//                     target = null;
//                 }
//                 if (moveNow)
//                 {
//                     StartTurbo();
//                     StartMove(target.Value);
//                 }
//                 else if (EnergyRegeneration < 3 && FoodsInInfraRed.Count != 0)
//                 {
//                     target = FoodsInInfraRed[0];
//                     moveNow = true;
//                 }
//                 else if (shootNow)
//                 {
//                     if (frame % 5 == 0)
//                     {
//                         Shoot(target.Value);
//                         countShoot++;
//                     }
//                     if (countShoot == 6)
//                     {
//                         shootNow = false;
//                         countShoot = 0;
//                     }
//                 }
//                 else if (EnemiesInInfraRed.Count != 0)
//                 {
//                     target = EnemiesInInfraRed[0];
//                     shootNow = true;
//                     countShoot = 0;
//                 }
//                 else
//                     target = null;
//             }

//         }
//         frame++;
//     }
//     private bool checkPosition(float range = 5f)
//         => this.Location.X >= this.target.Value.X - range
//         && this.Location.X <= this.target.Value.X + range
//         && this.Location.Y <= this.target.Value.Y - range
//         && this.Location.Y <= this.target.Value.Y + range;
// }