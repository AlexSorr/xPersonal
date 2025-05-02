SELECT 
    u.id, u.username, ui.id, up."name", us."level" 
FROM "user" u 
    JOIN userinfo UI ON U.ID = UI._userid
    JOIN userstat US ON U.Id = US._userid
    JOIN userparameters UP ON US._parameter = UP.id
ORDER BY u.id ASC

SELECT * FROM userparameters