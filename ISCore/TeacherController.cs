using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISCore.Models;

namespace ISCore
{
    sealed public class TeacherController
    {
        private Repository<Teacher> DataRepo;

        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="path">Path to file with teachers data</param>
        public TeacherController(String path)
        {
            DataRepo = new Repository<Teacher>(path);
        }

        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="dataRepo">Repository with teachers data</param>
        public TeacherController(Repository<Teacher> dataRepo)
        {
            DataRepo = dataRepo;
        }

        /// <summary>
        /// Add teacher to repository
        /// </summary>
        /// <param name="newTeacher">Teacher that will be added to repository</param>
        public void AddTeacher(Teacher newTeacher)
        {
            var queryNumOfItems = DataRepo.Where(teach => teach.Id == newTeacher.Id).Count();
            if ( queryNumOfItems == 0)
                DataRepo.Add(newTeacher);
            else
            {
                var errorMessage = "Teacher with " + newTeacher.Id + " id, is already exist!";
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// Remove teacher from repository
        /// </summary>
        /// <param name="delTeacher">Teacher that will be delete from repository</param>
        public void RemoveTeacher(Teacher delTeacher)
        {
            DataRepo.Remove(delTeacher);
        }

        /// <summary>
        /// Remove teacher from repository by id
        /// </summary>
        /// <param name="delTeacher">id of teacher that will be delete from repository</param>
        public void DeleteTeacher(int id)
        {
            DataRepo.Remove(id);
        }

        /// <summary>
        /// Change teacher in repository
        /// </summary>
        /// <param name="oldTeacher">Teacher that will be removed</param>
        /// <param name="newTeacher">Teacher that will be added</param>
        public void ChangeTeacher(Teacher oldTeacher, Teacher newTeacher)
        {
            DataRepo.Remove(oldTeacher);
            DataRepo.Add(newTeacher);
        }

        /// <summary>
        /// Get all teachers from repository
        /// </summary>
        /// <returns>List of teachers</returns>
        public List<Teacher> GetAll()
        {
            var queryAll = from teacher in DataRepo select teacher;
            return queryAll.ToList();
        }

        /// <summary>
        /// Get teacher by id
        /// </summary>
        /// <param name="id">id of teacher that will be removed</param>
        /// <returns>Teacher that have Id = 'id'</returns>
        public Teacher GetTeacher(int id)
        {
            var queryTeacher = DataRepo.Single(teacher => teacher.Id == id);
            return queryTeacher;
        }
    }
}
