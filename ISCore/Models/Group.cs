using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ISCore.Models
{
    [Serializable()]
    sealed public class ISGroup
    {
        /// <summary>
        /// Properties
        /// </summary>
        
        [XmlElement("ID")]
        public int Id { get; set; }

        [XmlElement("Number")]
        public int Number { get; set; }

        [XmlArray("StudentsList")]
        [XmlArrayItem("StudentID")]
        public List<int> StudentIDs { get; set; }

        [XmlArray("TeachersList")]
        [XmlArrayItem("TeacherID")]
        public List<int> TeacherIDs { get; set; }

        /// <summary>
        /// Private default constructor for Serialization
        /// </summary>
        private ISGroup() { }

        /// <summary>
        /// Public constructor with params
        /// </summary>
        /// <param name="id">Unique identificator of ISGroup</param>
        /// <param name="groupId">Number of group</param>
        public ISGroup(int id, int number)
        {
            Id = id;
            StudentIDs = new List<int>();
            TeacherIDs = new List<int>();
            Number = number;
        }
        
        /// <summary>
        /// Add an new student to group
        /// </summary>
        /// <param name="nStud">Id of student that will be added to ISGroup</param>
        public void AddStudent(int studentId)
        {
            StudentIDs.Add(studentId);
        }

        /// <summary>
        /// Remove an existing student from group
        /// </summary>
        /// <param name="student">Id of student that will be removed from group</param>
        public void RemoveStudent(int studentId)
        {
            StudentIDs.Remove(studentId);
        }

        /// <summary>
        /// Add an new teacherId to group
        /// </summary>
        /// <param name="nTeacher">Id of teacherId that will be added to ISGroup</param>
        public void AddTeacher(int teacherId)
        {
            TeacherIDs.Add(teacherId);
        }
    }
}
