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
            }
        }

        private void buttonApplyNormal_Click(object sender, EventArgs e)
        {
            if (checkingP)
            {
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
