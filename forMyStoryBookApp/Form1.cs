using System.Data.SQLite;

namespace forMyStoryBookApp
{
    public partial class Form1 : Form
    {
        public int ID = 0;
        public int level = 0;
        public String name;
        public int age = 0;
        public double averageLife = 60;
        public int HP = 0;
        public int MP = 0;
        public int SP = 0;
        public int skillPoints = 0;
        public int Strength = 0;
        public int Intelligence = 0;
        public int Agility = 0;
        public int Vigor = 0;
        public int Durability = 0;
        public String Intuition;
        public String Class;
        public String Title;
        public static Boolean checkingP = false;



        List<PersonModel> people = new List<PersonModel>();



        private Dictionary<Control, Rectangle> controls = new Dictionary<Control, Rectangle>();
        private Size originalSize;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.Resize += Form1_Resize;
            LoadPeopleList();
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
#nullable disable
            originalSize = this.ClientSize;

            // Tüm bileþenlerin baþlangýç konumunu ve boyutunu kaydet
            foreach (Control ctrl in this.Controls)
            {
                controls[ctrl] = new Rectangle(ctrl.Location, ctrl.Size);
            }

        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            float scaleX = (float)this.ClientSize.Width / originalSize.Width;
            float scaleY = (float)this.ClientSize.Height / originalSize.Height;

            foreach (Control ctrl in this.Controls)
            {
                if (controls.ContainsKey(ctrl))
                {
                    Rectangle original = controls[ctrl];

                    // Bileþenin yeni boyutunu ve konumunu hesapla
                    int newX = (int)(original.X * scaleX);
                    int newY = (int)(original.Y * scaleY);
                    int newWidth = (int)(original.Width * scaleX);
                    int newHeight = (int)(original.Height * scaleY);

                    ctrl.SetBounds(newX, newY, newWidth, newHeight);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void LoadPeopleList()
        {
            people = SqlliteDataAccess.LoadPeople();
            WireUpPeopleList();
        }
        private void WireUpPeopleList()
        {
            listBoxNames.DataSource = null;
            listBoxNames.DataSource = people;
            listBoxNames.DisplayMember = "FullName";
        }

        private void listBoxNames_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelSP_Click(object sender, EventArgs e)
        {

        }

        private void buttonCheckNormal_Click(object sender, EventArgs e)
        {
            int age, level, strength, intelligence, agility, vigor, durability, id;
            if (!int.TryParse(textBoxAge.Text.Trim(), out age) ||
                !int.TryParse(textBoxLevel.Text.Trim(), out level) ||
                !int.TryParse(textBoxStrength.Text.Trim(), out strength) ||
                !int.TryParse(textBoxIntelligence.Text.Trim(), out intelligence) ||
                !int.TryParse(textBoxAgility.Text.Trim(), out agility) ||
                !int.TryParse(textBoxVigor.Text.Trim(), out vigor) ||
                !int.TryParse(textBoxDurability.Text.Trim(), out durability) ||
                !int.TryParse(textBoxID.Text.Trim(), out id))


            {
                MessageBox.Show("Please fill all the requiremented places!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SkillPoints hesapla
            skillPoints = (age * 3 + level * 3 - strength - intelligence - agility - vigor - durability);
            textBoxSkillPoints.Text = skillPoints.ToString();

            if (skillPoints < 0)
            {
                MessageBox.Show("Skill points cannot be negative number", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Deðerleri güncelle
                this.age = age;
                this.Strength = strength;
                this.Intelligence = intelligence;
                this.Agility = agility;
                this.Vigor = vigor;
                this.Durability = durability;
                this.level = level;
                this.ID = id;
                this.name = textBoxName.Text;
                checkingP = true;
                this.Title=comboBoxTitle.Text;
            }
        }

        private void buttonApplyNormal_Click(object sender, EventArgs e)
        {
            if (checkingP) //this controls if we checked. If there is no problem, then it'll work
            {
                switch (Title)
                {



                    case "Noble": Intelligence += 15; Agility += 3;Durability += 5; Vigor += 30; break;

                    case "Citizen": Intelligence += 10; Agility += 7; Durability += 3; Vigor += 20; break;

                    case "Outcast": Intelligence += 10; Agility += 7; Durability += 3; Vigor += 10; break;

                    case "Peasant": Strength += 10; Agility += 3; Durability += 8; Vigor += 15; break;



                    case "Fighter": Strength += 15; Durability += 5; Vigor += 10; break;

                    case "Warrior": Strength += 20; Durability += 10; Vigor += 12; break;

                    case "Archer": Agility += 20; Durability += 10; Vigor += 12; break;

                    case "Mage": Intelligence += 20; Durability += 8; Vigor += 10; break;

                    case "Bandit": Agility += 15; Vigor += 10; Durability += 5; break;

                    case "Assassin": Agility += 20; Vigor += 5; Durability += 5; break;



                    case "Ki Master": Strength += 45; Vigor += 30; Durability += 50 ; break;

                    case "Knight": Strength += 50; Vigor += 40; Durability += 45; break;

                    case "Sharpshooter": Agility += 50; Vigor += 30; Durability += 25; break;

                    case "Great Mage": Intelligence += 80; Vigor += 60; Durability += 10; break;

                    case "Rogue Lord": Strength += 30; Agility += 50; Vigor += 35; Durability += 20; break;

                    case "Master Assassin": Strength += 25; Agility += 65; Vigor += 50; Durability += 15; break;



                    case "Legendary Fighter": Strength += 100; Vigor += 80; Durability += 90; break;

                    case "Elite Knight": Strength += 110; Vigor += 70; Durability += 100; Agility += 30 ; break;

                    case "Archer GrandMaster": Agility += 100; Vigor += 50; Durability += 40; Strength += 20; Intelligence += 40; break;

                    case "War Mage": Agility += 100; Vigor += 100; Durability += 60; Strength += 100; Intelligence += 150; break;

                    case "Rogue Emperor": Agility += 120; Vigor += 80; Durability += 50; Strength += 100; break;

                    case "Shadow Assassin": Strength += 110; Agility += 150; Vigor += 70; Intelligence += 60; Durability +=50 ; break;



                    case "Deathbringer": Agility += 100; Vigor += 150; Durability += 100; Strength += 150; Intelligence += 500; break;




                }




                int[] multipliersDurability = { 15, 30, 100, 150, 300, 450, 500 };//this is the same thing as HP, multipliers. Adding HP some amount, not as much as vigor, yet it is fair enough.
                int hundredsDurability = Durability / 100;
                int remainderDurability = Durability % 100;

                for (int i = 0; i<hundredsDurability && i < multipliersDurability.Length;i++)
                {
                    HP += multipliersDurability[i] * 100;
                }
                if (hundredsDurability < multipliersDurability.Length)
                {
                    HP += remainderDurability * multipliersDurability[hundredsDurability];
                }
                




                int[] multipliers = { 50, 100, 200, 400, 800, 1600, 3200 }; // I divided levels by hundreds. Each hundred has its very own hp rising.
                int hundreds = Vigor / 100;//I'm taking hundreds as a integer vigor/100, so it will give me exact number that which hundred I'm working
                int remainder = Vigor % 100;//and that is remainder that I need to calculate afterwards. This makes code simple, little complexs. 

                for (int i = 0; i < hundreds && i < multipliers.Length; i++) //even though I haven't wrote any comment thing, I'ma add from this point on, of course there is not specific queue I'm going to make
                {//It is a loop allows that each hundred takes its own hp value.
                    HP += multipliers[i] * 100;
                }

                if (hundreds < multipliers.Length)//and this is for remainder, which we don't calculate while calculating hundreds.
                {
                    HP += remainder * multipliers[hundreds];
                }
                //now I'mma do same thing for MP and SP






                int[] multipliersMP = { 100, 200, 400, 800, 1600, 3200, 6400 }; // I divided levels by hundreds. Each hundred has its very own MP rising.
                int hundredsMP = Intelligence / 100;//I'm taking hundreds as a integer Intelligence/100, so it will give me exact number that which hundred I'm working
                int remainderMP = Intelligence % 100;//and that is remainder that I need to calculate afterwards. This makes code simple, little complexs. 

                for (int i = 0; i < hundredsMP && i < multipliersMP.Length; i++) 
                {//It is a loop allows that each hundred takes its own mp value.
                    MP += multipliersMP[i] * 100;
                }

                if (hundredsMP < multipliersMP.Length)//and this is for remainder, which we don't calculate while calculating hundreds.
                {
                    MP += remainderMP * multipliersMP[hundredsMP];
                }






                int[] multipliersSP = { 1, 2, 3, 4, 5, 6, 7 };
                int hundredsSP = Agility / 100;
                int remainderSP = Agility % 100;

                for (int i = 0; i < hundredsSP && i < multipliersSP.Length; i++)
                {//It is a loop allows that each hundred takes its own sp value.
                    SP += multipliersSP[i] * 100;
                }

                if (hundredsMP < multipliersMP.Length)//and this is for remainder, which we don't calculate while calculating hundreds.
                {
                    SP += remainderSP * multipliersSP[hundredsSP]; // I got bored while I'm adding comment lines, it's going to be held for a while.
                }




                averageLife += Vigor*0.5 + Durability*0.25 ;
                double ClassOp;
                ClassOp = (Strength  + Durability  + Intelligence  + Agility  + Vigor) / 5;
                switch (ClassOp)
                {
                    case double n when (n >= 0 && n <= 100):
                        Class = "F";
                        break;
                    case double n when (n > 100 && n <= 200):
                        Class = "E";
                        break;
                    case double n when (n > 200 && n <= 300):
                        Class = "D";
                        break;
                    case double n when (n > 300 && n <= 400):
                        Class = "C";
                        break;
                    case double n when (n > 400 && n <= 500):
                        Class = "B";
                        break;
                    case double n when (n > 500 && n <= 600):
                        Class = "A";
                        break;
                    case double n when (n > 600 && n <= 700):
                        Class = "S";
                        break;
                    default: Class = "F"; break;

                }
                










                labelID.Text = ID.ToString();
                labelName.Text = name;
                labelAge.Text = age.ToString();
                labelLevel.Text = level.ToString();
                labelSkillPoints.Text = skillPoints.ToString();
                labelStrength.Text = Strength.ToString();
                labelIntelligence.Text = Intelligence.ToString();
                labelAgility.Text = Agility.ToString();
                labelVigor.Text = Vigor.ToString();
                labelDurability.Text = Durability.ToString();
                labelTitle.Text = Title ;
                labelHP.Text = HP.ToString();
                labelMP.Text = MP.ToString();
                labelSP.Text = SP.ToString();
                labelClass.Text = Class;
                labelIntuition.Text = Intuition;
                labelAverageLife.Text= averageLife.ToString();









                textBoxList.Text = 
                    "######################" + Environment.NewLine +
                    "ID: " + labelID.Text + Environment.NewLine +
                    "Level: " + labelLevel.Text + Environment.NewLine +
                    "Name: "  + labelName.Text + Environment.NewLine +
                    "Age: " + labelAge.Text + Environment.NewLine +
                    "Average Life: " + labelAverageLife.Text + Environment.NewLine +
                    "HP: " +labelHP.Text + Environment.NewLine +
                    "MP: " +labelMP.Text +Environment.NewLine +
                    "SP: " +labelSP.Text + Environment.NewLine +
                    "Skill Points: " + labelSkillPoints.Text + Environment.NewLine +
                    "Strength: " + labelStrength.Text + Environment.NewLine +
                    "Intelligence: " + labelIntelligence.Text + Environment.NewLine +
                    "Agility: " + labelAgility.Text +Environment.NewLine +
                    "Vigor: " + labelVigor.Text + Environment.NewLine +
                    "Durability: " + labelDurability.Text + Environment.NewLine +
                    "Intuition: " + labelIntuition.Text +Environment.NewLine +
                    "Class: " + labelClass.Text + Environment.NewLine +
                    "######################";








                ID = 0;
                level = 0;
                name = "";
                age = 0;
                averageLife = 60;
                HP= 0;
                MP= 0;
                SP= 0;
                skillPoints = 0;
                Strength = 0;
                Intelligence = 0;
                Agility = 0;
                Vigor = 0;
                Durability = 0;
                Intuition = "";
                Class = "";
                Title = "";

                checkingP = false;
            }

            else 
            {
                MessageBox.Show("Please check before apply!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            







        }
    }

    public class PersonModel
    {
        public int ID { get; set; }
        public int level { get; set; }
        public String name { get; set; }
        public int age { get; set; }
        public int averageLife { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int SP { get; set; }
        public int skillPoints { get; set; }
        public int Strength { get; set; }

        public int Intelligence { get; set; }

        public int Agility { get; set; }

        public int Vigor { get; set; }

        public int Durability { get; set; }

        public String Intuition { get; set; }

        public String Class { get; set; }

    }
}
