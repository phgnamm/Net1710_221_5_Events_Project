using Events.Common;
using Events.Data.DAO;
using Events.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Business
{
    public interface ICustomerBusiness
    {
        Task<IEventsAppResult> Insert(Customer customer);
        Task<IEventsAppResult> Delete(int customerId);
        Task<IEventsAppResult> Read(int customerId);
        Task<IEventsAppResult> ReadAll();
        Task<IEventsAppResult> Update(Customer customer);
    }
    public class CustomerBusiness : ICustomerBusiness
    {
        //    private readonly Net17102215EventsContext _eventsContext;
        private readonly CustomerDAO _DAO;
        public CustomerBusiness()
        {
            //   _eventsContext = eventsContext;
            _DAO = new CustomerDAO();
        }

        public async Task<IEventsAppResult> Delete(int customerId)
        {
            try
            {
                var customer = await _DAO.GetByIdAsync(customerId);
                if (customer == null)
                {
                    return new EventsAppResult(-1, "Customer not found", null);
                }
                else
                {
                    //   _eventsContext.Remove(customer);
                    //   await _eventsContext.SaveChangesAsync();
                    await _DAO.RemoveAsync(customer);
                    return new EventsAppResult(1, "Delete customer successfully");
                }
            }
            catch (Exception ex)
            {
                return new EventsAppResult(-1, $"Error delete customer detail: {ex.Message}");
            }
        }

        public async Task<IEventsAppResult> Insert(Customer customer)
        {
            try
            {
                await _DAO.CreateAsync(customer);
                //   await _eventsContext.SaveChangesAsync();
                return new EventsAppResult(0, "Order detail create successfully", customer);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(-1, $"Error inserting order detail: {ex.Message}");
            }
        }

        public async Task<IEventsAppResult> Read(int customerId)
        {
            try
            {
                var customer = await _DAO.GetByIdAsync(customerId);
                if (customer == null)
                {

                }
                return new EventsAppResult(0, "Customer retrieved successfully", customer);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(-1, $"Error retrived successfully {ex.Message}");
            }
        }

        public async Task<IEventsAppResult> ReadAll()
        {
            var customer = await _DAO.GetAllAsync();
            if (customer == null)
            {
                return new EventsAppResult(Const.WARNING_NO_DATA_CODE, Const.FAIL_READ_MSG);
            }
            else
            {
                return new EventsAppResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customer);
            }
        }

        public async Task<IEventsAppResult> Update(Customer customer)
        {
            try
            {
                var existingCustomer = await _DAO.GetByIdAsync(customer.CustomerId);

                if (existingCustomer == null)
                {
                    return new EventsAppResult(-1, "Customer not found");
                }

                existingCustomer.FullName = customer.FullName;
                existingCustomer.Email = customer.Email;
                existingCustomer.PhoneNumber = customer.PhoneNumber;
                existingCustomer.Gender = customer.Gender;
                existingCustomer.DateOfBirth = customer.DateOfBirth;

                await _DAO.UpdateAsync(existingCustomer);
                return new EventsAppResult(0, "Update customer successfully", customer);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(-1, $"Error updating customer: {ex.Message}");
            }
        }
    }
}
