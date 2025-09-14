using AutoMapper;
using LeaveManagmentSystem.Data;
using LeaveManagmentSystem.Models;
using LeaveManagmentSystem.ViewModels.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagmentSystem.Services
{
    public class LeaveTypesService : ILeaveTypesService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public LeaveTypesService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<List<ReadVM>> GetAllAsync()
        {
            var leaveTypes = await _dbContext.LeaveTypes.ToListAsync();
            return _mapper.Map<List<ReadVM>>(leaveTypes);
        }

        public async Task<T?> GetByIdAsync<T>(int id) where T : class
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id cannot be smaller or equal to zero.", nameof(id));
            }
            var leaveType = await _dbContext.LeaveTypes.FirstOrDefaultAsync(lt => lt.Id == id);
            if (leaveType == null)
            {
                return null;
            }
            return _mapper.Map<T>(leaveType);
        }

        public async Task CreateAsync(CreateVM createVm)
        {
            if (createVm == null)
            {
                throw new ArgumentNullException(nameof(createVm), "Create view model cannot be null.");
            }
            var leaveType = _mapper.Map<LeaveType>(createVm);
            _dbContext.LeaveTypes.Add(leaveType);
            await _dbContext.SaveChangesAsync();

        }

        public async Task EditAsync(EditVM editVM)
        {

            if (editVM is null)
            {
                throw new ArgumentNullException(nameof(editVM), "Edit view model is null");
            }

            var leaveType = _mapper.Map<LeaveType>(editVM);
            _dbContext.LeaveTypes.Update(leaveType);
            await _dbContext.SaveChangesAsync();

        }

        public async Task Remove(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id cannot be smaller or equal to zero.", nameof(id));
            }

            var leaveType = await _dbContext.LeaveTypes.FirstOrDefaultAsync(lt => lt.Id == id);

            if (leaveType != null)
            {
                _dbContext.Remove(leaveType);
                await _dbContext.SaveChangesAsync();

            }
        }

        public bool LeaveTypeExists(int id)
        {
            return _dbContext.LeaveTypes.Any(e => e.Id == id);
        }

        public async Task<bool> IsLeaveTypeNameRepeatedAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }

            var result = await _dbContext.LeaveTypes.AnyAsync(lt => lt.Name.ToLower().Equals(name.ToLower()));
            return result;
        }

        public async Task<bool> IsLeaveTypeNameRepeatedForEditAsync(int id, EditVM editVm)
        {
            if (id is <= 0)
            {
                throw new ArgumentException("Id cannot be smaller or equal to zero.", nameof(id));
            }
            var result = await _dbContext.LeaveTypes.AnyAsync(lt => lt.Name.ToLower().Equals(editVm.Name.ToLower()) && lt.Id != id);
            return result;
        }
    }



}
