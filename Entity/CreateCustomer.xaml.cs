using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CreateCustomer.xaml
    /// </summary>
    public partial class CreateCustomer : Window
    {
        CountriesContext db;
        Customer customer;
        public CreateCustomer()
        {
            InitializeComponent();
            db = new();
            customer = new();
            edit_button.IsEnabled = false;
        }

        public CreateCustomer(Customer customer)
        {
            InitializeComponent();
            db = new();
            this.customer = customer;
            add_button.IsEnabled = false;
            firstname_textbox.Text = customer.FirstName.Trim();
            lastname_textbox.Text = customer.LastName.Trim();
            email_textbox.Text = customer.Email.Trim();
            gender_textbox.Text = customer.Gender.Trim();
            birthdate_picker.Text = customer.BirthDate.ToString();
            town_textbox.Text = db.Cities.FirstOrDefault(x => x.Id == customer.TownId).Name.Trim();
        }

        private void add_event(object sender, RoutedEventArgs e)
        {
            try
            {
                bool flag = true;
                if (string.IsNullOrEmpty(firstname_textbox.Text))
                {
                    MessageBox.Show("First name must be declare");
                    flag = false;
                }
                else
                    customer.FirstName = firstname_textbox.Text;

                if (string.IsNullOrEmpty(lastname_textbox.Text))
                {
                    MessageBox.Show("Last name must be declare");
                    flag = false;
                }
                else
                    customer.LastName = lastname_textbox.Text;

                string pattern = @"^[a-zA-Z0-9_]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,5}$";
                if (Regex.IsMatch(email_textbox.Text, pattern))
                    customer.Email = email_textbox.Text;
                else
                {
                    MessageBox.Show("Email doesn`t match to the template");
                    flag = false;
                }

                if (gender_textbox.Text != "Male" && gender_textbox.Text != "Female")
                {
                    MessageBox.Show("Gender must be Male or Female");
                    flag = false;
                }
                else
                    customer.Gender = gender_textbox.Text;

                if (birthdate_picker.SelectedDate == null)
                {
                    MessageBox.Show("Select the birthdate");
                    flag = false;
                }
                else
                    customer.BirthDate = (DateTime)birthdate_picker.SelectedDate;

                var town = db.Cities.FirstOrDefault(x => x.Name.Trim() == town_textbox.Text);

                if (town != null)
                {
                    customer.TownId = town.Id;
                    customer.CountryId = town.CountryId;
                }
                else
                {
                    MessageBox.Show("Town must be one of the list below");
                    flag = false;
                }

                if (flag)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    MessageBox.Show("The customer was succefully added");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void show_cities_ev(object sender, RoutedEventArgs e)
        {
            try
            {
                var el = db.Cities.Select(x => x.Name.Trim() + ", ");
                string str = "";
                foreach( var item in el)
                {
                    str += item;
                }
                MessageBox.Show(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void edit_event(object sender, RoutedEventArgs e)
        {
            try
            {
                bool flag = true;
                if (string.IsNullOrEmpty(firstname_textbox.Text))
                {
                    MessageBox.Show("First name must be declare");
                    flag = false;
                }
                else
                    customer.FirstName = firstname_textbox.Text;

                if (string.IsNullOrEmpty(lastname_textbox.Text))
                {
                    MessageBox.Show("Last name must be declare");
                    flag = false;
                }
                else
                    customer.LastName = lastname_textbox.Text;

                string pattern = @"^[a-zA-Z0-9_]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,5}$";
                if (Regex.IsMatch(email_textbox.Text, pattern))
                    customer.Email = email_textbox.Text;
                else
                {
                    MessageBox.Show("Email doesn`t match to the template");
                    flag = false;
                }

                if (gender_textbox.Text != "Male" && gender_textbox.Text != "Female")
                {
                    MessageBox.Show("Gender must be Male or Female");
                    flag = false;
                }
                else
                    customer.Gender = gender_textbox.Text;

                if (birthdate_picker.SelectedDate == null)
                {
                    MessageBox.Show("Select the birthdate");
                    flag = false;
                }
                else
                    customer.BirthDate = (DateTime)birthdate_picker.SelectedDate;

                var town = db.Cities.FirstOrDefault(x => x.Name.Trim() == town_textbox.Text);

                if (town != null)
                {
                    customer.TownId = town.Id;
                    customer.CountryId = town.CountryId;
                }
                else
                {
                    MessageBox.Show("Town must be one of the list below");
                    flag = false;
                }

                if (flag)
                {

                    var el = db.Customers.FirstOrDefault(x => x.Id == customer.Id);
                    if (el != null)
                    {
                        el.Id = customer.Id;
                        el.FirstName = customer.FirstName;
                        el.LastName = customer.LastName;
                        el.Gender = customer.Gender;
                        el.BirthDate = customer.BirthDate;
                        el.Email = customer.Email;
                        el.TownId = customer.TownId;
                        el.CountryId = customer.CountryId;
                    }

                    db.SaveChanges();
                    MessageBox.Show("The customer was succefully edited");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
