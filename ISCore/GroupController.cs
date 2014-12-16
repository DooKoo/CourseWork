using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISCore.Models;

namespace ISCore
{
    sealed public class GroupController
    {
        private Repository<ISGroup> DataRepo;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dataRepo">Repository where storage data about Groups</param>
        public GroupController(Repository<ISGroup> dataRepo)
        {
            DataRepo = dataRepo;
        }

        /// <summary>
        /// Add group to repository
        /// </summary>
        /// <param name="newGroup">Group that will be added to repository</param>
        public void AddGroup(ISGroup newGroup)
        {
            var queryNumOfIds = 0;
            var queryNumOfNumbers = 0;
            if (DataRepo.Count != 0)
            {
                queryNumOfIds = DataRepo.Where(group => group.Id == newGroup.Id).Count();
                queryNumOfNumbers = DataRepo.Where(group =>
                    group.Number == newGroup.Number).Count();
            }
            if ( queryNumOfIds == 0 && queryNumOfNumbers == 0)
                DataRepo.Add(newGroup);
            else
            {
                var errorMessage = "Group with " + newGroup.Id + " id, is already exist!";
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// Remove group from repository
        /// </summary>
        /// <param name="delGroup">Group that will be removed from repository</param>
        public void RemoveGroup(ISGroup delGroup)
        {
            DataRepo.Remove(delGroup);
        }

        /// <summary>
        /// Remove group from repository by id
        /// </summary>
        /// <param name="delGroup">id of Group that will be removed from repository</param>
        public void RemoveGroup(int id)
        {
            DataRepo.Remove(id);
        }

        /// <summary>
        /// Change group in repository
        /// </summary>
        /// <param name="oldGroup">Group that will be delete from repository</param>
        /// <param name="newGroup">Group that will be added to repository</param>
        public void ChangeGroup(ISGroup oldGroup, ISGroup newGroup)
        {
            DataRepo.Remove(oldGroup);
            DataRepo.Add(newGroup);
        }

        /// <summary>
        /// Get all groups that storage in repository
        /// </summary>
        /// <returns>List of groups</returns>
        public List<ISGroup> GetAll()
        {
            var queryAll = from gr in DataRepo select gr;
            return queryAll.ToList();
        }

        /// <summary>
        /// Get group that storage in repository by id
        /// </summary>
        /// <param name="id">id of group</param>
        /// <returns>group that have Id == 'id'</returns>
        public ISGroup GetGroup(int id)
        {
            var queryGroup = DataRepo.Where(gr => gr.Id == id).ToList().First();
            return queryGroup;
        }
    }
}
