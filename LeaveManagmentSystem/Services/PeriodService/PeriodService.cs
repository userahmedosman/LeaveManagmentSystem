using AutoMapper;
using LeaveManagmentSystem.Models;
using LeaveManagmentSystem.ViewModels.LeaveTypes;
using LeaveManagmentSystem.ViewModels.Periods;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagmentSystem.Services.PeriodService
{
    public class PeriodService : IPeriodService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PeriodService(ApplicationDbContext dbContext, IMapper mapper) {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<List<PeriodReadVM>> GetAllPeriodsAsync()
        {
            var periods = await _dbContext.Periods.ToListAsync();
            return _mapper.Map<List<PeriodReadVM>>(periods);
        }

        public async Task<T> GetPeriodByIdAysnc<T>(int id) where T : class
        {
            var period = await _dbContext.Periods.FirstOrDefaultAsync(x => x.Id == id);

            var periodVM = _mapper.Map<T>(period);

            return periodVM;
        }
        public async Task<bool> CheckPeriodIsInitiatedAsync()
        {
            var result = await _dbContext.Periods.AnyAsync(x => x.EndDate.Year == DateTime.Now.Year);
           
            return result;
        }

     

        public bool CheckPeriodToCreateIsValid(PeriodCreateVM period)
        {
            var date = period.StartDate.Day == 01 && period.EndDate.Day == 31;
            var month = period.StartDate.Month == 01 && period.EndDate.Month == 12;
            var year = period.StartDate.Year == DateTime.Now.Year && period.EndDate.Year == DateTime.Now.Year;

            return (date && month && year);
           
        }

        public async Task CreateAsync(PeriodCreateVM createVm)
        {
            if (createVm == null) {
                throw new Exception("Period creation faild due to null period data provided");
            }

            var period = _mapper.Map<Period>(createVm);
            _dbContext.Periods.Add(period);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditPeriodNameAsync(PeriodEditVM editVM)
        {
         
             await _dbContext.Periods.Where(q => q.Id == editVM.Id)
            .ExecuteUpdateAsync(s => s.SetProperty(p => p.Name, editVM.Name));
        }

       

        public async Task RemoveOldPeriodAsync(int peridId)
        {
            await _dbContext.Periods.Where(q => q.Id == peridId)
            .ExecuteDeleteAsync(default);
        }

        public async Task<bool> PeriodExists(int id)
        {
            return await _dbContext.Periods.AnyAsync(e => e.Id == id);
        }

       
    }
}
