
use Wedding_system;

-- Show All Events
select u.Username , e.EventTime , h.Hall_Name , h.Location , h.Description 
from Users u inner join Events e on u.Username = e.User_ID  
inner join Halls h on e.Hall_ID =  h.HallId where u.Username = 'marks';


-- Show Users Only Reviews
select e.User_ID  , e.EventName , h.Hall_Name , r.ReviewContent  from Reviews r 
inner join  Events e on e.ID = r.EventID 
inner join Halls h on h.HallID = r.HallID 
where e.User_ID = 'vinosiva'; 



