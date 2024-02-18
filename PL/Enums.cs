﻿
using System.Collections;

namespace PL;

/// <summary>
/// enum of the engineer level from the do
/// </summary>
internal class EngineerLevelCollection : IEnumerable
{
    static readonly IEnumerable<BO.EngineerLevel> s_enums =
(Enum.GetValues(typeof(BO.EngineerLevel)) as IEnumerable<BO.EngineerLevel>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
