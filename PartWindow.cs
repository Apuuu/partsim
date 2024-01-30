namespace cproj;
using System.Windows.Forms;
using System.Drawing;

class PartWindow : Form
{

    public PartWindow()
    {
        CreateWindow();
        AddRestartButton();
        AddSettingsButton();
        GenCircles(Program.amount);

        DoubleBuffered = true;
    }
    
    public static List<(double x, double y, double radius)> circles = [];
    public static void RefreshCircles(){
        circles.Clear();
    }

    public void CreateWindow()
    {

        Text = "Particle Sim v0.1.0";
        Size = new Size(Program.size, Program.size);
        BackColor = Color.LightGray;

    }

    //Restart Button
    public void AddRestartButton(){
        Button restartButton = new(){
            Text = "Restart",
            Location = new System.Drawing.Point(10, 10)
        };

        restartButton.Click += RestartButton_Click;
        Controls.Add(restartButton);
            
    }

    private void RestartButton_Click(object? sender, EventArgs e){
        Program.allowSim = false;
        Phys.ShuffleVell(Program.vell);
         
    }

        //Settings Button
    public void AddSettingsButton(){
        Button settingsButton = new(){
            Text = "Settings",
            Location = new System.Drawing.Point(90, 10)
        };

        settingsButton.Click += SettingsButton_Click;
        Controls.Add(settingsButton);
            
    }

    private void SettingsButton_Click(object? sender, EventArgs e){
        if(!SettWindow.activeSettWindow){
            
            SettWindow settWindow = new()
            {
                Owner = this
            };
            settWindow.Show();
            SettWindow.activeSettWindow = true;

        }
            
    }

    public Vector2D GetCirclePos(int index){
        return new Vector2D(circles[index].x, circles[index].y);
    }

    readonly Random r = new();

    public void GenCircles(int amount){
        for (int i = 0; i < amount; i++){
            double x = r.NextDouble() * Program.size;
            double y = r.NextDouble() * Program.size;

            circles.Add((x, y, 1.5));
        }

        Invalidate();
    }

    public void MoveCircleByIndex(int index, double deltaX, double deltaY){
        if (index >= 0 && index < circles.Count){
            var circle = circles[index];
            circles[index] = (circle.x + deltaX, circle.y + deltaY, circle.radius);
            Invalidate();
        }else{
            Console.WriteLine("Invalid index: " + index);
        }
    }

    public void setCirclePosByIndex(int index, double x, double y){
        if (index >= 0 && index < circles.Count){
            var circle = circles[index];
            circles[index] = (x, y, circle.radius);
            Invalidate();
        }else{
            Console.WriteLine("Invalid index: " + index);
        }
    }

    public void AddText(string text, Font font, Brush brush, PointF position){
        using Graphics g = CreateGraphics();
        g.DrawString(text, font, brush, position);
    }

    protected override void OnPaint(PaintEventArgs e){
        base.OnPaint(e);
        Graphics g = e.Graphics;

        foreach (var (x, y, radius) in circles)
        {
            Rectangle rect = new((int)(x - radius), (int)(y - radius), (int)(radius * 2), (int)(radius * 2));
            g.DrawEllipse(Pens.Black, rect);
        }
    }
}