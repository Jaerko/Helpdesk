using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL
{
    public class ProblemDAO
    {



        private IRepository repo;

        public ProblemDAO()
        {
            repo = new HelpdeskRepository();
        }

        //GetByLastname
        public Problem GetByProblemName(string probName)
        {
            Problem prob = null;
            var builder = Builders<Problem>.Filter;
            var filter = builder.Eq("Description", probName);

            try
            {
                prob = repo.GetOne<Problem>(filter);
            }
            catch (Exception ex)
            {
                DALUtilsV2.ErrorRoutine(ex, "ProblemDAO", "GetByDescription");
            }
            return prob;
        }
        // End GetByLastname


        //Update with Repo
        public UpdateStatus Update(Problem prob)
        {
            UpdateStatus status = UpdateStatus.Failed;
            try
            {

                var builder = Builders<Problem>.Filter;
                var filter = Builders<Problem>.Filter.Eq("Id", prob.Id) & Builders<Problem>.Filter.Eq("Version", prob.Version);
                var update = Builders<Problem>.Update
                    .Set("Description", prob.Description)
                    .Inc("Version", 1);


                status = repo.Update(prob.Id.ToString(), filter, update);


            }
            catch (Exception ex)
            {
                DALUtilsV2.ErrorRoutine(ex, "ProblemDAO", "Update");

            }
            return status;
        }
        //End UpdateWithRepo



        //Create - add an Problem document to the Problems collection

        public Problem Create(Problem prob)
        {
            HelpdeskRepository repo = new HelpdeskRepository(new DbContext());
            try
            {
                prob = repo.Create(prob);
            }
            catch (Exception ex)
            {
                DALUtilsV2.ErrorRoutine(ex, "ProblemDAO", "Create");
            }
            return prob;
        }

        //Delete - returns a long (1 or 0) depending on if the delete was successful
        public long Delete(string Id)
        {
            long deleteFlag = 0;
            try
            {
                deleteFlag = repo.Delete<Problem>(Id);

            }
            catch (Exception ex)
            {
                DALUtilsV2.ErrorRoutine(ex, "ProblemDAO", "Delete");
            }

            return deleteFlag;
        }


        //GetAll - This one is just going to return a straight List of Problem instances

        public List<Problem> GetAll()
        {


            List<Problem> probList = new List<Problem>();


            try
            {
                probList.AddRange(repo.GetAll<Problem>().ToList());
            }
            catch (Exception ex)
            {
                DALUtilsV2.ErrorRoutine(ex, "ProblemDAO", "GetAll");
            }
            return probList;
        }


        //GetByID - Similar to GetByLastname, see above.
        public Problem GetById(string Id)
        {
            Problem prob = null;
            var builder = Builders<Problem>.Filter;

            try
            {
                prob = repo.GetById<Problem>(Id);
            }
            catch (Exception ex)
            {
                DALUtilsV2.ErrorRoutine(ex, "ProblemDAO", "GetById");
            }
            return prob;
        }




    }
}
