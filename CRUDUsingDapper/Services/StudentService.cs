using CRUDUsingDapper.Common;
using CRUDUsingDapper.IServices;
using CRUDUsingDapper.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CRUDUsingDapper.Services
{
    public class StudentService : IStudentService
    {
        Student _oStudent = null;
        List<Student> _oStudents = null;

        public string Delete(int studentId)
        {
            string message = "";

            try
            {
                _oStudent = new Student()
                {
                    StudentId = studentId
                };

                using IDbConnection con = new SqlConnection(Global.ConnectionString);
                if (con.State == ConnectionState.Closed) con.Open();

                var oStudents = con.Query<Student>(
                    "SP_Student",
                    this.SetParameters(_oStudent, (int)OperationType.Delete),
                    commandType: CommandType.StoredProcedure);

                message = "Deleted";
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public Student Get(int studentId)
        {
            _oStudent = new Student();
            using IDbConnection con = new SqlConnection(Global.ConnectionString);
            if (con.State == ConnectionState.Closed) con.Open();

            var oStudents = con.Query<Student>("SELECT * FROM Student WHERE StudentId = " + studentId).ToList();

            if (oStudents != null && oStudents.Count() > 0)
            {
                _oStudent = oStudents.SingleOrDefault();
            }
            return _oStudent;
        }

        public List<Student> Gets()
        {
            _oStudents = new List<Student>();
            using IDbConnection con = new SqlConnection(Global.ConnectionString);
            if (con.State == ConnectionState.Closed) con.Open();

            var oStudents = con.Query<Student>("SELECT * FROM Student").ToList();

            if (oStudents != null && oStudents.Count() > 0)
            {
                _oStudents = oStudents;
            }
            return _oStudents;
        }

        public Student Save(Student oStudent)
        {
            _oStudent = new Student();
            try
            {
                int operationType = Convert.ToInt32(_oStudent.StudentId == 0 ? OperationType.Insert : OperationType.Update);

                using IDbConnection con = new SqlConnection(Global.ConnectionString);
                if (con.State == ConnectionState.Closed) con.Open();

                var oStudents = con.Query<Student>(
                    "SP_Student",
                    this.SetParameters(oStudent, operationType),
                    commandType: CommandType.StoredProcedure);

                if (oStudents != null && oStudents.Count() > 0)
                {
                    _oStudent = oStudents.FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                _oStudent.Message = ex.Message;
            }
            return _oStudent;
        }

        private DynamicParameters SetParameters(Student oStudent, int operationType)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@StudentId", oStudent.StudentId);
            parameters.Add("@Name", oStudent.Name);
            parameters.Add("@Roll", oStudent.Roll);
            parameters.Add("@operationType", operationType);
            return parameters;
        }
    }
}
