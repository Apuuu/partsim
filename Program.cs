namespace cproj;

class Program
{

    public static double particleAttraction = 15.0;
    readonly static double partMass = 2.0;
    readonly static double maxForce = 0.5;
    readonly static double accelMod = 1;
    public static double dampingMod = 0.8;
    readonly public static double repellForce = 1.5;
    readonly public static double repellDistance = 150;
    public readonly static int size = 720;
    public static int amount = 100;
    public static bool allowSim = true;
    public static Vector2D[] vell = new Vector2D[amount];

    static void Main(){

        PartWindow window = new();

        Phys.initVell(vell);

        Random r = new();

        System.Windows.Forms.Timer timer = new()
        {
            Interval = 16
        };

        timer.Tick += (sender, e) =>{

            if(allowSim){
                runSim(window);
            }else if(allowSim == false){
                restartSim(window, r);
            }

        };

        timer.Start();

        Application.Run(window);

        timer.Stop();
        timer.Dispose(); 
        
    }

    public static void runSim(PartWindow window){
 
        for (int i = 0; i < amount; i++){

            Vector2D totalAcc = new Vector2D(0, 0);

            for (int j = 0; j < amount; j++){
                if (i != j){
                    Vector2D acc = Phys.CalculateGravAcc(window.GetCirclePos(i), window.GetCirclePos(j), particleAttraction, partMass, maxForce);
                    totalAcc.x += acc.x;
                    totalAcc.y += acc.y;
                }
            }

            vell[i].x += totalAcc.x * accelMod;
            vell[i].y += totalAcc.y * accelMod;

            vell[i].x *= dampingMod;
            vell[i].y *= dampingMod;

            window.MoveCircleByIndex(i, vell[i].x, vell[i].y);
        }
    
    }

    public static void restartSim(PartWindow window, Random r){
        Phys.initVell(vell);
        for(int i = 0; i < amount; i++){
            window.setCirclePosByIndex(i, r.Next(0, size), r.Next(0, size));
        }
        allowSim = true;
    }

}   