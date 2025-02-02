using System.Data.SQLite;

namespace forMyStoryBookApp
{
    public partial class Form1 : Form
    {
        public int ID = 0;
        public int level = 0;
        public String name;
        public int age = 0;
        public int averageLife = 60;
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
                int hundredsSP = SP / 100;
                int remainderSP = SP % 100;

                for (int i = 0; i < hundredsSP && i < multipliersSP.Length; i++)
                {//It is a loop allows that each hundred takes its own sp value.
                    SP += multipliersSP[i] * 100;
                }

                if (hundredsMP < multipliersMP.Length)//and this is for remainder, which we don't calculate while calculating hundreds.
                {
                    SP += remainderSP * multipliersSP[hundredsSP];
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
                checkingP=false;
                labelTitle.Text = Title ;
                labelHP.Text = HP.ToString();
                labelMP.Text = MP.ToString();
                labelSP.Text = SP.ToString();

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
        public string name { get; set; }
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

        public int Intuition { get; set; }

        public int cClass { get; set; }

    }
}
