using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpdeskDAL;


namespace HelpdeskViewModels
{
    public class ProblemViewModel
    {

        private ProblemDAO _dao;
        public string Description { get; set; }
        public string Id { get; set; }
        public int Version { get; set; }


        public ProblemViewModel()
        {
            _dao = new ProblemDAO();
        }
        

        public void GetById()
        {
            try
            {

                Problem prob = _dao.GetById(Id);
                Description = prob.Description;
                Version = prob.Version;
                Id = prob.GetIdAsString();
            }
            catch (Exception ex)
            {
                Id = "not found";
                ViewModelUtils.ErrorRoutine(ex, "ProblemViewModel", "GetById");
            }
        }


        public int Update()
        {
            UpdateStatus opStatus = UpdateStatus.Failed;

            try
            {
                Problem prob = new Problem();
                prob.SetIdFromString(Id);
                prob.Version = Version;
                prob.Description = Description;

                opStatus = _dao.Update(prob);

            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "ProblemViewModel", "Update");
            }

            return Convert.ToInt16(opStatus);//Web layer won't know about enum
        }

        public void Create()
        {

            try
            {
                Problem prob = new Problem();
                prob.Description = Description;
                prob.Version = 1;
                Id = _dao.Create(prob).GetIdAsString();


            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "ProblemViewModel", "GetById");
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
                ViewModelUtils.ErrorRoutine(ex, "ProblemViewModel", "Delete");
            }

            return deleteFlag;
        }

        public List<ProblemViewModel> GetAll()
        {


            List<ProblemViewModel> vmList = new List<ProblemViewModel>();

            try
            {
                List<Problem> probList = _dao.GetAll();

                foreach (Problem e in probList)
                {
                    //return only fields for display, subsequent get will fill other fields
                    ProblemViewModel viewModel = new ProblemViewModel();
                    viewModel.Id = e.GetIdAsString();
                    viewModel.Description = e.Description;
                    vmList.Add(viewModel);//add to list
                }
            }

            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "ProblemViewModel", "GetAll");
            }
            return vmList;
        }
    }
}
