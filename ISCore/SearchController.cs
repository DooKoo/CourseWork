
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISCore.Models;

namespace ISCore
{
    public class SearchController
    {
        /// <summary>
        /// Private repositories 
        /// </summary>
        private Repository<Student> StudentRepository;
        private Repository<ISGroup> GroupRepository;
        private Repository<Subject> SubjectRepository;

        /// <summary>
        /// Constructor with params that init Repos from suplies
        /// </summary>
        /// <param name="studentRepository">Link to StudentRepository</param>
        /// <param name="groupRepository">Link to GroupRepository</param>
        /// <param name="subjectRepository">Link to SubjectRepository</param>
        public SearchController(Repository<Student> studentRepository,
            Repository<ISGroup> groupRepository, Repository<Subject> subjectRepository)
        {
            StudentRepository = studentRepository;
            GroupRepository = groupRepository;
            SubjectRepository = subjectRepository;
        }

        /// <summary>
        /// Search student by name, surname
        /// </summary>
        /// <param name="name">Name of student that will be found</param>
        /// <param name="surname">Surname of student that will be found</param>
        /// <returns>List of Students that has 'name' and 'surname'</returns>
        public List<Student> SerchByName(String name, String surname)
        {
            var queryByName = from stud in StudentRepository
                              where stud.Name == name && stud.Surname == surname
                              select stud;

            return queryByName.ToList();
        }

        /// <summary>
        /// Search student by group
        /// </summary>
        /// <param name="groupId">groupId of group of students</param>
        /// <returns>List of students that studies in group with 'nuber'</returns>
        public List<Student> SearchByGroup(int groupId)
        {
            var queryByGroup = from gr in GroupRepository
                               where gr.Id == groupId
                               select gr.StudentIDs;

            List<Student> result = new List<Student>();

            foreach(int item in queryByGroup.First())
            {
                var studentQuery = from stud in StudentRepository
                                   where stud.Id == item
                                   select stud;

                result.Add(studentQuery.First());
            }                                   

            return result;
        }

        /// <summary>
        /// Search student by Teacher
        /// </summary>
        /// <param name="teacherId">Id of Teacher of students</param>
        /// <returns>List of students of 'teacherId'</returns>
        public List<Student> SearchByTeacher(int teacherId)
        {
            var querySubject = from subject in SubjectRepository
                               where subject.TeacherId == teacherId
                               select subject;
            List<int> groupIds = querySubject.First().GroupIds;

            List<int> studentIds = new List<int>();
            foreach (int item in groupIds)
            {
                var queryStudentIds = from gr in GroupRepository
                                      where gr.Id == item
                                      select gr.StudentIDs;
                studentIds.AddRange(queryStudentIds.First());
            }

            List<Student> result = new List<Student>();
            foreach (int item in studentIds)
            {
                var queryStudent = from student in StudentRepository
                                   where student.Id == item
                                   select student;
                result.Add(queryStudent.First());
            }
            return result;
        }

        /// <summary>
        /// Search students by subject
        /// </summary>
        /// <param name="subject">Id of subject that students studies</param>
        /// <returns>List of students that studied 'subject'</returns>
        public List<Student> SearchBySubject(int subjectId)
        {
            var queryGroupIds = from subj in SubjectRepository
                                where subj.Id == subjectId
                                select subj.GroupIds;

            List<int> studentIds = new List<int>();
            foreach (int item in queryGroupIds.First())
            {
                var queryStudentIds = from gr in GroupRepository
                                      where gr.Id == item
                                      select gr.StudentIDs;
                studentIds.AddRange(queryStudentIds.First());
            }

            List<Student> result = new List<Student>();
            foreach (int item in studentIds)
            {
                var queryStudent = from student in StudentRepository
                                   where student.Id == item
                                   select student;
                result.Add(queryStudent.First());
            }
            return result;
        }
    }
}
