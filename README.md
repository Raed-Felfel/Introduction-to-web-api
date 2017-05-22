# Introduction to web API
in this tutorial you'll learn
- creaing a simple API controller with two methods
- calling the methods using the browser
- route customization

### Create a new web API blank application
-	Open visual studio
- From the file menu, select new, then project. Or press Ctrl+Shift+N
- Select the Asp.Net Web Application from the dialog box. Give it a name and click ok.
- Select the Empty template, tick the API checkbox and click ok.

### Add a data model
- Right click the Models folder, select New, Class from the menu.
- Name your class Student and define its property as below:

```
namespace WebApi1.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string FullName { get; set; }
    }
}
```
### Create the first API controller
- Right click the controller folder and select add then controller menu item.
- From the dialog box select the Web API 2 Controller – Empty template.
- Name it StudentsController and click Add.
 - Write the below code which defines an in memory array of student to simplify the code. Then define 2 different actions on to return all the array and the other one to return a specific element in the array.
```
public class StudentsController : ApiController
    {
        Student[] students = new Student[]
        {
            new Student {Id=1, Grade=1,FullName="Mohammad Ali" },
            new Student {Id=2, Grade=1,FullName="Omar Habib" },
            new Student {Id=3, Grade=1,FullName="Shadi Jamil" },
            new Student {Id=4, Grade=2,FullName="Kamel bbadri" },
            new Student {Id=5, Grade=2,FullName="Khali Ahmad" }
        };

        public IEnumerable<Student> GetAllStudents()
        {
            return students;
        }

        public IHttpActionResult GetStudent(int id)
        {
            var student = students.FirstOrDefault(std => std.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
}
```
### Test using explorer
- My application root was “http://localhost:51456/” .
- So open the explorer and write http://localhost:51456/api/Students/ to test the first method. 
- And http://localhost:51456/Api/Students/1 to test the second one.

### Discussing the routes
To understand the default routing open the file WebApiConfig.cs in App_Start folder . and note the below route pattern
```
config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
    );
```
This tells the application to accept routes of three parts
- start by api prefix 
- controller name in the second part
- and optionally id parameter as a third part

### http verbs
- By default; when the action name starts by get the controller suppose it accepts get requests without looking to the action name. 
- if we have more than one get actions in the controller, it will look to parameters signatures to distinguish between them. 
- so in our example it was enough to include the controller name and invoke a get request. 
- when we have a parameter in the request the controller mapped the request to "GetStudent" action. 
- and mapped the request to "GetAllStudents" when the request provided no parameters.

### Including the action name in the controller
Sometimes you may need to include the action name inside the URI to explicitly enable the client to choose the action instead of the default behaviour which map the action according to the verb (first some letters of the action name) and the parameters. To do this open WebApiConfig.cs and change to the below settings:

```
config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
);
```
Now the previously used URIs will not work
- http://localhost:51456/api/Students (not working)
- http://localhost:51456/api/Students/getallstudents (will work)

### Explicitly customize controller and action names
MVC Api comes with two useful filters one to define the controller route called “RoutePrefix("")” and the other one is used to define an alternative action name “Route("")” the below demonstrate the use:
```
[RoutePrefix("api/stds")]
    public class StudentsController : ApiController
    {
        ...

        [Route("all")]
        public IEnumerable<Student> GetAllStudents()
        {
            return students;
        }

        [Route("one")]
        public IHttpActionResult GetStudent(int id)
        {
            var student = students.FirstOrDefault(std => std.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
}
```
Here are the working URIs for the above code
- http://localhost:51456/api/stds/all
- http://localhost:51456/api/stds/one/2

Next tutorial will explain the verbs and model binding.
