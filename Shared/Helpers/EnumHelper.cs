using System.Text;

namespace Shared.Helpers;

public static class EnumHelper
{
    public static IEnumerable<T> GetValues<T>() => Enum.GetValues(typeof (T)).Cast<T>();
    
}