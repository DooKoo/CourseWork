using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ISCore;
using ISCore.Models;

namespace CourseWork
{
    class View
    {
        private Dictionary<String, Command> CommandDictionary;

        private Repository<Student> StudentRepository;
        private Repository<ISGroup> GroupRepository;
        private Repository<Subject> SubjectRepository;
        private Repository<Teacher> TeacherRepository;

        private StudentController StudentControll;
        private TeacherController TeacherControll;
        private GroupController GroupControll;
        private SearchController SearchControll;

        private String Path;

        /// <summary>
        /// Constructor with params where files loads
        /// </summary>
        /// <param inputString="Path">path to folder where will storage files</param>
        public View(String path)
        {
            Path = path;

            Thread StudentThread = new Thread(() =>
            {
                StudentRepository = new Repository<Student>(Path + "Students.cdat");
                StudentRepository.Load();
            });

            Thread GroupThread = new Thread(() =>
            {
                GroupRepository = new Repository<ISGroup>(Path + "Groups.cdat");
                GroupRepository.Load();

            });

            Thread SubjectThread = new Thread(() =>
            {
                SubjectRepository = new Repository<Subject>(Path + "Subject.cdat");
                SubjectRepository.Load();
            });

            Thread TeacherThread = new Thread(() =>
            {
                TeacherRepository = new Repository<Teacher>(Path + "Teachers.cdat");
                TeacherRepository.Load();
            });

            StudentThread.Start();
            GroupThread.Start();
            SubjectThread.Start();
            TeacherThread.Start();

            while (StudentThread.IsAlive || GroupThread.IsAlive ||
                    SubjectThread.IsAlive || TeacherThread.IsAlive)
            {
                Console.Clear();
                Console.Write("Wait");
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(500);
                    Console.Write(".");
                }
            }
            Console.Clear();
            CommandDictionary = new Dictionary<string, Command>();

            StudentControll = new StudentController(StudentRepository);
            TeacherControll = new TeacherController(TeacherRepository);
            GroupControll = new GroupController(GroupRepository);
            SearchControll = new SearchController(StudentRepository, GroupRepository, SubjectRepository);

            // COMMANDS
            CommandDictionary.Add("add student", new Command("add student", "Add new student;"));
            CommandDictionary.Add("add group", new Command("add group", "Add new group;"));
            CommandDictionary.Add("show students", new Command("show students", "Show list of students"));
            CommandDictionary.Add("show groups", new Command("show groups", "Show list of groups"));
            CommandDictionary.Add("student info", new Command("student info", "Show information about student"));
            CommandDictionary.Add("group info", new Command("group info", "Show information about group"));
            CommandDictionary.Add("add subject", new Command("add subject", "Add new subject;"));
            CommandDictionary.Add("add teacher", new Command("add teacher", "Add new teacher;"));
            CommandDictionary.Add("add student to group", new Command("add student to group", "Add student to group"));
            CommandDictionary.Add("add teacher to group", new Command("add teacher to group", "Add teacher to group"));
            CommandDictionary.Add("set subject teacher", new Command("set subject teacher", "Set teacher for subject"));
            CommandDictionary.Add("find students by group", new Command("find students by group", "View all students of group"));
            CommandDictionary.Add("find students by teacher", new Command("find students by teacher", "View all students of teacher"));
            CommandDictionary.Add("find students by subject", new Command("find students by subject", "View all students of subject"));
            CommandDictionary.Add("clr", new Command("clr", "Delete all inforamation from screen"));
            CommandDictionary.Add("exit", new Command("exit", "Close the application;"));
            CommandDictionary.Add("help", new Command("help", "Information about all commands;"));

            // ACTION OF COMMANDS
            CommandDictionary["add group"].SetAction(() =>
            {
                GroupControll.AddGroup(GetGroup());
            });

            CommandDictionary["add subject"].SetAction(() => 
            {
                SubjectRepository.Add(GetSubject());
            });

            CommandDictionary["add teacher"].SetAction(() => 
            {
                TeacherRepository.Add(GetTeacher());
            });

            CommandDictionary["add student"].SetAction(() =>
            {
                StudentControll.AddStudent(GetStudent());
            });

            CommandDictionary["add teacher to group"].SetAction(() => 
            { 
                var groupId = GetId("group");
                var teacherId = GetId("teacher");
                GroupControll.GetGroup(groupId).AddTeacher(teacherId);
            });

            CommandDictionary["add student to group"].SetAction(() =>
            {
                var groupId = GetId("group");
                var studentId = GetId("student");
                GroupControll.GetGroup(groupId).AddStudent(studentId);
            });

            CommandDictionary["set subject teacher"].SetAction(() => 
            {
                var subId = GetId("subject");
                var teacherId = GetId("teacher");
                SubjectRepository[subId].SetTeacher(teacherId);
            });

            CommandDictionary["show students"].SetAction(() => 
            {
                var listOfStudents = StudentControll.GetAll();
                Console.WriteLine("Students:");
                Console.WriteLine("--------------------------------------");
                foreach (Student student in listOfStudents)
                {
                    Console.WriteLine("Id: {0}; Name: {1}; Surname: {2};", 
                        student.Id, student.Name, student.Surname);
                }
                Console.WriteLine("--------------------------------------");
            });

            CommandDictionary["show groups"].SetAction(() => 
            {
                var listOfGroups = GroupControll.GetAll();
                Console.WriteLine("Groups:");
                Console.WriteLine("--------------------------------------");
                foreach (ISGroup group in listOfGroups)
                {
                    Console.WriteLine("Id: {0}; Number: {1};", group.Id, group.Number);
                }
                Console.WriteLine("--------------------------------------");
            });

            CommandDictionary["group info"].SetAction(() =>
            {
                ISGroup group = GroupControll.GetGroup(GetId("group"));
                Console.WriteLine("Group:{0}", group.Id);
                Console.WriteLine("Number:{0}", group.Number);
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Students:");
                foreach (int id in group.StudentIDs)
                {
                    Student student = StudentControll.GetStudent(id);
                    Console.WriteLine("Id: {0}; Name: {1}; Surname: {2};", id, student.Name, student.Surname);
                }
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Teachers that teach group:");
                foreach (int id in group.TeacherIDs)
                {
                    Teacher queryTeacher = TeacherRepository
                        .Where(teacher => teacher.Id == id).Single();
                    Console.WriteLine("Id: {0}; Name: {1}; Surname: {2};", id, queryTeacher.Name, queryTeacher.Surname);
                }
                Console.WriteLine("--------------------------------------");
            });

            CommandDictionary["student info"].SetAction(() => 
            {
                Student student = StudentControll.GetStudent(GetId("student"));
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Id: {0};", student.Id);
                Console.WriteLine("Name: {0};", student.Name);
                Console.WriteLine("Surname: {0};", student.Surname);
                Console.WriteLine("Course: {0};", student.Course);
                Console.WriteLine("Course: {0};", student.GradeBook);
                Console.WriteLine("Address: {0};", student.Address);
                Console.WriteLine("Mobile number: {0};", student.MobilePhone);
                Console.WriteLine("----------------------------------------");
            });

            CommandDictionary["find students by group"].SetAction(() => 
            {
                var listOfStudents = SearchControll.SearchByGroup(GetId("group"));
                foreach (Student student in listOfStudents)
                {
                    Console.WriteLine("Id: {0}; Name: {1}; Surname: {2};", student.Id, student.Name, student.Surname);
                }
            });

            CommandDictionary["find students by teacher"].SetAction(() =>
            {
                var listOfStudents = SearchControll.SearchByTeacher(GetId("teacher"));
                foreach (Student student in listOfStudents)
                {
                    Console.WriteLine("Id: {0}; Name: {1}; Surname: {2};", student.Id, student.Name, student.Surname);
                }
            });

            CommandDictionary["find students by subject"].SetAction(() =>
            {
                var listOfStudents = SearchControll.SearchByGroup(GetId("subject"));
                foreach (Student student in listOfStudents)
                {
                    Console.WriteLine("Id: {0}; Name: {1}; Surname: {2};", student.Id, student.Name, student.Surname);
                }
            });

            CommandDictionary["help"].SetAction(() =>
            {
                foreach (Command item in CommandDictionary.Values)
                {
                    Console.WriteLine("{0} : {1}", item.Text, item.Description);
                }
            });

            CommandDictionary["clr"].SetAction(() =>
            {
                Console.Clear();
            });

            CommandDictionary["exit"].SetAction(() =>
            {
                Console.WriteLine("Okey :( Good bye!");
                Exit();
                Environment.Exit(0);
            });

            MainMenu();
        }

        public void MainMenu()
        {
            Console.WriteLine("Current directory: {0}", Path);
            while (true)
            {
                Console.Write(">");
                String action = Console.ReadLine();
                try
                {
                    CommandDictionary[action].Action(MainMenu);
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("Invalid command. Use 'help' to see more commands");
                }
            }
        }

        private ISGroup GetGroup()
        {
            bool check = false;
            String numOfGroup = "";
            while (!check)
            {
                Console.Write("Press number of group:\n>");
                numOfGroup = Console.ReadLine();
                if (numOfGroup == "back")
                {
                    MainMenu();
                }
                check = Regex.IsMatch(numOfGroup, @"^\d{3}$");
                if (!check)
                {
                    Console.WriteLine("Oops :( Error. Try again!");
                }
            }
            return new ISGroup(GroupRepository.Count, Convert.ToInt32(numOfGroup));
        }

        private Student GetStudent()
        {
            Student resultStudent = new Student(StudentRepository.Count);
            String[] patterns = {@"^[A-Z][-a-zA-Z]{1,31}$", 
                                    @"^[A-Z][-a-zA-Z]{1,63}$", 
                                    @"^[1-5]{1}$",
                                    @"^[A-Z]{2}\d{5}$",
                                    @"^[A-Z]{1}[-A-Za-z]{1,15} [A-Z]{1}[-A-Za-z]{1,15} \d{1,3}$", 
                                    @"^\+380\d{9}$"};
            String[] fields = {"name", 
                                  "surname",
                                  "course", 
                                  "grade book", 
                                  "adress(City Street 123)",
                                  "number(+380123456789)",};
            String[] results = { "", "", "", "", "", "" };

            for (int i = 0; i < 6; i++)
            {
                bool check = false;
                String inputString = "";
                while (!check)
                {
                    Console.Write("Press "+ fields[i] +" of student:\n>");
                    inputString = Console.ReadLine();
                    if (inputString == "back")
                    {
                        MainMenu();
                    }
                    check = Regex.IsMatch(inputString, patterns[i]);   
                    if (!check)
                    {
                        Console.WriteLine("Incorrect "+ fields[i] +". Try again Please!");
                    }
                }
                results[i] = inputString;
            }
            resultStudent.Name = results[0];
            resultStudent.Surname = results[1];
            resultStudent.Course = Convert.ToInt32(results[2]);
            resultStudent.GradeBook = results[3];
            resultStudent.Address = results[4];
            resultStudent.MobilePhone = results[5]; 
            return resultStudent;
        }

        private int GetId(String owner)
        {
            bool check = false;
            var inputString = "";
            while (!check)
            {
                Console.Write("Press id of {0}:\n>", owner);
                inputString = Console.ReadLine();
                if (inputString == "back")
                {
                    MainMenu();
                }
                check = Regex.IsMatch(inputString, @"\d{1,5}");
                if (!check)
                {
                    Console.WriteLine("Oops :( Incorrect Id. Try again!");
                }
            }
            return Convert.ToInt32(inputString);
        }

        private Subject GetSubject()
        {
            Subject resultSubject = new Subject(SubjectRepository.Count);
            bool check = false;
            var subName = "";
            while (!check)
            {
                Console.WriteLine("Press subject name:\n>");
                subName = Console.ReadLine();
                if(subName == "back")
                {
                    MainMenu();
                }
                check = Regex.IsMatch(subName, @"^[A-Z][A-Za-z ]{2,10}$");
                if(!check)
                {
                    Console.WriteLine("Oops :( Incorrect name. Try again!");
                }
            }
            resultSubject.Name = subName;
            return resultSubject;
        }

        private Teacher GetTeacher()
        {
            Teacher teacher = new Teacher(TeacherRepository.Count);
            String[] patterns = {@"^[A-Z][-a-zA-Z]{1,31}$", 
                                    @"^[A-Z][-a-zA-Z]{1,63}$",
                                };
 
            String[] fields = {"name", 
                               "surname",
                              };
            String[] results = { "", "" };

            for (int i = 0; i < 2; i++)
            {
                bool check = false;
                String inputString = "";
                while (!check)
                {
                    Console.Write("Press " + fields[i] + " of teacher:\n>");
                    inputString = Console.ReadLine();
                    if (inputString == "back")
                    {
                        MainMenu();
                    }
                    check = Regex.IsMatch(inputString, patterns[i]);
                    if (!check)
                    {
                        Console.WriteLine("Incorrect " + fields[i] + ". Try again Please!");
                    }
                }
                results[i] = inputString;
            }
            teacher.Name = results[0];
            teacher.Surname = results[1];
            return teacher;
        }

        private void Exit()
        {
            Thread StudentSaveThread = new Thread(() =>
            {
                StudentRepository.Save();
            });

            Thread GroupSaveThread = new Thread(() =>
            {
                GroupRepository.Save();
            });

            Thread SubjectSaveThread = new Thread(() =>
            {
                SubjectRepository.Save();
            });

            Thread TeacherSaveThread = new Thread(() =>
            {
                TeacherRepository.Save();
            });

            StudentSaveThread.Start();
            GroupSaveThread.Start();
            SubjectSaveThread.Start();
            TeacherSaveThread.Start();

            while (StudentSaveThread.IsAlive || GroupSaveThread.IsAlive
                || SubjectSaveThread.IsAlive || TeacherSaveThread.IsAlive)
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(300);
                    Console.Write(".");
                }
            }
        }
    }
}
