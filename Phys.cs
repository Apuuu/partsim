namespace cproj;

public class Vector2D(double x, double y)
{
    public double x = x;
    public double y = y;
}
class Phys{

    readonly static Random r = new();
    public static double getVec2Dlength(Vector2D vec){
        double vectx = vec.x+Program.size/2;
        double vecty = vec.y+Program.size/2;
        return Math.Sqrt(vectx*vectx + vecty*vecty);
    }

    public static void initVell(Vector2D[] vell){
        for(int i = 0; i < Program.amount; i++){
            vell[i] = new Vector2D(0,0);
        }
    }

    public static void ShuffleVell(Vector2D[] vell){
        for(int i = 0; i < Program.amount; i++){

            double x = r.NextDouble() * Program.size;
            double y = r.NextDouble() * Program.size;
            vell[i] = new Vector2D(x,y);
        }
    }

    public static Vector2D CalculateGravAcc(Vector2D pos1, Vector2D pos2, double particleAttraction, double partMass, double maxForce)
    {
        double dx = pos2.x - pos1.x;
        double dy = pos2.y - pos1.y;

        double distanceSquared = dx * dx + dy * dy;
        if (distanceSquared == 0)
        {
            return new Vector2D(0, 0);
        }

        double forceMagnitude;  
        if (distanceSquared < Program.repellDistance){
            forceMagnitude = -Program.repellForce;
        }else{
            forceMagnitude = Math.Clamp(particleAttraction * partMass * partMass / distanceSquared, -maxForce, maxForce);
        }

        double acceleration = forceMagnitude / Math.Sqrt(distanceSquared);

        return new Vector2D(dx * acceleration, dy * acceleration);
    }
    
}