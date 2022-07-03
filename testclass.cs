using Xunit;
using System.Linq;
using Xunit.Abstractions;

public class testclass
{
    [Fact]
    public void TestAddMeeting()
    {
        var meetingRegister = new meetings.MeetingRegister();
        var attendant = new meetings.Attendants("Kestas");
        List<meetings.Attendants> list = new List<meetings.Attendants>();
        list.Add(attendant);
        var date1 = new DateTime(2008, 3, 1, 7, 0, 0);
        var date2 = new DateTime(2008, 3, 1, 7, 0, 0);
        var meeting = new meetings.Meeting("Meetingas", "Kestas", "HubMeeting", "Hub", "Live", date1, date2, list);
        meetingRegister.AddMeeting(meeting);
        Assert.Equal(meetingRegister.GetName(0), "Meetingas");
    }

    [Fact]
    public void TestDeleteMeeting()
    {
        var meetingRegister = new meetings.MeetingRegister();
        var meetingRegister2 = new meetings.MeetingRegister();
        var attendant = new meetings.Attendants("Kestas");
        List<meetings.Attendants> list = new List<meetings.Attendants>();
        list.Add(attendant);
        var date1 = new DateTime(2008, 3, 1, 7, 0, 0);
        var date2 = new DateTime(2008, 3, 1, 7, 0, 0);
        var meeting = new meetings.Meeting("Meetingas", "Kestas", "HubMeeting", "Hub", "Live", date1, date2, list);
        meetingRegister.AddMeeting(meeting);
        meetingRegister.DeleteMeeting(0, "Kestas");
        Assert.DoesNotContain("Kestas", meetingRegister.GetName(0));
    }
    
    [Theory]
    // 2pass;1fail
    [InlineData("Kestas")]
    [InlineData("Gedas")]
    [InlineData("Kestutis")]
    public void TestAddPersonToMeeting(string value)
    {
        var meetingRegister = new meetings.MeetingRegister();
        var attendant = new meetings.Attendants("Kestas");
        var attendantTest = new meetings.Attendants(value);
        List<meetings.Attendants> list = new List<meetings.Attendants>();
        list.Add(attendant);
        var date1 = new DateTime(2008, 3, 1, 7, 0, 0);
        var date2 = new DateTime(2008, 3, 1, 7, 0, 0);
        var meeting = new meetings.Meeting("Meetingas", "Kestas", "HubMeeting", "Hub", "Live", date1, date2, list);
        meetingRegister.AddMeeting(meeting);
        meetingRegister.AddPersonToMeeting(attendantTest, 0);
        Assert.Equal(2, meetingRegister.GetPeople(0));
    }

    [Fact]
    public void TestRemovePersonFromMeeting()
    {
        var meetingRegister = new meetings.MeetingRegister();
        var attendant = new meetings.Attendants("Kestas");
        var attendant1 = new meetings.Attendants("Gedas");
        var attendant2 = new meetings.Attendants("Kestutis");
        List<meetings.Attendants> list = new List<meetings.Attendants>();
        list.Add(attendant);
        var date1 = new DateTime(2008, 3, 1, 7, 0, 0);
        var date2 = new DateTime(2008, 3, 1, 7, 0, 0);
        var meeting = new meetings.Meeting("Meetingas", "Kestas", "HubMeeting", "Hub", "Live", date1, date2, list);
        meetingRegister.AddMeeting(meeting);
        meetingRegister.AddPersonToMeeting(attendant1, 0);
        meetingRegister.AddPersonToMeeting(attendant2, 0);
        meetingRegister.RemovePersonFromMeeting(0, 2);
        Assert.Equal(2, meetingRegister.GetPeople(0));
    }

    [Fact]
    public void FailingTestRemovePersonFromMeeting()
    {
        var meetingRegister = new meetings.MeetingRegister();
        var attendant = new meetings.Attendants("Kestas");
        var attendant1 = new meetings.Attendants("Gedas");
        var attendant2 = new meetings.Attendants("Kestutis");
        List<meetings.Attendants> list = new List<meetings.Attendants>();
        list.Add(attendant);
        var date1 = new DateTime(2008, 3, 1, 7, 0, 0);
        var date2 = new DateTime(2008, 3, 1, 7, 0, 0);
        var meeting = new meetings.Meeting("Meetingas", "Kestas", "HubMeeting", "Hub", "Live", date1, date2, list);
        meetingRegister.AddMeeting(meeting);
        meetingRegister.AddPersonToMeeting(attendant1, 0);
        meetingRegister.AddPersonToMeeting(attendant2, 0);
        meetingRegister.RemovePersonFromMeeting(0, 0);
        Assert.Equal(2, meetingRegister.GetPeople(0));
    }
}