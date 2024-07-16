using Events.Business.Base;
using Events.Common;
using Events.Data;
using Events.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Business.Business
{
    public interface ICompanyBusiness
    {
        public Task<IEventsAppResult> GetAll();
        public Task<IEventsAppResult> GetById(int id);
        public Task<IEventsAppResult> Create(Company company);
        public Task<IEventsAppResult> Delete(int id);
        public Task<IEventsAppResult> Update(Company company);
    }
    public class CompanyBusiness : ICompanyBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public CompanyBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }
        public async Task<IEventsAppResult> Delete(int id)
        {
            try
            {
                var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
                if (company != null)
                {
                    var result = await _unitOfWork.CompanyRepository.RemoveAsync(company);
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
        public async Task<IEventsAppResult> Create(Company company)
        {
            try
            {
                var d = DateTime.Now;
                company.CreatedDate = d;
                company.UpdatedDate = d;
                await _unitOfWork.CompanyRepository.CreateAsync(company);

                return new EventsAppResult
                {
                    Status = Const.SUCCESS_CREATE_CODE,
                    Message = Const.SUCCESS_CREATE_MSG,
                    Data = company
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
        public async Task<IEventsAppResult> GetById(int id)
        {

            try
            {
                var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);

                if (company == null)
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
                    Data = company
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
        public async Task<IEventsAppResult> GetAll()
        {
            try
            {
                var companys = await _unitOfWork.CompanyRepository.GetAllAsync();
                if (!companys.Any())
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
                    Data = companys.ToList()
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
        public async Task<IEventsAppResult> Update(Company company)
        {
            try
            {
                var existCompany = await _unitOfWork.CompanyRepository.GetByIdAsync(company.CompanyId);

                if (existCompany == null)
                {
                    return new EventsAppResult
                    {
                        Status = Const.FAIL_UPDATE_CODE,
                        Message = Const.FAIL_UPDATE_MSG
                    };
                }

                if (!string.IsNullOrWhiteSpace(company.Name))
                {
                    existCompany.Name = company.Name;
                }
                if (!string.IsNullOrWhiteSpace(company.CompanyPhone))
                {
                    existCompany.CompanyPhone = company.CompanyPhone;
                }
                if (!string.IsNullOrWhiteSpace(company.BusinessSector))
                {
                    existCompany.BusinessSector = company.BusinessSector;
                }
                if (!string.IsNullOrWhiteSpace(company.TaxesId))
                {
                    existCompany.TaxesId = company.TaxesId;
                }
                if (!string.IsNullOrWhiteSpace(company.Address))
                {
                    existCompany.Address = company.Address;
                }
                if (!string.IsNullOrWhiteSpace(company.City))
                {
                    existCompany.City = company.City;
                }
                if (!string.IsNullOrWhiteSpace(company.Country))
                {
                    existCompany.Country = company.Country;
                }

                existCompany.UpdatedDate = DateTime.Now;

                await _unitOfWork.CompanyRepository.UpdateAsync(existCompany);

                return new EventsAppResult
                {
                    Status = Const.SUCCESS_UPDATE_CODE,
                    Message = Const.SUCCESS_UPDATE_MSG,
                    Data = existCompany
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
