namespace DD
{
    public partial class Form1 : Form
    {
        private bool btnCreated = false;
        private Button[,] buttons;

        public Form1()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (RowColumn.Text == "")
            {
                MessageBox.Show("Заполните размер");
            }
            else
            {
                CreateButton();
            }
        }

        private void RowColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void DrawBtn(int n)
        {
            var size = SizeButton(this.ClientSize.Width, this.ClientSize.Height, n);

            buttons = new Button[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Name = $"Button{i}_{j}";
                    buttons[i, j].TabIndex = 0;
                    buttons[i, j].Location = new System.Drawing.Point((int)(size.Item1 * i + 10), (int)(size.Item2 * j) + 152);
                    buttons[i, j].Size = new System.Drawing.Size((int)size.Item1, (int)size.Item2);
                    buttons[i, j].BackColor = Color.Blue;
                    buttons[i, j].UseVisualStyleBackColor = true;
                    Controls.Add(buttons[i, j]);
                }
            }
            btnCreated = true;
        }

        private void RemoveBtn()
        {
            for (int i = 0; i < Math.Sqrt(buttons.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(buttons.Length); j++)
                {
                    Controls.Remove(buttons[i, j]);
                }
            }
            btnCreated = false;
        }

        private void CreateButton()
        {
            if (!btnCreated)
            {
                DrawBtn(int.Parse(RowColumn.Text));
            }
            else
            {
                RemoveBtn();
                DrawBtn(int.Parse(RowColumn.Text));
            }
        }

        private (double, double) SizeButton(int width, int height, int n)
        {
            return ((double)width / n - 10, (double)(height - 142) / n - 10);
        }
    }
}