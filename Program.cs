using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;


namespace meetings
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"meetings.json";

            Console.WriteLine("Please enter your name:");
            string user = Console.ReadLine();

            Console.WriteLine("Select operation:");
            Console.WriteLine("1 Create new meeting");
            Console.WriteLine("2 Delete a meeting");
            Console.WriteLine("3 Add a person to the meeting");
            Console.WriteLine("4 Remove a person from the meeting");
            Console.WriteLine("5 Filter the meetings by description");
            Console.WriteLine("6 Filter the meetings by responsible person");
            Console.WriteLine("7 Filter the meetings by category");
            Console.WriteLine("8 Filter the meetings by type");
            Console.WriteLine("9 Filter the meetings by starting date");
            Console.WriteLine("0 Filter the meetings by number of attendees");
            Console.WriteLine("x close");

            MeetingRegister meetingRegister = new MeetingRegister();
            if (new FileInfo(fileName).Length != 0)
            {
                meetingRegister = JsonConvert.DeserializeObject<MeetingRegister>(File.ReadAllText(fileName));
            }

            var userInput = Console.ReadLine();
            while (true)
            {
                switch (userInput)
                {
                    case "1":
                        DateTime startDate = new DateTime();
                        DateTime endDate = new DateTime();
                        List<Attendants> list = new List<Attendants>();

                        Console.WriteLine("Enter meeting name:");
                        var name = Console.ReadLine();
                        Console.WriteLine("Enter responsible person for meeting:");
                        var responsiblePerson = Console.ReadLine();
                        Attendants attendant = new Attendants(responsiblePerson.ToString());
                        list.Add(attendant);
                        Console.WriteLine("Enter description of the meeting:");
                        var description = Console.ReadLine();
                        Console.WriteLine("Enter category of the meeting (CodeMonkey, Hub, Short, Teambuilding):");
                        var category = Console.ReadLine();
                        Console.WriteLine("Enter type of the meeting (Live, InPerson):");
                        var type = Console.ReadLine();

                        Console.WriteLine("Enter start date of the meeting(yyyy-M-dd):");
                        while (true)
                        {
                            try
                            {
                                startDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-M-dd", null);
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Wrong start date input, try again");
                            }
                        }
                        Console.WriteLine("Enter end date of the meeting(yyyy-M-dd):");
                        while (true)
                        {
                            try
                            {
                                endDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-M-dd", null);
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Wrong end date input, try again");
                            }
                        }

                        Meeting meeting = new Meeting(name.ToString(), responsiblePerson.ToString(), description.ToString(), category, type, startDate, endDate, list);
                        if (meeting.Category == "error" || meeting.Type == "error")
                        {
                            Console.WriteLine("Error in making meeting");
                        }
                        else
                        {
                            meetingRegister.AddMeeting(meeting);
                        }
                        break;
                    case "2":
                        int input;
                        Console.WriteLine("Choose meeting to delete");
                        meetingRegister.DisplayMeetings();

                        while (true)
                        {
                            try
                            {
                                input = Convert.ToInt32(Console.ReadLine());
                                meetingRegister.DeleteMeeting(Convert.ToInt32(input) - 1, user.ToString());
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Wrong input");
                                break;
                            }
                        }
                        break;
                    case "3":
                        int inputt;
                        Console.WriteLine("What person you want to add");
                        string person = Console.ReadLine();
                        Attendants attendantt = new Attendants(person);
                        Console.WriteLine("Choose meeting to where add a person");
                        meetingRegister.DisplayMeetings();

                        while (true)
                        {
                            try
                            {
                                inputt = Convert.ToInt32(Console.ReadLine());
                                meetingRegister.AddPersonToMeeting(attendantt, Convert.ToInt32(inputt) - 1);
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Wrong input");
                                break;
                            }
                        }
                        break;
                    case "4":
                        int meetingId;
                        int personId;
                        Console.WriteLine("Choose meeting from where remove a person");
                        meetingRegister.DisplayMeetings();

                        while (true)
                        {
                            try
                            {
                                meetingId = Convert.ToInt32(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Wrong input");
                            }
                        }
                        Console.WriteLine("Choose a person to remove");
                        meetingRegister.DisplayPeople(meetingId - 1);

                        while (true)
                        {
                            try
                            {
                                personId = Convert.ToInt32(Console.ReadLine());
                                meetingRegister.RemovePersonFromMeeting(meetingId - 1, personId - 1);
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Wrong input");
                            }
                        }
                        break;
                    case "5":
                        Console.WriteLine("Enter description to filter by");
                        var filterDescription = Console.ReadLine();
                        meetingRegister.FilterMeetingsByDescription(filterDescription.ToString());
                        break;
                    case "6":
                        Console.WriteLine("Enter responsible person of meeting to filter by");
                        var filterPerson = Console.ReadLine();
                        meetingRegister.FilterMeetingsByResponsiblePerson(filterPerson);
                        break;
                    case "7":
                        Console.WriteLine("Enter category of meeting to filter by");
                        var filterCategory = Console.ReadLine();
                        meetingRegister.FilterMeetingsByCategory(filterCategory);
                        break;
                    case "8":
                        Console.WriteLine("Enter type of meeting to filter by");
                        var filterType = Console.ReadLine();
                        meetingRegister.FilterMeetingsByType(filterType);
                        break;
                    case "9":
                        Console.WriteLine("Enter starting date of meeting(yyyy-M-dd) to filter by");

                        while (true)
                        {
                            try
                            {
                                DateTime filterDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-M-dd", null);
                                meetingRegister.FilterMeetingsByStartingDate(filterDate);
                                break;

                            }
                            catch
                            {
                                System.Console.WriteLine("Wrong input");
                            }
                        }
                        break;
                    case "0":
                        Console.WriteLine("Enter number of attendees of meeting to filter by");

                        while (true)
                        {
                            try
                            {
                                int filterAttendees = Convert.ToInt32(Console.ReadLine());
                                meetingRegister.FilterMeetingsByNumberOfAttendees(filterAttendees);
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Wrong input");
                                break;
                            }
                        }
                        break;
                    case "x":
                        string serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(meetingRegister, Formatting.Indented);
                        
                        using (StreamWriter sw = new StreamWriter(fileName))
                        {
                            sw.Write(serializedObject);
                        }
                        return;
                    default:
                        Console.WriteLine("Select valid operation.");
                        break;
                }
                Console.WriteLine("Select operation:");
                userInput = Console.ReadLine();
            }
        }
    }
    class MeetingRegister
    {
        [JsonProperty]
        private List<Meeting> meetingRegister { get; set; } = new List<Meeting>();

        public string GetName(int id)
        {
            if (meetingRegister.Count() > 0)
                return meetingRegister[id].name;
            else
                return "";
        }

        public int GetPeople(int id)
        {
            return meetingRegister[0].attendants.Count();
        }

        public MeetingRegister() { }

        public void AddMeeting(Meeting meeting)
        {
            meetingRegister.Add(meeting);
        }

        public void DeleteMeeting(int meetingId, string user)
        {
            if (meetingRegister.Contains(meetingRegister[meetingId]) && meetingRegister[meetingId].responsiblePerson == user)
            {
                meetingRegister.RemoveAt(meetingId);
                Console.WriteLine("Meeting has been deleted");
            }
            else
            {
                Console.WriteLine("Meeting can not be deleted");
            }
        }

        public void AddPersonToMeeting(Attendants attendant, int meetingId)
        {
            bool flag = false;

            for (int i = 0; i < meetingRegister[meetingId].attendants.Count(); i++)
            {
                if (meetingRegister[meetingId].attendants[i].name == attendant.name)
                {
                    flag = true;
                }
            }
            if (flag == true)
            {
                Console.WriteLine("Person is already at the meeting");
            }
            else if (flag == false)
            {
                meetingRegister[meetingId].attendants.Add(attendant);
                Console.WriteLine("{0} has been added to the meeting at {1}", attendant.name, DateTime.Now);
            }
        }

        public void RemovePersonFromMeeting(int meetingId, int personId)
        {
            if (meetingRegister[meetingId].responsiblePerson != meetingRegister[meetingId].attendants[personId].name)
            {
                Console.WriteLine("{0} has been removed", meetingRegister[meetingId].attendants[personId].name.ToString());
                meetingRegister[meetingId].attendants.RemoveAt(personId);

            }
            else
            {
                Console.WriteLine("Can not remove person that is responsible for meeting");
            }
        }

        public void FilterMeetingsByDescription(string description)
        {
            int counter = 1;
            for (int i = 0; i < meetingRegister.Count(); i++)
            {
                if (meetingRegister[i].description.Contains(description))
                {
                    Console.WriteLine("Nr: {0, -1} | Meeting name: {1, -25} | Responsible person: {2, -10} | Meeting category: {3, -13} | Meeting type: {4, -10} | Start date: {5, -15} | End date: {6, -15} | Attendants count: {7, -3}", counter, meetingRegister[i].name, meetingRegister[i].responsiblePerson,
                meetingRegister[i].Category, meetingRegister[i].Type, meetingRegister[i].startDate, meetingRegister[i].endDate, meetingRegister[i].attendants.Count());
                    counter++;
                }
            }
        }

        public void FilterMeetingsByResponsiblePerson(string person)
        {
            int counter = 1;
            for (int i = 0; i < meetingRegister.Count(); i++)
            {
                if (meetingRegister[i].responsiblePerson == person)
                {
                    Console.WriteLine("Nr: {0, -1} | Meeting name: {1, -25} | Responsible person: {2, -10} | Meeting category: {3, -13} | Meeting type: {4, -10} | Start date: {5, -15} | End date: {6, -15} | Attendants count: {7, -3}", counter, meetingRegister[i].name, meetingRegister[i].responsiblePerson,
                meetingRegister[i].Category, meetingRegister[i].Type, meetingRegister[i].startDate, meetingRegister[i].endDate, meetingRegister[i].attendants.Count());
                    counter++;
                }
            }
        }

        public void FilterMeetingsByCategory(string category)
        {
            int counter = 1;
            for (int i = 0; i < meetingRegister.Count(); i++)
            {
                if (meetingRegister[i].Category.ToLower() == category.ToLower())
                {
                    Console.WriteLine("Nr: {0, -1} | Meeting name: {1, -25} | Responsible person: {2, -10} | Meeting category: {3, -13} | Meeting type: {4, -10} | Start date: {5, -15} | End date: {6, -15} | Attendants count: {7, -3}", counter, meetingRegister[i].name, meetingRegister[i].responsiblePerson,
                meetingRegister[i].Category, meetingRegister[i].Type, meetingRegister[i].startDate, meetingRegister[i].endDate, meetingRegister[i].attendants.Count());
                    counter++;
                }
            }
        }

        public void FilterMeetingsByType(string type)
        {
            int counter = 1;
            for (int i = 0; i < meetingRegister.Count(); i++)
            {
                if (meetingRegister[i].Type.ToLower() == type.ToLower())
                {
                    Console.WriteLine("Nr: {0, -1} | Meeting name: {1, -25} | Responsible person: {2, -10} | Meeting category: {3, -13} | Meeting type: {4, -10} | Start date: {5, -15} | End date: {6, -15} | Attendants count: {7, -3}", counter, meetingRegister[i].name, meetingRegister[i].responsiblePerson,
                meetingRegister[i].Category, meetingRegister[i].Type, meetingRegister[i].startDate, meetingRegister[i].endDate, meetingRegister[i].attendants.Count());
                    counter++;
                }
            }
        }

        public void FilterMeetingsByStartingDate(DateTime date)
        {
            int counter = 1;
            for (int i = 0; i < meetingRegister.Count(); i++)
            {
                if (meetingRegister[i].startDate == date)
                {
                    Console.WriteLine("Nr: {0, -1} | Meeting name: {1, -25} | Responsible person: {2, -10} | Meeting category: {3, -13} | Meeting type: {4, -10} | Start date: {5, -15} | End date: {6, -15} | Attendants count: {7, -3}", counter, meetingRegister[i].name, meetingRegister[i].responsiblePerson,
                meetingRegister[i].Category, meetingRegister[i].Type, meetingRegister[i].startDate, meetingRegister[i].endDate, meetingRegister[i].attendants.Count());
                    counter++;
                }
            }
        }

        public void FilterMeetingsByNumberOfAttendees(int number)
        {
            int counter = 1;
            for (int i = 0; i < meetingRegister.Count(); i++)
            {
                if (meetingRegister[i].attendants.Count() >= number)
                {
                    Console.WriteLine("Nr: {0, -1} | Meeting name: {1, -25} | Responsible person: {2, -10} | Meeting category: {3, -13} | Meeting type: {4, -10} | Start date: {5, -15} | End date: {6, -15} | Attendants count: {7, -3}", counter, meetingRegister[i].name, meetingRegister[i].responsiblePerson,
                meetingRegister[i].Category, meetingRegister[i].Type, meetingRegister[i].startDate, meetingRegister[i].endDate, meetingRegister[i].attendants.Count());
                    counter++;
                }
            }
        }
        public void DisplayMeetings()
        {
            for (int i = 0; i < meetingRegister.Count(); i++)
            {
                Console.WriteLine("Nr: {0, -1} | Meeting name: {1, -25} | Responsible person: {2, -10} | Meeting category: {3, -13} | Meeting type: {4, -10} | Start date: {5, -15} | End date: {6, -15} | Attendants count: {7, -3}", i + 1, meetingRegister[i].name, meetingRegister[i].responsiblePerson,
                meetingRegister[i].Category, meetingRegister[i].Type, meetingRegister[i].startDate, meetingRegister[i].endDate, meetingRegister[i].attendants.Count());
            }
        }

        public void DisplayPeople(int meetingId)
        {
            for (int i = 0; i < meetingRegister[meetingId].attendants.Count(); i++)
            {
                Console.WriteLine("Nr: {0, -1} | Person name: {1}", i + 1, meetingRegister[meetingId].attendants[i].name);
            }
        }
    }
    class Meeting
    {
        public string name { get; set; }
        public string responsiblePerson { get; set; }
        public string description { get; set; }
        private string category;
        private string type;
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public List<Attendants> attendants;

        public Meeting() { }

        public Meeting(string aName, string aResponsiblePerson, string aDescription, string aCategory, string aType, DateTime aStartDate, DateTime aEndDate, List<Attendants> aAttendants)
        {
            name = aName;
            responsiblePerson = aResponsiblePerson;
            description = aDescription;
            Category = aCategory;
            Type = aType;
            startDate = aStartDate;
            endDate = aEndDate;
            attendants = aAttendants;
        }

        public string Category
        {
            get { return category; }
            set
            {
                if (value == "CodeMonkey" || value == "Hub" || value == "TeamBuilding" || value == "Short" || value == "codeMonkey" || value == "hub" || value == "teamBuilding" || value == "short")
                {
                    category = value;
                }
                else
                {
                    category = "error";
                }
            }
        }

        public string Type
        {
            get { return type; }
            set
            {
                if (value == "Live" || value == "InPerson" || value == "live" || value == "inPerson")
                {
                    type = value;
                }
                else
                {
                    type = "error";
                }
            }
        }
    }

    class Attendants
    {
        public string name { get; set; }

        public Attendants() { }

        public Attendants(string aName)
        {
            name = aName;
        }
    }
}
