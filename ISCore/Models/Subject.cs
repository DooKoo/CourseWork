using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ISCore.Models
{
    [Serializable()]
    sealed public class Subject
    {
        /// <summary>
        /// Properties
        /// </summary>
        [XmlElement("ID")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public String Name { get; set; }

        [XmlElement("TeacherID")]
        public int TeacherId { get; set; }

        [XmlArray("GroupList")]
        [XmlArrayItem("GroupID")]
        public List<int> GroupIds { get; set; }

        /// <summary>
        /// Private default constructor for XML serialization
        /// </summary>
        private Subject() { }

        /// <summary>
        /// Public constructor with params.
        /// </summary>
        /// <param name="id">Unique identificator of Subject</param>
        public Subject(int id)
        {
            Id = id;
            GroupIds = new List<int>();
        }

        /// <summary>
        /// Method that set teacherId for current subject
        /// </summary>
        /// <param name="teacherId">Id of Teacher that provide current subject</param>
        public void SetTeacher(int teacherId)
        {
            TeacherId = teacherId;
        }

        /// <summary>
        /// Method that add group to List of groups that studied this subject
        /// </summary>
        /// <param name="group">Id of ISGroup that will be studied this subject</param>
        public void AddGroup(int groupId)
        {
            GroupIds.Add(groupId);
        }

        /// <summary>
        /// Method that delete group from List of groups that studied this subject
        /// </summary>
        /// <param name="group">Id of ISGroup that stop studied this subject</param>
        public void RemoveGroup(int groupId)
        {
            GroupIds.Remove(groupId);
        }
    }
}
