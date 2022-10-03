namespace Football_Penalty_Goal_Shootout
{
    public partial class Form1 : Form
    {
        private List<string> keeperPosition = new List<string> { "left", "right", "top", "topLeft", "topRight"};
        private List<PictureBox> goalTarget;
        private int ballX = 0;
        private int ballY = 0;
        private int goal = 0;
        private int miss = 0;
        private string state;
        private string playerTarget;
        private bool aimSet = false;
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            goalTarget = new List<PictureBox> { left, right, top, topLeft, topRight};
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SetGoalTargetEvent(object sender, EventArgs e)
        {
            if (aimSet) {
                return;
            }

            BallTimer.Start();
            KeeperTimer.Start();
            ChangeGoalKeeperImage();

            var senderObject = (PictureBox)sender;
            senderObject.BackColor = Color.Beige;

            switch (senderObject.Tag.ToString()) {
                case "topRight":
                    ballX = -7;
                    ballY = 15;
                    playerTarget = senderObject.Tag.ToString();
                    aimSet = true;
                    break;
                case "right":
                    ballX = -11;
                    ballY = 15;
                    playerTarget = senderObject.Tag.ToString();
                    aimSet = true;
                    break;
                case "top":
                    ballX = 0;
                    ballY = 20;
                    playerTarget = senderObject.Tag.ToString();
                    aimSet = true;
                    break;
                case "topLeft":
                    ballX = 7;
                    ballY = 15;
                    playerTarget = senderObject.Tag.ToString();
                    aimSet = true;
                    break;
                case "left":
                    ballX = 7;
                    ballY = 11;
                    playerTarget = senderObject.Tag.ToString();
                    aimSet = true;
                    break;
                default:
                    break;
            }

            CheckScore();
        }

        private void KeeperTimerEvent(object sender, EventArgs e)
        {
            switch (state) {
                case "left":
                    goalKeeper.Left -= 6;
                    goalKeeper.Top = 284;
                    break;
                case "right":
                    goalKeeper.Left += 6;
                    goalKeeper.Top = 284;
                    break;
                case "top":
                    goalKeeper.Top -= 6;
                    break;
                case "topLeft":
                    goalKeeper.Left -= 6;
                    goalKeeper.Top -= 3;
                    break;
                case "topRight":
                    goalKeeper.Left += 6;
                    goalKeeper.Top -= 3;
                    break;
            }

            foreach (PictureBox x in goalTarget) {
                if (goalKeeper.Bounds.IntersectsWith(x.Bounds)) {
                    KeeperTimer.Stop();
                    goalKeeper.Location = new Point(418, 169);
                    goalKeeper.Image = Properties.Resources.stand_small;
                }
            }
        }

        private void BallTimerEvent(object sender, EventArgs e)
        {
            football.Left -= ballX;
            football.Top -= ballY;

            foreach (PictureBox x in goalTarget) {
                if (football.Bounds.IntersectsWith(x.Bounds)) {
                    football.Location = new Point(430, 500);
                    ballX = 0;
                    ballY = 0;
                    aimSet = false;
                    BallTimer.Stop();
                }
            }
        }

        private void CheckScore() {
            if (state == playerTarget) {
                miss++;
                lblMissed.Text = "Missed: " + miss;
            }
            else {
                goal++;
                lblScore.Text = "Scored: " + goal;
            }
        }

        private void ChangeGoalKeeperImage() {
            KeeperTimer.Start();
            int i = random.Next(0, keeperPosition.Count);
            state = keeperPosition[i];
            
            switch (i) {
                case 0: 
                    goalKeeper.Image = Properties.Resources.left_save_small;
                    break;
                case 1:
                    goalKeeper.Image = Properties.Resources.right_save_small;
                    break;
                case 2:
                    goalKeeper.Image = Properties.Resources.top_save_small;
                    break;
                case 3:
                    goalKeeper.Image = Properties.Resources.top_left_save_small;
                    break;
                case 4:
                    goalKeeper.Image = Properties.Resources.top_right_save_small;
                    break;
            }
        }
    }
}