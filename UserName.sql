SELECT UserName, Name FROM UsersRoles
INNER JOIN Users ON UsersRoles.UserId = Users.UserId
INNER JOIN Roles On UsersRoles.RoleId = Roles.RoleId



SELECT B.UserName, C.Name FROM UsersRoles A 
INNER JOIN Users B ON A.UserId = B.UserId
INNER JOIN Roles C ON A.RoleId = C.RoleId