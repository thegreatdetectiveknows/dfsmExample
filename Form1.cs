using System.Windows.Forms;

namespace Practice
{
    public partial class Form1 : Form
    {
        string currentState; // Состояние ДКА
        DeterministicFSM dFSM; // ДКА

        public Form1()
        {
            InitializeComponent();
            addRows();

            var Q = new List<string> { "H", "A", "B", "C", "D", "S1", "S2", "S3", "S4" }; // все состояния
            var Sigma = new List<char> { '0', '1' }; // алфавит
            var Delta = new List<Transition>{ // переходы
            new Transition("H", '0', "A"),
            new Transition("A", '0', "B"),
            new Transition("B", '0', "C"),
            new Transition("C", '0', "S1"),
            new Transition("D", '0', "D"),
            new Transition("S1", '0', "S2"),
            new Transition("S2", '0', "S3"),
            new Transition("S3", '0', "S4"),
            new Transition("S4", '0', "S1"),
            new Transition("H", '1', "A"),
            new Transition("A", '1', "B"),
            new Transition("B", '1', "C"),
            new Transition("C", '1', "D"),
            new Transition("D", '1', "D"),
            new Transition("S1", '1', "S2"),
            new Transition("S2", '1', "S3"),
            new Transition("S3", '1', "S4"),
            new Transition("S4", '1', "D"),
         };
            var Q0 = "H"; // начальное состояние
            var F = new List<string> { "S1", "S2", "S3", "S4" }; // конечные состояния

            dFSM = new DeterministicFSM(Q, Sigma, Delta, Q0, F); // ДКА

            currentState = Q0;
            textBox2.Text = $"Начальное состояние: {currentState}.";
            panel2.BackColor = SystemColors.ControlDark;

            clearColor();
            timer1.Start();
        }

        // Заполнение таблицы
        private void addRows()
        {
            dataGridView1.Rows.Add("H", "A", "A", "0");
            dataGridView1.Rows.Add("A", "B", "B", "0");
            dataGridView1.Rows.Add("B", "C", "C", "0");
            dataGridView1.Rows.Add("C", "S1", "D", "0");
            dataGridView1.Rows.Add("D", "D", "D", "0");
            dataGridView1.Rows.Add("S1", "S2", "S2", "1");
            dataGridView1.Rows.Add("S2", "S3", "S3", "1");
            dataGridView1.Rows.Add("S3", "S4", "S4", "1");
            dataGridView1.Rows.Add("S4", "S1", "D", "1");
        }

        // Разрешать ввод только 1 и 0. Обработчик нажатия Enter.
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
                e.Handled = true;
            }
            else if (e.KeyChar != '0' && e.KeyChar != '1' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // timer1.Stop();
            clearColor();
            try
            {
                currentState = dFSM.Accepts(textBox1.Text);
            }
            catch (Exception ex)
            {
                textBox2.Text = "Ошибка! В цепочке обнаружены символы не из входного алфавита.";
                panel2.BackColor = Color.Black;
                currentState = String.Empty;
                return;
            }
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox2.Text = $"Начальное состояние: {currentState}.";
                panel2.BackColor = SystemColors.ControlDark;
            }
            else if (dFSM.Contains(currentState))
            {
                textBox2.Text = $"Допускающее состояние: {currentState}.";
                panel2.BackColor = Color.SeaGreen;
            }
            else
            {
                textBox2.Text = $"Недопускающее состояние: {currentState}.";
                panel2.BackColor = Color.IndianRed;
            }
            // timer1.Start();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridView1.ClearSelection();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value as String == currentState)
                {
                    if (row.DefaultCellStyle.BackColor == Color.Gainsboro)
                        row.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    else row.DefaultCellStyle.BackColor = Color.Gainsboro;
                }
            }
        }

        // Изменение цвета всех строк в таблице на обычный
        private void clearColor()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                row.DefaultCellStyle.BackColor = Color.WhiteSmoke;
        }
    }
}
