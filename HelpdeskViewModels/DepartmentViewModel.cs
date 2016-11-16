using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpdeskDAL;


namespace HelpdeskViewModels
{
    public class DepartmentViewModel
    {

        private DepartmentDAO _dao;
        public string DepartmentName { get; set; }
        public string Id { get; set; }
        public int Version { get; set;}


        public DepartmentViewModel()
        {
            _dao = new DepartmentDAO();
        }

        public void GetByDepartmentName()
        {
            try
            {

                Department dept = _dao.GetByDepartmentName(DepartmentName);

                DepartmentName = dept.DepartmentName;
                Id = dept.GetIdAsString();

            }
            catch (Exception ex)
            {
                DepartmentName = "not found";
                ViewModelUtils.ErrorRoutine(ex, "DepartmentViewModel", "GetByDepartmentName");
            }
        }

        public void GetById()
        {
            try
            {

                Department dept = _dao.GetById(Id);
                DepartmentName = dept.DepartmentName;
                Version = dept.Version;                
                Id = dept.GetIdAsString();
            }
            catch (Exception ex)
            {
                Id = "not found";
                ViewModelUtils.ErrorRoutine(ex, "DepartmentViewModel", "GetById");
            }
        }


        public int Update()
        {
            UpdateStatus opStatus = UpdateStatus.Failed;

            try
            {
                Department dept = new Department();
                dept.SetIdFromString(Id);
                dept.Version = Version;
                dept.DepartmentName = DepartmentName;

                opStatus = _dao.Update(dept);

            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "DepartmentViewModel", "Update");
            }

            return Convert.ToInt16(opStatus);//Web layer won't know about enum
        }

        public void Create()
        {

            try
            {
                Department dept = new Department();
                dept.DepartmentName = DepartmentName;
                dept.Version = 1;
               Id = _dao.Create(dept).GetIdAsString();


            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "DepartmentViewModel", "GetById");
            }

        }

        public long Delete()
        {
            long deleteFlag = 0;
            try
            {
                deleteFlag = _dao.Delete(Id);

            }

            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "DepartmentViewModel", "Delete");
            }

            return deleteFlag;
        }

        public List<DepartmentViewModel> GetAll()
        {


            List<DepartmentViewModel> vmList = new List<DepartmentViewModel>();

            try
            {
                List<Department> deptList = _dao.GetAll();

                foreach (Department e in deptList)
                {
                    //return only fields for display, subsequent get will fill other fields
                    DepartmentViewModel viewModel = new DepartmentViewModel();
                    viewModel.Id = e.GetIdAsString();
                    viewModel.DepartmentName = e.DepartmentName;
                    vmList.Add(viewModel);//add to list
                }
            }

            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "DepartmentViewModel", "GetAll");
            }
            return vmList;
        }
    }
}
