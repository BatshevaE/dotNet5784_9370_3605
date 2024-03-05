//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PL;

//public class SelectTaskToGunt
//{
//    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();


//    public DataTable Entries { get; set; }
//    public SelectTaskToGunt()
//    {
//        List<BO.Task> tasks = s_bl.Task.ReadAll2().ToList();
//        Entries = new DataTable()
//        {
//            Columns = {
//                new DataColumn("TaskId", typeof(string)),
//                new DataColumn("TaskName", typeof(string)),
//                new DataColumn("Dependencies", typeof(string)),
//            }
//        };
//        for (int i = 0; i < tasks.Count(); i++)
//        {
//            BO.Task task = tasks[i];
//            List<BO.TaskInList> d = task.Dependencies!;
//            string str = "(";
//            for (int j = 0; j < d.Count(); j++)
//            {
//                str += $"{d[j].Id},";
//            }
//            str.Remove(str.Length - 1);
//            str += ")";
//            Entries.Rows.Add(task.Id, task.Name, str);

//        }
//    }



//}
