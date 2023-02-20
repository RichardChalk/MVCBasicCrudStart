using ClassLibrary.Data;
using ClassLibrary.DTOs;
using ClassLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Tenta.Models.Customer;

namespace Tenta.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ApplicationDbContext _context;

        public CustomerController(ICustomerService customerService, ApplicationDbContext context)
        {
            _customerService = customerService;
            _context = context;
        }

        // READ ALL -  READ ALL - READ ALL - READ ALL - READ ALL - READ ALL - READ ALL -
        // READ ALL -  READ ALL - READ ALL - READ ALL - READ ALL - READ ALL - READ ALL -
        // READ ALL -  READ ALL - READ ALL - READ ALL - READ ALL - READ ALL - READ ALL -
        // READ ALL -  READ ALL - READ ALL - READ ALL - READ ALL - READ ALL - READ ALL -
        public IActionResult Customers(string q)
        
        {
            var customersVM = new CustomersVM();
            customersVM.Customers = _customerService.GetAllCustomers(q);
            customersVM.Countries = _customerService.FillCountryDropDown();

            return View(customersVM);
        }

        // CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - 
        // CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - 
        // CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - 
        // CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - CREATE - 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Customers(CustomersVM customersVM)
        {
            if (ModelState.IsValid)
            {
                _customerService.CreateCustomer(customersVM.CustomerCreateDTO);

                // Used for Toastr notifications
                TempData["success"] = "Customer created successfully";

                _context.SaveChanges();
                return RedirectToAction("Customers", "Customer");
            }

            customersVM.Customers = _customerService.GetAllCustomers(customersVM.q);
            customersVM.Countries = _customerService.FillCountryDropDown();

            return View(customersVM);
        }
    }
}
