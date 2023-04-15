//aggregation, composition,
//realization, and generalization demo
//call the URL below to view the UML diagram

//To execute, open a terminal and call [dotnet run] from the same directory as Program.cs
//or execute in an IDE such as VS Code or Visual Studio or JetBrains Rider

//http://www.plantuml.com/plantuml/svg/bLBDJjj04BxxAKRf1IYOzegY29G3egfKYOgRBuiztgnOprYxuz2MykvT_smsDbKh8kNWcVr-lfcvzgAST8tDMbK-AOtmuD71IvO1619qjIeHxgOYzBz7PWjVMHiwwKAPL8Fdq2hldzbLfnd0Q_WJ2uB5Mgx6fz2WxBW2RpGfwHoU5qGCfcajDaYYn33DDRzBLuLEAAf7BqwL0gKDCd1avkExtTn4hfubwVk2nf39WA8AMiTxKw67agbqA5ZGt39mFGjOa33SepQzYXiubBKmPG7vsQ9_aoYserDdz2vOZI13EtH6nk05hSqcqAulpUR8_c5SUK5RonwzmFR3vHUrLw0n23CqdHKJtaS-HINsQnvpht_d-I0GJJ21DVHq1K7l7fKrluR0lMwFca0N5ybyggK9308noA0HFq8ZrGFaWIfqsgcgKuA9yusNMoHqCpi5dHn5EbqItouui-mrErpxbZldSRYc3CPjJBLlTHqeISFBq3gGUWWd7SAjOwpDS6G9dHXXGUKdvBmQyH6v-Wmhu61yzIdFprPBEt-RoOtM3lNHG1OOpUY1E-clrPVS8ZmRsKtCG_I5IZ2nWhFauUuV2zboqx8tIJnE7t-4i1vmZmIcXlzvwlToZLHrZVq3	

IUser undergraduateStudent = new UndergraduateStudent();
IUser graduateStudent = new GraduateStudent();
IUser registrar = new Registrar();

//create a list of IUser
var users = new List<IUser>() { 
    undergraduateStudent,
    graduateStudent,
    registrar
};

//login each by type (not actually doing anything here)
foreach (var user in users) {
    user.Login(user.GetType().Name, "password");
}

//create a course
Course? courseSwe3313 = new Course();

//create a course section and
//add section to course
courseSwe3313.CourseOfferings.Add(new CourseOffering());

//add students to course
courseSwe3313.CourseOfferings[0].Students.Add((Student)undergraduateStudent);
courseSwe3313.CourseOfferings[0].Students.Add((Student)graduateStudent);
//have to cast the students to Student type
//because they are assigned to a higher level type, IUser

//output course details
void OutputCourseDetails(Course course)
{
    Console.WriteLine($"Course has {course.CourseOfferings.Count} CourseOfferings");
    for (int courseOfferingIdx = 0; courseOfferingIdx < course.CourseOfferings.Count; courseOfferingIdx++)
    {
        Console.WriteLine($"Course Offering {courseOfferingIdx + 1} has {course.CourseOfferings[courseOfferingIdx].Students.Count} students");
    }
}
OutputCourseDetails(courseSwe3313);

//destroy course and related course offerings.
//Course to CourseOffering is an *aggregation*
//so destroying the course destroys the offerings.
//CourseOffering holds students in a composition
//relationship. No student records will go out of scope.
courseSwe3313 = null;

//prove it's dead by calling OutputCourseDetails again
//put in a try block - this will fail
try
{	        
    //this will create a warning in the output...
    //c# is telling us this is probably going to explode
    //warning CS8604: Possible null reference argument for parameter 'course' in 'void OutputCourseDetails(Course course)'.
    OutputCourseDetails(courseSwe3313);
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message} (NOTE: This is expected behavior)");
}

//demonstrate students still exist after course is destroyed
Console.WriteLine((undergraduateStudent.GetType().Name));
Console.WriteLine((graduateStudent.GetType().Name));


///////////////////////////////////
// START OF CLASSES
///////////////////////////////////

public interface IUser
{
	bool Login(string username, string password);
}

public abstract class User : IUser
{
	public bool Login(string username, string password) {
		Console.WriteLine($"{username} is logged in!");
		return true;
	}
}

public interface IStudent { }

public abstract class Student : User, IStudent { }

public class GraduateStudent : Student { }

public class UndergraduateStudent : Student { }

public class Registrar : User { }

public class Course { 
	public List<CourseOffering> CourseOfferings { get; } = new();
}

public class CourseOffering {
	public List<IStudent> Students { get; } = new();
}

//plantuml

/*

http://www.plantuml.com/plantuml/uml

@startuml

package User {

  interface IUser
  interface IStudent
  abstract class User
  abstract class Student

  IUser <|.. User

  User <|-- Student
  User <|-- Registrar

  IStudent <|.. Student
  Student <|-- UndergraduateStudent
  Student <|-- GraduateStudent

}

package Course {
  
  class CourseOffering {  
    Students : List<IStudent>
  }

  class Course {
    CourseOfferings: List<CourseOffering>
  }

  Course "1" o-- "0..*" CourseOffering
  
  CourseOffering "1" *-- "0..*" IStudent

}
@enduml
*/