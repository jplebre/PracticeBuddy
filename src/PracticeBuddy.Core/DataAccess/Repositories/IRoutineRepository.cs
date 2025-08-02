using PracticeBuddy.Core.DataModels;

namespace PracticeBuddy.Core.DataAccess.Repositories;

public interface IRoutineRepository
{
    Task<Routine> GetRoutine(int routineId);
    Task<IList<Routine>> GetRoutines();
    Task<int> InsertRoutine(Routine routine);
}
