using Events.Business.Base;
using Events.Common;
using Events.Data;
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
        public Task<IEventsAppResult> GetAllCustomers();
        public Task<IEventsAppResult> GetCustomer(int customerId);
        public Task<IEventsAppResult> Create(Customer customer);
        public Task<IEventsAppResult> Delete(int customerId);
        public Task<IEventsAppResult> Update(Customer customer);
    }
    public class CustomerBusiness : ICustomerBusiness
    {
        //    private readonly Net17102215EventsContext _eventsContext;
        private readonly UnitOfWork _unitOfWork;
        public CustomerBusiness()
        {
            //   _eventsContext = eventsContext;
            _unitOfWork = new UnitOfWork();
        }

        public async Task<IEventsAppResult> Delete(int customerId)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
                if (customer != null)
                {
                    var result = await _unitOfWork.CustomerRepository.RemoveAsync(customer);
                    if (result)
                    {
                        return new EventsAppResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new EventsAppResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new EventsAppResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new EventsAppResult(-4, ex.ToString());
            }
        }

        public async Task<IEventsAppResult> Create(Customer customer)
        {
            try
            {
                await _unitOfWork.CustomerRepository.CreateAsync(customer);

                return new EventsAppResult
                {
                    Status = Const.SUCCESS_CREATE_CODE,
                    Message = Const.SUCCESS_CREATE_MSG,
                    Data = customer
                };
            }
            catch (Exception ex)
            {
                return new EventsAppResult
                {
                    Status = Const.ERROR_EXCEPTION,
                    Message = ex.ToString(),
                };
            }
        }


        public async Task<IEventsAppResult> GetCustomer(int customerId)
        {

            try
            {

                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);

                if (customer == null)
                {
                    return new EventsAppResult
                    {
                        Status = Const.WARNING_NO_DATA_CODE,
                        Message = Const.WARNING_NO_DATA__MSG
                    };
                }
                return new EventsAppResult
                {
                    Status = Const.SUCCESS_READ_CODE,
                    Message = Const.SUCCESS_READ_MSG,
                    Data = customer
                };
            }
            catch (Exception ex)
            {
                return new EventsAppResult
                {
                    Status = Const.ERROR_EXCEPTION,
                    Message = ex.ToString(),
                };
            }
        }

        public async Task<IEventsAppResult> GetAllCustomers()
        {
            try
            {
                var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
                if (!customers.Any())
                {
                    return new EventsAppResult
                    {
                        Status = Const.WARNING_NO_DATA_CODE,
                        Message = Const.WARNING_NO_DATA__MSG,
                    };
                }
                return new EventsAppResult()
                {
                    Status = Const.SUCCESS_READ_CODE,
                    Message = Const.SUCCESS_READ_MSG,
                    Data = customers.ToList()
                };
            }
            catch (Exception ex)
            {
                return new EventsAppResult
                {
                    Status = Const.ERROR_EXCEPTION,
                    Message = ex.ToString(),
                };
            }
        }

        public async Task<IEventsAppResult> Update(Customer customer)
        {
            try
            {
                var existCustomer = await _unitOfWork.CustomerRepository.GetByIdAsync(customer.CustomerId);

                if (existCustomer == null)
                {
                    return new EventsAppResult
                    {
                        Status = Const.FAIL_UPDATE_CODE,
                        Message = Const.FAIL_UPDATE_MSG
                    };
                }

                existCustomer.FullName = customer.FullName;
                existCustomer.Email = customer.Email;
                existCustomer.PhoneNumber = customer.PhoneNumber;
                existCustomer.Gender = customer.Gender;
                existCustomer.DateOfBirth = customer.DateOfBirth;

                await _unitOfWork.CustomerRepository.UpdateAsync(existCustomer);

                return new EventsAppResult
                {
                    Status = Const.SUCCESS_UPDATE_CODE,
                    Message = Const.SUCCESS_UPDATE_MSG,
                };
            }
            catch (Exception ex)
            {
                return new EventsAppResult
                {
                    Status = Const.ERROR_EXCEPTION,
                    Message = ex.ToString(),
                };
            }

        }
    }
}
