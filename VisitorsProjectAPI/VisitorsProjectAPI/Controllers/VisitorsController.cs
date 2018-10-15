using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VisitorsProjectAPI.Models;

namespace VisitorsProjectAPI.Controllers
{
    public class VisitorsController : ApiController
    {
        public List<Visitors> GetAllVisitors()
        {
           return VisitorSqlAccess.GetAllVisitorsFromLog();
        }
        public List<Visitors> GetVisitorLogByName(string name)
        {
            return VisitorSqlAccess.GetVisitorLogByName(name);
        }
        public List<Visitors> GetVisitorLogByMeetingPerson(string meetingPerson)
        {
            return VisitorSqlAccess.GetVisitorLogByMeetingPerson(meetingPerson);
        }
        public List<Visitors> GetLogByDateAndTime(string fromDate, string toDate, string fromTime, string toTime)
        {
            return VisitorSqlAccess.GetLogByDateAndTime(fromDate, toDate, fromTime, toTime);
        }
        public List<Visitors> GetLogByPurposeOfVisit(string purposeOfVisit)
        {
            return VisitorSqlAccess.GetLogByPurposeOfVisit(purposeOfVisit);
        }
    }
}
