using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace ISCore.Models
{
    [Serializable()]
    sealed public class Student
    {
        /// <summary>
        /// Properties
        /// </summary>
        [XmlElement(ElementName = "ID")]
        public int Id { get; set; }

        [XmlElement(ElementName = "Name")]
        public String Name { get; set; }

        [XmlElement(ElementName = "Surname")]
        public String Surname { get; set; }

        [XmlElement(ElementName = "Course")]
        public int Course { get; set; }

        [XmlElement(ElementName = "GradeBook")]
        public String GradeBook { get; set; }

        [XmlElement(ElementName = "Address")]
        public String Address { get; set; }

        [XmlElement(ElementName = "Phone")]
        public String MobilePhone { get; set; }

        /// <summary>
        /// Private default constructor for XML serialization
        /// </summary>
        private Student() { }

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="id">Unique identificator of Student</param>
        public Student(int id)
        {
            Id = id;
        }
    }
}
