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
                var currency = await _DAO.GetByIdAsync(customerId);
                if (currency != null)
                {
                    var result = await _DAO.RemoveAsync(currency);
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

        public async Task<IEventsAppResult> Insert(Customer customer)
        {
            try
            {
                //

                int result = await _DAO.CreateAsync(customer);
                if (result > 0)
                {
                    return new EventsAppResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new EventsAppResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new EventsAppResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }


        public async Task<IEventsAppResult> Read(int customerId)
        {

            try
            {
                #region Business rule
                #endregion

                var currency = await _DAO.GetByIdAsync(customerId);

                if (currency == null)
                {
                    return new EventsAppResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new EventsAppResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, currency);
                }
            }
            catch (Exception ex)
            {
                return new EventsAppResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IEventsAppResult> ReadAll()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                var currencies = await _DAO.GetAllAsync();

                if (currencies == null)
                {
                    return new EventsAppResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new EventsAppResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, currencies);
                }
            }
            catch (Exception ex)
            {
                return new EventsAppResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IEventsAppResult> Update(Customer customer)
        {
            try
            {
                int result = await _DAO.UpdateAsync(customer);
                if (result > 0)
                {
                    return new EventsAppResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new EventsAppResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new EventsAppResult(-4, ex.ToString());
            }
        }
    }
}
