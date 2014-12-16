using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISCore.Models;

namespace ISCore
{
    sealed public class StudentController
    {
        private Repository<Student> DataRepo;

        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="path">Path to file where storage students data</param>
        public StudentController(String path)
        {
            DataRepo = new Repository<Student>(path);
        }

        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="dataRepo">Repository with students data</param>
        public StudentController(Repository<Student> dataRepo)
        {
            DataRepo = dataRepo;
        }

        /// <summary>
        /// Add student to repository
        /// </summary>
        /// <param name="newStudent">Student that will be added to repository</param>
        public void AddStudent(Student newStudent)
        {
            var queryNumOfItems = DataRepo.Where(stud => stud.Id == newStudent.Id).Count();
            if ( queryNumOfItems == 0)
                DataRepo.Add(newStudent);
            else
            {
                var errorMessage = "Student with " + newStudent.Id + " id, is already exist!";
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// Remove student from repository
        /// </summary>
        /// <param name="delStudent">Student that will be removed from repository</param>
        public void RemoveStudent(Student delStudent)
        {
            DataRepo.Remove(delStudent);
        }

        /// <summary>
        /// Remove student from repository by id
        /// </summary>
        /// <param name="id">Id of student that will be removed from repository</param>
        public void RemoveStudent(int id)
        {
            DataRepo.Remove(id);
        }

        /// <summary>
        /// Change student in repository
        /// </summary>
        /// <param name="oldStudent">Student that will be removed from repository</param>
        /// <param name="newStudent">Student that will be added to repository</param>
        public void ChangeStudent(Student oldStudent, Student newStudent)
        {
            DataRepo.Remove(oldStudent);
            DataRepo.Add(newStudent);
        }

        /// <summary>
        /// Get all students of repository
        /// </summary>
        /// <returns>List of students from repository</returns>
        public List<Student> GetAll()
        {
            var queryAll = from stud in DataRepo select stud;
            return queryAll.ToList();
        }

        /// <summary>
        /// Get student from repository by id
        /// </summary>
        /// <param name="id">Id of student that will be returned</param>
        /// <returns>Student that have Id = 'id'</returns>
        public Student GetStudent(int id)
        {
            var queryStudent = DataRepo.Where(stud => stud.Id == id).ToList().First();
            return queryStudent;
        }
    }
}
