//using eFastBanking.Business.Base;
//using eFastBanking.Data.Models;
//using eFastBanking.Data.DAO;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using eFastBanking.Common;
//using Events.Common;

//namespace eFastBanking.Business.Category
//{
//    public interface ICurrencyBusiness
//    {
//        Task<IBusinessResult> GetAll();
//        Task<IBusinessResult> GetById(string code);
//        Task<IBusinessResult> Save(Currency currency);
//        Task<IBusinessResult> Update(Currency currency);
//        Task<IBusinessResult> DeleteById(string code);
//    }
//    public class CurrencyBusiness : ICurrencyBusiness
//    {
//        private readonly CurrencyDAO _DAO;

//        public CurrencyBusiness()
//        {
//            _DAO = new CurrencyDAO();
//        }

//        public async Task<IBusinessResult> GetAll()
//        {
//            try
//            {
//                #region Business rule
//                #endregion

//                //var currencies = _DAO.GetAll();
//                var currencies = await _DAO.GetAllAsync();

//                if (currencies == null)
//                {
//                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
//                }
//                else
//                {
//                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, currencies);
//                }
//            }
//            catch (Exception ex)
//            {
//                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
//            }
//        }

//        public async Task<IBusinessResult> GetById(string code)
//        {
//            try
//            {
//                #region Business rule
//                #endregion

//                var currency = await _DAO.GetByIdAsync(code);

//                if (currency == null)
//                {
//                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
//                }
//                else
//                {
//                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, currency);
//                }
//            }
//            catch (Exception ex)
//            {
//                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
//            }
//        }

//        public async Task<IBusinessResult> Save(Currency currency)
//        {
//            try
//            {
//                //

//                int result = await _DAO.CreateAsync(currency);
//                if (result > 0)
//                {
//                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
//                }
//                else
//                {
//                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
//                }
//            }
//            catch (Exception ex)
//            {
//                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
//            }
//        }

//        public async Task<IBusinessResult> Update(Currency currency)
//        {
//            try
//            {
//                int result = await _DAO.UpdateAsync(currency);
//                if (result > 0)
//                {
//                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
//                }
//                else
//                {
//                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
//                }
//            }
//            catch (Exception ex)
//            {
//                return new BusinessResult(-4, ex.ToString());
//            }
//        }

//        public async Task<IBusinessResult> DeleteById(string code)
//        {
//            try
//            {
//                var currency = await _DAO.GetByIdAsync(code);
//                if (currency != null)
//                {
//                    var result = await _DAO.RemoveAsync(currency);
//                    if (result)
//                    {
//                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
//                    }
//                    else
//                    {
//                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
//                    }
//                }
//                else
//                {
//                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
//                }
//            }
//            catch (Exception ex)
//            {
//                return new BusinessResult(-4, ex.ToString());
//            }
//        }

//    }
//}