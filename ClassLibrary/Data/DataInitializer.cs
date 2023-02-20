using ClassLibrary.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Data
{
    public class DataInitializer
    {
        private readonly ICustomerService _customerService;
        private readonly ApplicationDbContext _context;

        public DataInitializer(ICustomerService customerService, ApplicationDbContext context)
        {
            _customerService = customerService;
            _context = context;
        }

        public void SeedData()
        {
            _context.Database.Migrate();
            SeedCountries();
        }

        private void SeedCountries()
        {
            CountryDoesNotExist("Sweden");
            CountryDoesNotExist("Norway");
            CountryDoesNotExist("Denmark");
            CountryDoesNotExist("Finland");
        }

        private void CountryDoesNotExist(string countryLabel)
        {
            // Check to see if country already exists in db
            if (_context.Countries.Any(c => c.CountryLabel == countryLabel)) return;
            
            _context.Countries
                .Add(new Country
                {
                    CountryLabel = countryLabel
                });
            _context.SaveChanges();
        }
    }
}
