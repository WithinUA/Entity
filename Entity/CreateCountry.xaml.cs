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
    /// Interaction logic for CreateCountry.xaml
    /// </summary>
    public partial class CreateCountry : Window
    {
        CountriesContext db;
        Country country;

        //public CreateCountries()
        //{
        //    db = new CountriesContext();
        //    country = new Country();
        //}

        //public CreateCountries(Country country)
        //{
        //    db = new CountriesContext();
        //    this.country = country;
        //}
    }
}
