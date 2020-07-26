using System;

namespace TransportManagerLibrary.DAO
{
    class PersonalDAO
    {
        
        public  string EmpCode { get; set; }
        public  string CardNo { get; set; }
        public  string EmpName { get; set; }
        public  int ComCode { get; set; }
        public  int ShiftId { get; set; }
        public  string EmpType { get; set;}

        public  string MGR { get; set; }
        public  int GradeId { get; set; }
        public  int DeptId { get; set; }
        public  int DesignationId { get; set; }
        public  DateTime AppointDate { get; set; }
        public  DateTime JoiningDate { get; set; }
        public  DateTime ConfirmDate { get; set; }
        public  string PBXExt { get; set; }
        public  string  EducDegree { get; set; }
        public  DateTime  Birthday { get; set; }
        public  byte Sex { get; set; }
        public  byte MeritalStatus { get; set; }
        public  string SpouseName { get; set; }
        public  string FatherName { get; set; }
        public  string MotherName { get; set; }
        public  string BloodGroup { get; set; }
        public  string Address1 { get; set; }
        public  string Address2 { get; set; }
        public  string Address3 { get; set; }
        public  string PostCode { get; set; }
        public  string Country { get; set; }
        public  string Phone { get; set; }
      
        public  string Mobile { get; set; }
        public  string Fax { get; set; }
        public  string Email { get; set; }
        public  int status { get; set; }
        public  string DrivingLice { get; set; }

        public   void ClearAll()
        {
            EmpCode = null;
             CardNo =null;
            EmpName = null;
            ComCode=1;
             ShiftId=0;
            EmpType=null;
            MGR = null;
            GradeId = 1;
            DeptId = 1;
            DesignationId =1;
            AppointDate = DateTime.Today;
            JoiningDate = DateTime.Today;
            ConfirmDate = DateTime.Today;
            PBXExt = null;
            EducDegree = null;
            Birthday = DateTime.Today;
            Sex = 0;
            MeritalStatus = 0;
            SpouseName = null;
            FatherName = null;
            MotherName = null;
            BloodGroup = null;
            Address1 = null;
            Address2 = null;
            Address3 = null;
            PostCode = null;
            Country = null;
            Phone = null;

            Mobile = null;
            Fax = null;
            Email = null;
            status = 0;
            DrivingLice = null;


        }
    }
}
