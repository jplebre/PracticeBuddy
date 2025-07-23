using PracticeBuddy.Core.Models;

namespace PracticeBuddy.Core.DataAccess.Repositories;

public class RoutineRepository
{
    private readonly IDapperDb _database;

    public RoutineRepository(IDapperDb database)
    {
        _database = database;
    }

    public async Task<int> InsertRoutine(Routine routine)
    {
        throw new NotImplementedException();

        // var query = @"
        //     INSERT INTO `Talks` 
        //     (EasyAccessKey,Name,Topic,
        //     Description,SpeakerName,TalkCreationTime,
        //     TalkStartTime,QuestionnaireId)
        //     VALUES 
        //     (@EasyAccessKey,@Name,@Topic,
        //     @Description,@SpeakerName,@TalkCreationTime,
        //     @TalkStartTime,@QuestionnaireId);
        //     SELECT LAST_INSERT_ID()";
        // return _dapper.Query<int>(query, talkDTO).FirstOrDefault();
    }
}