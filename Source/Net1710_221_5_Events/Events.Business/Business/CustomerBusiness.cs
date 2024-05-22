using Events.Business.Base;
using Events.Common;
using Events.Data.DAO;
using Events.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Business.Business
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
                    return new EventsAppResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    await _DAO.RemoveAsync(customer);
                    return new EventsAppResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new EventsAppResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
            }
        }

        public async Task<IEventsAppResult> Insert(Customer customer)
        {
            try
            {
                await _DAO.CreateAsync(customer);
                return new EventsAppResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, customer);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
        }

        public async Task<IEventsAppResult> Read(int customerId)
        {

            var customer = await _DAO.GetByIdAsync(customerId);
            if (customer != null)
            {
                return new EventsAppResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customer);
            }
            else
            {
                return new EventsAppResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }
        }

        public async Task<IEventsAppResult> ReadAll()
        {
            var customer = await _DAO.GetAllAsync();
            if (customer == null)
            {
                return new EventsAppResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
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
                    return new EventsAppResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
                }

                existingCustomer.FullName = customer.FullName;
                existingCustomer.Email = customer.Email;
                existingCustomer.PhoneNumber = customer.PhoneNumber;
                existingCustomer.Gender = customer.Gender;
                existingCustomer.DateOfBirth = customer.DateOfBirth;

                await _DAO.UpdateAsync(existingCustomer);
                return new EventsAppResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, customer);
            }
            catch (Exception ex)
            {
                return new EventsAppResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }
        }
    }
}
