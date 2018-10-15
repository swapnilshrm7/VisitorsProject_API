using System;
using System.Collections.Generic;
using System.Data.SqlClient; 
using VisitorsProjectAPI.Models;

namespace VisitorsProjectAPI.Controllers
{
    public class VisitorSqlAccess
    {
        public static List<Visitors> GetAllVisitorsFromLog()
        {
            List<Visitors> visitorsList = new List<Visitors>();
            Visitors visitor = new Visitors();
            SqlConnection connection = SQLConnection.SQLConnectionEstablishment();
            connection.Open();
            string getAllVisitorsLog = "select * from VisitorsLogs";
            SqlCommand sqlCommand = new SqlCommand(getAllVisitorsLog, connection);
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    visitor.ComingFrom = reader["ComingFrom"].ToString();
                    visitor.WhomToMeet = reader["WhomToMeet"].ToString();
                    visitor.EmployeeId = reader["EmployeeId"].ToString();
                    visitor.TimeIn = reader["TimeIn"].ToString();
                    visitor.TimeOut = reader["TimeOut"].ToString();
                    visitor.VisitorId = Convert.ToInt32(reader["VisitorId"].ToString());
                    visitor.GuardId = reader["GuardId"].ToString();
                    string getVisitorsDataById = "select * from VisitorsData where VisitorId=@VisitorId";
                    SqlCommand sqlCommand2 = new SqlCommand(getVisitorsDataById, connection);
                    sqlCommand2.Parameters.Add(new SqlParameter("VisitorId", visitor.VisitorId));
                    using (SqlDataReader reader2 = sqlCommand2.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            visitor.NameOfVisitor = reader["NameOfVisitor"].ToString();
                            visitor.GovtIdProof = reader["GovtIdProof"].ToString();
                            visitor.Contact = Convert.ToInt32(reader["Contact"].ToString());
                        }
                    }
                    visitorsList.Add(visitor);
                }
            }
            connection.Close();
            return visitorsList;
        }
        public static List<Visitors> GetVisitorLogByName(string name)
        {
            List<Visitors> visitorsList = new List<Visitors>();
            Visitors visitorEntry = new Visitors();
            int visitorId;
            SqlConnection connection = SQLConnection.SQLConnectionEstablishment();
            connection.Open();
            string getlVisitorsLogByName = "select * from VisitorsData where NameOfVisitor = @searchInput";
            SqlCommand sqlCommand = new SqlCommand(getlVisitorsLogByName, connection);
            sqlCommand.Parameters.Add(new SqlParameter("searchInput", name));
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    visitorId = Convert.ToInt32(reader["VisitorId"]);
                    string getVisitorsDataById = "select * from VisitorsLogs where VisitorId=@VisitorId";
                    SqlCommand sqlCommand2 = new SqlCommand(getVisitorsDataById, connection);
                    sqlCommand2.Parameters.Add(new SqlParameter("VisitorId", visitorId));
                    using (SqlDataReader readerInner = sqlCommand2.ExecuteReader())
                    {
                        while (readerInner.Read())
                        {
                            visitorEntry.NameOfVisitor = name;
                            visitorEntry.GovtIdProof = reader["GovtIdProof"].ToString();
                            visitorEntry.Contact = Convert.ToInt32(reader["Contact"].ToString());
                            visitorEntry.ComingFrom = readerInner["ComingFrom"].ToString();
                            visitorEntry.WhomToMeet = readerInner["WhomToMeet"].ToString();
                            visitorEntry.EmployeeId = readerInner["EmployeeId"].ToString();
                            visitorEntry.TimeIn = readerInner["TimeIn"].ToString();
                            visitorEntry.TimeOut = readerInner["TimeOut"].ToString();
                            visitorEntry.VisitorId = Convert.ToInt32(readerInner["VisitorId"].ToString());
                            visitorEntry.GuardId = readerInner["GuardId"].ToString();
                        }
                    }
                    visitorsList.Add(visitorEntry);
                }
            }
            connection.Close();
            return visitorsList;
        } 
        public static List<Visitors> GetVisitorLogByMeetingPerson(string meetingPerson)
        {
            List<Visitors> visitorsList = new List<Visitors>();
            Visitors visitorOfMeetingPerson = new Visitors(); 
            SqlConnection connection = SQLConnection.SQLConnectionEstablishment();
            connection.Open();
            string getVisitorsLogByMeetingPerson = "select * from VisitorsLogs where WhomToMeet = @searchInput";
            SqlCommand sqlCommand = new SqlCommand(getVisitorsLogByMeetingPerson, connection);
            sqlCommand.Parameters.Add(new SqlParameter("searchInput", meetingPerson));
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    visitorOfMeetingPerson.ComingFrom = reader["ComingFrom"].ToString();
                    visitorOfMeetingPerson.WhomToMeet = meetingPerson;
                    visitorOfMeetingPerson.EmployeeId = reader["EmployeeId"].ToString();
                    visitorOfMeetingPerson.TimeIn = reader["TimeIn"].ToString();
                    visitorOfMeetingPerson.TimeOut = reader["TimeOut"].ToString();
                    visitorOfMeetingPerson.VisitorId = Convert.ToInt32(reader["VisitorId"].ToString());
                    visitorOfMeetingPerson.GuardId = reader["GuardId"].ToString();
                    string getVisitorsDataById = "select * from VisitorsData where VisitorId=@VisitorId";
                    SqlCommand sqlCommand2 = new SqlCommand(getVisitorsDataById, connection);
                    sqlCommand2.Parameters.Add(new SqlParameter("VisitorId", visitorOfMeetingPerson.VisitorId));
                    using (SqlDataReader reader2 = sqlCommand2.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            visitorOfMeetingPerson.NameOfVisitor = reader["NameOfVisitor"].ToString();
                            visitorOfMeetingPerson.GovtIdProof = reader["GovtIdProof"].ToString();
                            visitorOfMeetingPerson.Contact = Convert.ToInt32(reader["Contact"].ToString());
                        }
                    }
                    visitorsList.Add(visitorOfMeetingPerson);
                }

            }
            //visitorsList.Add(visitorOfMeetingPerson);  
            connection.Close();
            return visitorsList;
        } 
        public static List<Visitors> GetLogByDateAndTime(string fromDate, string toDate, string fromTime, string toTime)
        {
            if (fromTime == "" && toTime == "")
            {
                fromTime = "00:00:00";
                toTime = "23:59:59";
            }
            List<Visitors> visitorsList = new List<Visitors>();
            Visitors visitorInSpecifiedTimeInterval = new Visitors();
            SqlConnection connection = SQLConnection.SQLConnectionEstablishment();
            connection.Open();
            string getVisitorsLogByTime = "select * from VisitorsLogs where DateOfLogin between @fromDate And @toDate and TimeIn = @fromTime and TimeOut = @toTime";
            SqlCommand sqlCommand = new SqlCommand(getVisitorsLogByTime, connection);
            sqlCommand.Parameters.Add(new SqlParameter("fromDate", fromDate));
            sqlCommand.Parameters.Add(new SqlParameter("toDate", toDate));
            sqlCommand.Parameters.Add(new SqlParameter("fromTime", fromTime));
            sqlCommand.Parameters.Add(new SqlParameter("toTime", toTime));
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    visitorInSpecifiedTimeInterval.ComingFrom = reader["ComingFrom"].ToString();
                    visitorInSpecifiedTimeInterval.WhomToMeet = reader["WhomToMeet"].ToString();
                    visitorInSpecifiedTimeInterval.EmployeeId = reader["EmployeeId"].ToString();
                    visitorInSpecifiedTimeInterval.VisitorId = Convert.ToInt32(reader["VisitorId"].ToString());
                    visitorInSpecifiedTimeInterval.GuardId = reader["GuardId"].ToString();
                    string getVisitorsDataById = "select * from VisitorsData where VisitorId=@VisitorId";
                    SqlCommand sqlCommand2 = new SqlCommand(getVisitorsDataById, connection);
                    sqlCommand2.Parameters.Add(new SqlParameter("VisitorId", visitorInSpecifiedTimeInterval.VisitorId));
                    using (SqlDataReader reader2 = sqlCommand2.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            visitorInSpecifiedTimeInterval.NameOfVisitor = reader["NameOfVisitor"].ToString();
                            visitorInSpecifiedTimeInterval.GovtIdProof = reader["GovtIdProof"].ToString();
                            visitorInSpecifiedTimeInterval.Contact = Convert.ToInt32(reader["Contact"].ToString());
                        }
                    }
                    visitorsList.Add(visitorInSpecifiedTimeInterval);
                }
            }
            return visitorsList;
        }
        public static List<Visitors> GetLogByPurposeOfVisit(string purposeOfVisit)
        {
            List<Visitors> visitorsList = new List<Visitors>();
            Visitors visitorWithPurposeToVisit = new Visitors();
            SqlConnection connection = SQLConnection.SQLConnectionEstablishment();
            connection.Open();
            string getVisitorsLogByMeetingPerson = "select * from VisitorsLogs where PurposeToVisit = @purposeOfVisit";
            SqlCommand sqlCommand = new SqlCommand(getVisitorsLogByMeetingPerson, connection);
            sqlCommand.Parameters.Add(new SqlParameter("purposeOfVisit", purposeOfVisit));
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    visitorWithPurposeToVisit.ComingFrom = reader["ComingFrom"].ToString();
                    visitorWithPurposeToVisit.WhomToMeet = purposeOfVisit;
                    visitorWithPurposeToVisit.EmployeeId = reader["EmployeeId"].ToString();
                    visitorWithPurposeToVisit.TimeIn = reader["TimeIn"].ToString();
                    visitorWithPurposeToVisit.TimeOut = reader["TimeOut"].ToString();
                    visitorWithPurposeToVisit.VisitorId = Convert.ToInt32(reader["VisitorId"].ToString());
                    visitorWithPurposeToVisit.GuardId = reader["GuardId"].ToString();
                    string getVisitorsDataById = "select * from VisitorsData where VisitorId=@VisitorId";
                    SqlCommand sqlCommand2 = new SqlCommand(getVisitorsDataById, connection);
                    sqlCommand2.Parameters.Add(new SqlParameter("VisitorId", visitorWithPurposeToVisit.VisitorId));
                    using (SqlDataReader reader2 = sqlCommand2.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            visitorWithPurposeToVisit.NameOfVisitor = reader["NameOfVisitor"].ToString();
                            visitorWithPurposeToVisit.GovtIdProof = reader["GovtIdProof"].ToString();
                            visitorWithPurposeToVisit.Contact = Convert.ToInt32(reader["Contact"].ToString());
                        }
                    }
                    visitorsList.Add(visitorWithPurposeToVisit);
                }

            }
           // visitorsList.Add(visitorOfMeetingPerson);
            connection.Close();
            return visitorsList;
        }
    }
}