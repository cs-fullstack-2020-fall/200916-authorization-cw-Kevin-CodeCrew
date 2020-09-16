# 200916 Authetication/Authorization CRUD CW

### SetUp

1. Create a new MVC application called `authorizationcw` *with* ASP.NET Identity
`dotnet new mvc -o authorizationcw --auth individual`

2. Complete wiring in `startup.cs` and add Razor view compilation
```
services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
  .AddRoles<IdentityRole>() // Add this line
  .AddEntityFrameworkStores<ApplicationDbContext>();

// Razor compilation
services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Configure default authorization policy
services.AddMvc(obj => 
{
   AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
});
```

### 3. Model
Create a new model called `GameModel`
- id : primary key
- Title : required, must be under 100 characters
- Description : required
- Publisher : required
- Rating : must be a number from 1 - 5 (try to display in a unique way in views)
- NOTE: Define unique display names and error messages for each field (except primary key)

### 4. Controller
Create a new Controller for all Game endpoints 
- View All Games
- View a Game's Details (link each game listed to this details view)
- Create a Game
    - endpoint to view form (protected)
    - endpoint to perform db create
- Update an Existing Game
    - endpoint to view form (protected)
    - endpoint to perform db update
- Delete a Game
    - endpoint to view page (protected)
    - endpoint to perform db delete 
- Test accessible endpoints in postman

### 5. Views
- Update the `Startup.cs` file to route to the endpoint that lists all Games by default
- Remove the Privacy Page navigation from the nav bar and add navigation for adding a Game to nav bar
- View
    - Create a partial to display Game properties
    - Create a view to display all Games using a partial with a button to view details
    - Create a Game details view that displays a Games properties using a partial with a button to update and delete the Game
- Create and Update
    - Create a partial to display the form fields required to create and update a Game
    - Create a view that displays a model bound form using a partial submitting to the appropriate endpoint to create a Game
    - Create a view that displays a model bound form using a partial submitting to the appropriate endpoint to update a Game
- Create a view that displays a confirmation to delete a Game

### 6. Authorization
Use appropriate Roles to restrict access to different views:

Use SQL to insert 3 `roles` for `admin`, `employee`, `user` and assign 1 user to each role

Update the controller to user roles:
- give `user` role only access to list all Games and view Game details
- give `employee` role access to everything **but** delete Game
- give `admin` role access to everything

### EXTRA
Styling:
- Use bootstrap to style views
- Style validation messages to display in red text
- Style invalid form field to display with a red border

Client-side Validation:
- Implement client side validation by adding required script files to `_Layout` file

Seed Data:
- Create a migration that will automatically populate the `AspNetRoles` table with the 3 roles but does not automatically assign users to roles
