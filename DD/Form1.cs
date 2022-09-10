namespace DD
{
    public partial class Form1 : Form
    {
        private bool btnCreated = false;
        private Button[,] buttons;
        private int[,] matrix;

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

        private void Turn(object? sender, EventArgs e)
        {
            string[] nameBtn = (sender as Button).Name.Split("_");

            int i = int.Parse(nameBtn[0]);
            int j = int.Parse(nameBtn[1]);

            for (int column = 0; column < j; column++)
            {
                switch (matrix[i, column])
                {
                    case 0:
                        matrix[i, column] = 1;
                        buttons[i, column].Text = "¦";
                        break;
                    default:
                        matrix[i, column] = 0;
                        buttons[i, column].Text = "- -";
                        break;
                }
            }

            for (int column = j+1; column < int.Parse(RowColumn.Text); column++)
            {
                switch (matrix[i, column])
                {
                    case 0:
                        matrix[i, column] = 1;
                        buttons[i, column].Text = "¦";
                        break;
                    default:
                        matrix[i, column] = 0;
                        buttons[i, column].Text = "- -";
                        break;
                }
            }

            for (int row = 0; row < int.Parse(RowColumn.Text); row++)
            {
                switch (matrix[row, j])
                {
                    case 0:
                        matrix[row, j] = 1;
                        buttons[row, j].Text = "¦";
                        break;
                    default:
                        matrix[row, j] = 0;
                        buttons[row, j].Text = "- -";
                        break;
                }
            }
            int sum = 0;
            for (int row = 0; row < int.Parse(RowColumn.Text); row++)
            {
                for (int column = 0; column < int.Parse(RowColumn.Text); column++)
                {
                    sum += matrix[row, column]; 
                }
            }

            if (sum == matrix.Length || sum == 0)
            {
                MessageBox.Show("WIN");
                RemoveBtn();
            }
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

        private void DrawBtn(int n)
        {
            var size = SizeButton(this.ClientSize.Width, this.ClientSize.Height, n);

            buttons = new Button[n, n];

            matrix = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Name = $"{i}_{j}";
                    ButtonPosition(i, j, size.Item1);
                    buttons[i, j].Location = new Point((int)(size.Item1 * i + 10), (int)(size.Item2 * j) + 152);
                    buttons[i, j].Size = new Size((int)size.Item1, (int)size.Item2);
                    buttons[i, j].Click += Turn;
                    Controls.Add(buttons[i, j]);
                }
            }
            btnCreated = true;
        }

        private void ButtonPosition(int i, int j, double size)
        {
            Random random = new Random();

            int rnd = random.Next(2);

            matrix[i, j] = rnd;

            if (matrix[i, j] != 0)
            {
                buttons[i, j].Text = "¦";
                buttons[i, j].Font = new Font("Microsoft Sans Serif", (float)size / 5, FontStyle.Regular);
            } 
            else
            {
                buttons[i, j].Text = "- -";
                buttons[i, j].Font = new Font("Microsoft Sans Serif", (float)size / 5, FontStyle.Regular);
            }
        }

        private (double, double) SizeButton(int width, int height, int n)
        {
            return ((double)width / n - 10, (double)(height - 142) / n - 10);
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
    }
}
