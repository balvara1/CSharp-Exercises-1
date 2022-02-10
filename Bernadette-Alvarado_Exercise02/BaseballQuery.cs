using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bernadette_Alvarado_Exercise02
{
    public partial class BaseballQuery : Form
    {
        public BaseballQuery()
        {
            InitializeComponent();
        }

        // Entity Framework DbContext
        BaseballEntities dbcontext =
        new BaseballEntities();


        // load data from database into DataGridView
        private void BaseballQuery_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'baseballDataSet.Players' table. You can move, or remove it, as needed.
            this.playersTableAdapter.Fill(this.baseballDataSet.Players);

        }

        private void button_battingAve_Click(object sender, EventArgs e)
        {
            // all batting average

            var battingAve =
            from players in dbcontext.Players
                select new { players.FirstName, players.BattingAverage };

            textBox_output.AppendText("List of all Players and Batting Average:");
            textBox_output.AppendText(Environment.NewLine);

            foreach (var element in battingAve)
            {
                textBox_output.AppendText($"\r\nPlayer Name:  {element.FirstName}");
                textBox_output.AppendText(Environment.NewLine);
                textBox_output.AppendText($"\r\nBatting Average:{element.BattingAverage,-5}");
                textBox_output.AppendText(Environment.NewLine);

            }

            // highest batting average

            var battingAveHigh =
            (from players in dbcontext.Players
            orderby players.BattingAverage descending
            select new { players.FirstName, players.BattingAverage }).First();

            textBox_battingAveHigh.AppendText($"\r\nPlayer with Highest Batting Average is:");
            textBox_battingAveHigh.AppendText(Environment.NewLine);
            textBox_battingAveHigh.AppendText($"\r\nPlayer Name:  {battingAveHigh.FirstName}");
            textBox_battingAveHigh.AppendText(Environment.NewLine);
            textBox_battingAveHigh.AppendText($"\r\nBatting Avg:  {battingAveHigh.BattingAverage}");

            // batting average of all players

            var battingAveAll =
                (from players in dbcontext.Players
                 select players.BattingAverage).Average();

            textBox_battingAveAll.AppendText($"\r\nBatting Average of all the players:");
            textBox_battingAveAll.AppendText(Environment.NewLine);
            textBox_battingAveAll.AppendText($"\r\nBatting Average =  {battingAveAll}");
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            textBox_SearchLastName.Clear();
            textBox_SearchPlayerId.Clear();

            var loadall =
                from players in dbcontext.Players
                select new { players.PlayerID, players.FirstName, players.LastName, players.BattingAverage };

            dataGrid_Baseball.DataSource = loadall.ToList();
        }

        private void button_SearchLastName_Click(object sender, EventArgs e)
        {
            String lName;
            lName = textBox_SearchLastName.Text;

            var searchLName =
                from players in dbcontext.Players
                where players.LastName == lName
                select new { players.PlayerID, players.FirstName, players.LastName, players.BattingAverage };

            dataGrid_Baseball.DataSource = searchLName.ToList();

        }

        private void button_SearchPlayerId_Click(object sender, EventArgs e)
        {
            int pId;
            pId = Convert.ToInt32(textBox_SearchPlayerId.Text);

            var searchPId =
                from players in dbcontext.Players
                where players.PlayerID == pId
                select new { players.PlayerID, players.FirstName, players.LastName, players.BattingAverage };

            dataGrid_Baseball.DataSource = searchPId.ToList();

        }
    }
}
