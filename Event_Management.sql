select u.Username , e.EventTime , h.Hall_Name , h.Location , h.Description 
from Users u inner join Events e on u.Username = e.User_ID
inner join Halls h on e.Hall_ID =  h.HallId;