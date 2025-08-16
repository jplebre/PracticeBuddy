using System.Data;
using Dapper;

namespace PracticeBuddy.Core.DataAccess.TypeMappers;

public class TimeSpanHandler : SqlMapper.TypeHandler<TimeSpan>
{
    private TimeSpanHandler() { /* private constructor */ }

    // Make the field type ITypeHandler to ensure it cannot be used with SqlMapper.AddTypeHandler<T>(TypeHandler<T>)
    // by mistake.
    public static readonly SqlMapper.ITypeHandler Default = new TimeSpanHandler();

    public override TimeSpan Parse(object? value)
    {
        var timeSpan = (string)value!;
        return TimeSpan.ParseExact(timeSpan, "c", null);
    }

    public override void SetValue(IDbDataParameter parameter, TimeSpan value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value.ToString();
    }
}
