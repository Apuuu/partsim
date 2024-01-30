namespace cproj;
using System.Windows.Forms;
using System.Drawing;

class SettWindow : Form{

    public static bool activeSettWindow = false;
    public static bool applyChanges = false;

    public SettWindow(){

        initComponents();

        BackColor = Color.LightGray;

    }

    public void initComponents(){
        CreateSettingsWindow();

        #pragma warning disable CS8622
        TextBox amountField = CreateInputField(Program.dampingMod.ToString(),new Point(150, 20), new Size(50,10), AmountField_changed);
        Controls.Add(amountField);

        TextBox ParticleAttractionField = CreateInputField(Program.particleAttraction.ToString(),new Point(150, 50), new Size(50,10), ParticleAttractionField_changed);
        Controls.Add(ParticleAttractionField);

        Label DampingMod = createLabel("DampingMod", new Point(10,20));
        Controls.Add(DampingMod);

        Label ParticleAttraction = createLabel("ParticleAttraction", new Point(10,50));
        Controls.Add(ParticleAttraction);

        AddApplyButton();

        this.FormClosed += SettWindowWindow_FormClosed;
    }

    private void SettWindowWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
        activeSettWindow = false;
    }

    public void CreateSettingsWindow(){
        
        Text = "Particle Sim v0.1.0 settings";
        Size = new Size(250, (int)(Program.size*0.5));
        BackColor = Color.LightGray;

        activeSettWindow = true;

    }

    private TextBox CreateInputField(string text, Point location, Size size, EventHandler textChangedHandler)
    {
        TextBox inputField = new()
        {
            Text = text,
            Location = location,
            Size = size
        };

        inputField.TextChanged += textChangedHandler;
        return inputField;
    }

    public void AmountField_changed(object sender, EventArgs e){
        TextBox textBox = (TextBox)sender;
        string userInput = textBox.Text;
        
        if(double.TryParse(userInput, out double damount)){
            if(damount < 1 && damount > 0.01){
                Program.dampingMod = damount;
            }else{
                Program.dampingMod = 1;
            } 
        }else{
            #pragma warning disable CS1717

            Program.dampingMod = Program.dampingMod;
        }

    }

    public void ParticleAttractionField_changed(object sender, EventArgs e){
        TextBox textBox = (TextBox)sender;
        string userInput = textBox.Text;
        
        if(double.TryParse(userInput, out double damount)){
            if(damount < 20 && damount > -20){
                Program.particleAttraction = damount;
            }else{
                Program.particleAttraction = 1;
            }
        }else{
            #pragma warning disable CS1717

            Program.particleAttraction = Program.particleAttraction;
        }
        
        applyChanges = false;
    }

    public Label createLabel(string text, Point point){
        Label label = new Label();
        label.Text = text;
        label.Location = point;

        return label;
    }

    public void AddApplyButton(){
        Button applyButton = new(){
            Text = "Apply",
            Location = new System.Drawing.Point(125, 270)
        };

        applyButton.Click += ApplyButton_Click;
        Controls.Add(applyButton);
            
    }

    private void ApplyButton_Click(object? sender, EventArgs e){
        
        applyChanges = true;

    }        
}