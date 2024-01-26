using System.Globalization;

namespace BO;

public class Engineer
{
    int Id { get; init; }
    string Name { get; set; }  
    string Email { get; set; }
    EngineerLevel Level { get; set; }
    double Cost { get; set; }
    BO.TaskInEngineer? Task { get; set; }
}
