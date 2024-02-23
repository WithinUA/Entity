using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Entity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CountriesContext db;
        DataTable dt = null;
        public MainWindow()
        {
            InitializeComponent();
            db = new CountriesContext();
        }

        private void customers_ev(object sender, RoutedEventArgs e)
        {
            try
            {
                var dt = db.Customers.Select(x => x).ToList();
                grid_view.ItemsSource = null;
                grid_view.ItemsSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void emails_ev(object sender, RoutedEventArgs e)
        {
            try
            {
                var dt = db.Customers.Select(x => new {x.FirstName, x.LastName, x.Email }).ToList();
                grid_view.ItemsSource = null;
                grid_view.ItemsSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sections_ev(object sender, RoutedEventArgs e)
        {
            try
            {
                var dt = db.Sections.Select(x => new {x.TypeName}).ToList(); 
                grid_view.ItemsSource = null;
                grid_view.ItemsSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void promotional_ev(object sender, RoutedEventArgs e)
        {
            try
            {
                List<object> list = new();
                var dt = db.Sells.Join(db.Countries, sells => sells.CountryId, countries => countries.Id, (sells, countries) => new { sells, countries }).Join(db.Goods, x => x.sells.GoodId, y => y.Id, (x, y) => new { x, y }).GroupBy(x => x.y.Name);
                foreach ( var el in dt)
                {
                    foreach(var ell in el)
                    {
                        var selldata = new
                        {
                            GoodName = ell.y.Name,
                            CountryName = ell.x.countries.Name,
                            StartDate = ell.x.sells.StartDate,
                            EndDate = ell.x.sells.EndDate,
                            SellPercent = ell.x.sells.SellPercent
                        };
                        list.Add(selldata);
                    }
                }
                grid_view.ItemsSource = null;
                grid_view.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void towns_ev(object sender, RoutedEventArgs e)
        {
            try
            {
                var dt = db.Customers.Join(db.Cities, cust => cust.TownId, city => city.Id, (cust, city) => new { cust, city }).GroupBy(x => x.city.Name).Select(x => new { TownName = x.Key, CustomresCount = x.Count() }).ToList();
                grid_view.ItemsSource = null;
                grid_view.ItemsSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void countries_ev(object sender, RoutedEventArgs e)
        {
            try
            {
                var dt = db.Customers.Join(db.Countries, cust => cust.CountryId, city => city.Id, (cust, city) => new { cust, city }).GroupBy(x => x.city.Name).Select(x => new { CountryName = x.Key, CustomresCount = x.Count() }).ToList();
                grid_view.ItemsSource = null;
                grid_view.ItemsSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sides_ev(object sender, RoutedEventArgs e)
        {
            try
            {
                var dt = db.Customers.Join(db.Countries, cost => cost.CountryId, cntr => cntr.Id, (cost, cntr) => new { cost, cntr })
                    .Join(db.WorldSides, twotab => twotab.cntr.WorldSideId, sides => sides.Id, (twotab, sides) => new { twotab, sides })
                    .GroupBy(g => g.sides.Name)
                    .Select(s => new { SideName = s.Key, CostumersCount = s.Count()})
                    .ToList();
                grid_view.ItemsSource = null;
                grid_view.ItemsSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void spec_city_ev(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(spec_city_textbox.Text))
                {
                    MessageBox.Show("Input some city name first!");
                    return;
                }
                var city = db.Cities.FirstOrDefault(x => x.Name == spec_city_textbox.Text);
                if (city == null)
                {
                    MessageBox.Show("There is no such city in DB!");
                    return;
                }
                var cost = db.Customers.Where(x => x.TownId == city.Id).Select(x => new {x.FirstName, x.LastName }).ToList();
                grid_view.ItemsSource = null;
                grid_view.ItemsSource = cost;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void spec_country_ev(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(spec_country_textbox.Text))
                {
                    MessageBox.Show("Input some country name first!");
                    return;
                }
                var country = db.Countries.FirstOrDefault(x => x.Name == spec_country_textbox.Text);
                if (country == null)
                {
                    MessageBox.Show("There is no such country in DB!");
                    return;
                }
                var cost = db.Customers.Where(x => x.CountryId == country.Id).Select(x => new { x.FirstName, x.LastName }).ToList();
                grid_view.ItemsSource = null;
                grid_view.ItemsSource = cost;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void customer_create_ev(object sender, RoutedEventArgs e)
        {
            grid_view.DataContext = null;
            grid_view.ItemsSource = db.Customers.ToList();
            add_customer_button.IsEnabled = true;
            edit_customer_button.IsEnabled = true;
            delete_customer_button.IsEnabled = true;
        }

        private void customer_add_ev(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateCustomer newwindow = new();
                newwindow.ShowDialog();
                grid_view.DataContext = null;
                grid_view.ItemsSource = db.Customers.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void customer_edit_ev(object sender, RoutedEventArgs e)
        {
            try
            {
                var el = grid_view.SelectedItem as Customer;
                if (el != null)
                {
                    CreateCustomer newwindow = new(el);
                    newwindow.ShowDialog();
                    grid_view.DataContext = null;
                    grid_view.ItemsSource = db.Customers.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void customer_del_ev(object sender, RoutedEventArgs e)
        {
            try
            {
                var el = grid_view.SelectedItem as Customer;
                if (el != null)
                {
                    if (MessageBox.Show("Sure you want to delete?", "Delete Item", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var item = db.Customers.FirstOrDefault(x => x.Id == el.Id);
                        if (item != null)
                        {
                            db.Customers.Remove(item);
                            db.SaveChanges();
                        }
                    }
                    grid_view.DataContext = null;
                    grid_view.ItemsSource = db.Customers.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void countries_list__ev(object sender, RoutedEventArgs e)
        {

        }
    }
}