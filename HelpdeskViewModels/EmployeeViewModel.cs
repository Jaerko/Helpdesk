using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpdeskDAL;


namespace HelpdeskViewModels
{
    public class EmployeeViewModel
    {

        private EmployeeDAO _dao;
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phoneno { get; set; }
        public int Version { get; set; }
        public string DepartmentId { get; set; }
        public string Id { get; set; }
        public string StaffPicture64 { get; set; }
        public bool IsTech { get; set; }

        public EmployeeViewModel()
        {
            _dao = new EmployeeDAO();
        }

        public void GetByLastname()
        {
            try
            {

                Employee emp = _dao.GetByLastname(Lastname);

                Title = emp.Title;
                Firstname = emp.Firstname;
                Lastname = emp.Lastname;
                Phoneno = emp.Phoneno;
                IsTech = emp.IsTech;
                StaffPicture64 = emp.StaffPicture64;
                Email = emp.Email;
                Id = emp.GetIdAsString();
                DepartmentId = emp.GetDepartmentIdAsString();
                Version = emp.Version;
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                 ViewModelUtils.ErrorRoutine(ex, "EmployeeViewModel", "GetByLastname");
            }
        }

        public void GetById()
        {
            try
            {

                Employee emp = _dao.GetById(Id);

                Title = emp.Title;
                Firstname = emp.Firstname;
                Lastname = emp.Lastname;
                Phoneno = emp.Phoneno;
                Email = emp.Email;
                StaffPicture64 = emp.StaffPicture64;
                IsTech = emp.IsTech;
                Id = emp.GetIdAsString();
                DepartmentId = emp.GetDepartmentIdAsString();
                Version = emp.Version;
            }
            catch (Exception ex)
            {
                Id = "not found";
                 ViewModelUtils.ErrorRoutine(ex, "EmployeeViewModel", "GetById");
            }
        }

        public int Update()
        {
            UpdateStatus opStatus;

            try
            {
                Employee emp = new Employee();
                emp.SetIdFromString(Id);
                emp.SetDepartmentIdFromString(DepartmentId);
                emp.Title = Title;
                emp.Firstname = Firstname;
                emp.Lastname = Lastname;
                emp.Phoneno = Phoneno;
                emp.Email = Email;
                emp.IsTech = emp.IsTech;
                emp.StaffPicture64 = StaffPicture64;
                emp.Version = Version;
                opStatus = _dao.Update(emp);

            }
            catch (Exception ex)
            {
                opStatus = UpdateStatus.Failed; ;
                 ViewModelUtils.ErrorRoutine(ex, "EmployeeViewModel", "Update");
            }

            return Convert.ToInt16(opStatus);//Web layer won't know about enum
        }

        public void Create()
        {

            try
            {
                Employee emp = new Employee();
                emp.Title = Title;
                emp.Firstname = Firstname;
                emp.Lastname = Lastname;
                emp.Phoneno = Phoneno;
                emp.Email = Email;
                emp.Version = Version;
                emp.SetDepartmentIdFromString(DepartmentId);
                Id = _dao.Create(emp).GetIdAsString();



            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "EmployeeViewModel", "Create");
            }

        }

        public long Delete()
        {
            long deleteFlag = 0;
            try
            {
                deleteFlag = _dao.Delete(Id);
                
            }

            catch(Exception ex)
            {
                 ViewModelUtils.ErrorRoutine(ex, "EmployeeViewModel", "Delete");
            }

            return deleteFlag;
        }

        public List<EmployeeViewModel> GetAll()
        {

            
            List<EmployeeViewModel> vmList = new List<EmployeeViewModel>();

            try
            {
                List<Employee> empList = _dao.GetAll();

                foreach (Employee e in empList)
                {
                    //return only fields for display, subsequent get will fill other fields
                    EmployeeViewModel viewModel = new EmployeeViewModel();
                    viewModel.Id = e.GetIdAsString();
                    viewModel.Title = e.Title;
                    viewModel.Firstname = e.Firstname;
                    viewModel.Lastname = e.Lastname;
                    viewModel.Email = e.Email;
                    viewModel.Version = e.Version;
                    viewModel.DepartmentId = e.GetDepartmentIdAsString();
                    vmList.Add(viewModel);//add to list
                }
            }

            catch (Exception ex)
            {
                 ViewModelUtils.ErrorRoutine(ex, "EmployeeViewModel", "GetAll");
            }
            return vmList;
        }
    }
}
