using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Entity
{
    /// <summary>
    /// Interaction logic for NewCountry.xaml
    /// </summary>
    public partial class NewCountry : Window
    {
        CountriesContext db;
        Country country;
        public NewCountry()
        {
            InitializeComponent();
            db = new();
            country = new Country();
            edit_button.IsEnabled = false;
        }

        public NewCountry(Country country)
        {
            InitializeComponent();
            this.country = country;
            name_textbox.Text = country.Name.Trim();
            square_textbox.Text = country.Square.ToString();
            int id = (int)db.Countries.FirstOrDefault(x => x.Name == country.Name).WorldSideId;
            worldside_textbox.Text = db.WorldSides.FirstOrDefault(x => x.Id == id).Name.Trim();
            add_button.IsEnabled = false;
        }

        private void show_sides_ev(object sender, RoutedEventArgs e)
        {
            try
            {
                var el = db.WorldSides.Select(x => x.Name.Trim() + ", ");
                string str = "";
                foreach (var item in el)
                {
                    str += item;
                }
                MessageBox.Show(str);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); 
            }
        }

        private void add_event(object sender, RoutedEventArgs e)
        {
            try
            {
                bool flag = true;
                if (string.IsNullOrEmpty(name_textbox.Text))
                {
                    MessageBox.Show("The country name must be declare");
                    flag = false;
                }
                else
                    country.Name = name_textbox.Text;

                double square = double.TryParse(square_textbox.Text, out double x) ? x : -1;

                if (square < 0)
                {
                    MessageBox.Show("Square is in wrong format");
                    flag = false;
                }
                else
                    country.Square = square;
                
                string side = worldside_textbox.Text;

                var el = db.WorldSides.FirstOrDefault(x => x.Name == side);

                if (el == null)
                {
                    MessageBox.Show("World Side should be one of the list below");
                    flag = false;
                }
                else
                    country.WorldSideId = el.Id;

                if (flag)
                {
                    db.Countries.Add(country);
                    db.SaveChanges();
                    MessageBox.Show("The country was added succesfully");
                    Close();
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
