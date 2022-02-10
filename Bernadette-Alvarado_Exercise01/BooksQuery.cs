using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bernadette_Alvarado_Exercise01
{
    public partial class BooksQuery : Form
    {
        public BooksQuery()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // Entity Framework DbContext
            var dbcontext = new BooksEntities();

            /*----------------1a-----------------*/

            //  Get a count of all the authors grouped by title, sorted by title.
            //  It should display a title and number of authors who 
            // have written that title. 

            var authorsCount =
               from book in dbcontext.Titles
               orderby book.Title1
               select new { Count = book.Authors.Count(), Title = book.Title1};

            outputTextBox.AppendText("Author count per title:");
            outputTextBox.AppendText(Environment.NewLine);

            foreach (var element in authorsCount)
            {
                outputTextBox.AppendText($"\r\n\tAuthor Count:  {element.Count}");
                outputTextBox.AppendText($"\r\n\t{element.Title,-5}");
                outputTextBox.AppendText(Environment.NewLine);
            }


            /*----------------1b-----------------*/

            // Get a list of all the titles grouped by author name, sorted by author;
            // for a given author name sort the titles 
            //alphabetically.

            var bookGroupAuthor1 = from author in dbcontext.Authors
                                   orderby author.LastName, author.FirstName
                                   select new
                                   {
                                       Name = author.FirstName + " " + author.LastName,
                                       Titles =
                                        from book in author.Titles
                                        orderby book.Title1
                                        select book.Title1
                                   };

            outputTextBox.AppendText(Environment.NewLine);
            outputTextBox.AppendText(Environment.NewLine);
            outputTextBox.AppendText("\n\nTitles grouped by author name:");
            outputTextBox.AppendText(Environment.NewLine);


            foreach (var author in bookGroupAuthor1)
            {
                outputTextBox.AppendText($"\r\n\t{author.Name}:");
                foreach (var title in author.Titles)
                {
                    outputTextBox.AppendText($"\r\n\t{title}");
                }
                outputTextBox.AppendText(Environment.NewLine);
            }
            
        }
    }
}
