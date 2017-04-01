using LetsConnect.Data.Domains.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsConnect.Services.Interface.IActivity
{
 public interface IActivityRepository
    {
        List<Activity> GetAll(int PageNumber, int PageSize);
        Activity GetById(int Id);
        int AddNew(Int16 activityType, Exception ex, long activityDate, string ManuallyErrorname = "");
        int Update(Activity activity);
        bool Delete(int Id);
    }
}
