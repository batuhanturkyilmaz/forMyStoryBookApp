using System.Data.SQLite;

namespace forMyStoryBookApp
{
    public partial class Form1 : Form
    {

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
            //WireUpPeopleList(); 
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
