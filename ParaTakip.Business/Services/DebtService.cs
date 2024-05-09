using MongoDB.Bson;
using ParaTakip.Business.Base;
using ParaTakip.Business.Helpers;
using ParaTakip.Business.Interfaces;
using ParaTakip.Common;
using ParaTakip.Core;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.Entities;
using ParaTakip.Model.RequestModel;
using ParaTakip.Model.ResponseModel;
using static ParaTakip.Entities.AppUser;
using static ParaTakip.Entities.Wealth;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParaTakip.Business.Services
{
    public class DebtService : BaseService<Debt, IDebtDataAccess>, IDebtService
    {
        public Debt? GetByUserId(string authenticatedUserId)
        {
            return FilterBy(x => x.AppUserId == ObjectId.Parse(authenticatedUserId)).FirstOrDefault();
        }

        public List<GetEventsResponseModel> GetDebtEvents(string authenticatedUserId, DateTime dateTime)
        {
            List<GetEventsResponseModel> result = new List<GetEventsResponseModel>();
            var debt = this.GetByUserId(authenticatedUserId);
            foreach (var debtInfo in debt.Values)
            {
                result.Add(new GetEventsResponseModel
                {
                    EventType = EventType.LoanDebt,
                    StartDate = debtInfo.Date,
                    EndDate = debtInfo.Date,
                    AllDay = true,
                    EventData = debtInfo,
                    Title = $"{debtInfo.BankName} - {debtInfo.TotalAmount} TL, 1. taksit kredi borcu."
                });

                for (global::System.Int32 i = 2; i <= debtInfo.InstallmentCount; i++)
                {
                    result.Add(new GetEventsResponseModel
                    {
                        EventType = EventType.LoanDebt,
                        StartDate = debtInfo.Date.AddMonths(i-1),
                        EndDate = debtInfo.Date.AddMonths(i-1),
                        AllDay = true,
                        EventData = debtInfo,
                        Title = $"{debtInfo.BankName} - {debtInfo.TotalAmount} TL, {i}. taksit kredi borcu."
                    });
                }
            }

            return result;
        }

        public Debt? Update(List<DebtInfo> model, string authenticatedUserId)
        {
            Debt response;

            var userDebt = AppServiceProvider.Instance.Get<IDebtService>().GetByUserId(authenticatedUserId);
            if (userDebt == null)
            {
                userDebt = new Debt()
                {
                    AppUserId = MongoDB.Bson.ObjectId.Parse(authenticatedUserId)
                };

                userDebt.Values = model;
                response = this.Create(userDebt);
            }
            else
            {
                userDebt.Values = model;
                response = this.Update(userDebt);
            }

            return response;
        }

    }
}
