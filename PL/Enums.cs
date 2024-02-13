
using System.Collections;

namespace PL;

internal class EngineerLevelCollection : IEnumerable
{
    static readonly IEnumerable<BO.EngineerLevel> s_enums =
(Enum.GetValues(typeof(BO.EngineerLevel)) as IEnumerable<BO.EngineerLevel>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
