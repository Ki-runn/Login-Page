<h2>This is an Employee Appraisal form web application developed using </h2>
<ul>
  <li>MVC Dotnet</li>
  <li>SSMS</li>
  <li>JavaScript</li>
  <li>HTML and CSS</li>
</ul>

<p>
  <h2>Overview</h2>
This project is an appraisal management system that allows users to log in, view appraisals, and manage appraisals based on their roles (HR, Employee, Manager). The application includes functionality for user authentication, role-based access control, and appraisal management.

Features
User Authentication: Users can log in with their username and password.
Role Management: The application supports different roles (HR, Employee, Manager) with specific functionalities for each role.
Appraisal Management: Users can view and manage appraisals. Managers have additional privileges to view and edit employee appraisals.
Navigation and Layout: The layout includes a logout button and a footer link to the login page.
Project Structure
Login Page
Managed by the Account controller with the Login action.
Includes a footer link that redirects to the login page.
Login Action
Redirects to the welcome page upon successful login.
Displays a popup saying "username invalid" on an invalid login attempt.
Login Model
Captures username, password, and remember me functionality.
User Role Management
Added a "Role" column to the "Users" table in the database.
Roles include HR, Employee, and Manager.
Code to retrieve the manager ID for a specific user.
ViewAppraisals Action
Fetches appraisals for the logged-in user, filtering by employee or manager ID.
Role-based logic to show/hide and enable/disable buttons based on the user’s role and appraisal status.
EditEmployeeAppraisal Action
Fetches and displays an appraisal for editing based on its ID.
Redirects to the ViewAppraisals method if the logged-in user is the manager of the appraisal.
Layout and Navigation
Modified _Layout.cshtml to remove unnecessary navigation links and include a logout button.
Footer updated to include a link to the login page.</p>

<h2>Installation</h2>

<p>Clone the repository:

git clone [https://github.com/username/repository.git](https://github.com/Ki-runn/Login-Page
)
cd repository
Set up the database:

Ensure your database server is running.
<h2>Open the application</h2>
<p>
Navigate to http://localhost:5000 in your web browser.
Usage
Logging In
Navigate to the login page.
Enter your username and password.
Click the login button.
If credentials are valid, you will be redirected to the welcome page.
Viewing Appraisals
Logged-in users can view appraisals based on their roles.
Employees can see their own appraisals.
Managers can see appraisals for their team members.
HR can see appraisals for all employees.
Editing Appraisals
Managers can edit appraisals for their team members.
Navigate to the Edit Appraisal page by clicking the edit button next to the appraisal.</p>
Update the connection string in appsettings.json.
Run the application:

dotnet run</p>
<h2>Contributing
Fork the repository.
<p>
<h3>Create a new branch:</h3>
git checkout -b feature/your-feature
<h3>Make your changes.</h3>
Commit your changes:
git commit -m "Add your message"
<h3>Push to the branch:</h3>
git push origin feature/your-feature
Create a pull request.
<h2>SSMS Setup></h2>
<h3>Create a Database in SSMS naming it as LoginAppraisalDemo </h3>
<p>Create a table named Employees and add an entry with name,email,role,pno,manager id,password and then save the changes</p>
<p>You can then login as desired employee if the data exist in DBMS </p>
<h3>Make sure to replace server instance and database name in appsettings.json file</h3>
<h3>Use microsoft.EntityFramework core</h3>
