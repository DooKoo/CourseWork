using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ISCore.Models
{
    [Serializable()]
    sealed public class Teacher
    {
        /// <summary>
        /// Properties
        /// </summary>
        
        [XmlElement("ID")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public String Name { get; set; }

        [XmlElement("Surname")]
        public String Surname { get; set; }

        /// <summary>
        /// Private default constructor for Serialization 
        /// </summary>
        private Teacher() { }

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="id">Unique identificator of Teacher</param>
        public Teacher(int id)
        {
            Id = id;
        }
    }
}
