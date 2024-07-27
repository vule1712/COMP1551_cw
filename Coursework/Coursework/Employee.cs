using System.Data.SqlClient;

namespace Coursework
{
    // Adding Roles
    public enum Role
    {
        Teacher,
        Student
    }
    // Create Employee class
    public class Employee
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

        public Employee(string name, string telephone, string email, Role role)
        {
            Name = name;
            Telephone = telephone;
            Email = email;
            Role = role;
        }

    }
    // Teacher class inherited from Employee class
    public class Teacher : Employee
    {
        // Add teacher class's attributes
        public decimal Salary { get; set; }
        public string Teach { get; set; }

        public Teacher(string name, string telephone, string email, decimal salary, string teach)
            : base(name, telephone, email, Role.Teacher)
        {
            Salary = salary;
            Teach = teach;
        }
        // Adding teacher to the database
        public void AddTeacher(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Teacher (Name, Telephone, Email, Salary, Teach, Role) VALUES (@Name, @Telephone, @Email, @Salary, @Teach, @Role)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Telephone", Telephone);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Salary", Salary);
                command.Parameters.AddWithValue("@Teach", Teach);
                command.Parameters.AddWithValue("@Role", Role.Teacher);
                command.ExecuteNonQuery();
            }
        }
        // Modify Teacher from the database
        public void ModifyTeacher(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Teacher SET Telephone = @Telephone, Email = @Email, Salary = @Salary, Teach = @Teach WHERE Name = @Name";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Telephone", Telephone);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Salary", Salary);
                command.Parameters.AddWithValue("@Teach", Teach);
                command.ExecuteNonQuery();
            }
        }
        // Remove Teacher from database
        public void RemoveTeacher(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Teacher WHERE Name = @Name";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);
                command.ExecuteNonQuery();
            }
        }
    }
    // Student class inherited from Employee class
    public class Student : Employee
    {
        public Student(string name, string telephone, string email)
            : base(name, telephone, email, Role.Student)
        {

        }
        // Add student to the database
        public void AddStudent(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Student (Name, Telephone, Email, Role) VALUES (@Name, @Telephone, @Email, @Role)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Telephone", Telephone);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Role", Role.Student);
                command.ExecuteNonQuery();
            }
        }
        // Modify Student from the database
        public void ModifyStudent(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Student SET Telephone = @Telephone, Email = @Email, Role = @Role WHERE Name = @Name";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Telephone", Telephone);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Role", Role.Student); // Assuming you want to keep the role as Student
                command.ExecuteNonQuery();
            }
        }
        // Remove Student from database
        public void RemoveStudent(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Student WHERE Name = @Name";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);
                command.ExecuteNonQuery();
            }
        }
    }
    // Create Subject class
    public class Subject
    {
        public string Name { get; set; }
        public string Status { get; set; }

        public Subject(string name, string status)
        {
            Name = name;
            Status = status;
        }
        // Add new subject to the database
        public void AddSubject(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Subject (Name, Status) VALUES (@Name, @Status)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Status", Status);
                command.ExecuteNonQuery();
            }
        }
        // Modify subject from database
        public void ModifySubject(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Subject SET Status = @Status WHERE Name = @Name";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Status", Status);
                command.ExecuteNonQuery();
            }
        }
        // Remove subject from database
        public void RemoveSubject(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Subject WHERE Name = @Name";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);
                command.ExecuteNonQuery();
            }
        }
    }

}
