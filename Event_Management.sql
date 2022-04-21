select u.Username , e.EventTime , h.Hall_Name , h.Location , h.Description 
from Users u inner join Events e on u.Username = e.User_ID  
inner join Halls h on e.Hall_ID =  h.HallId where u.Username = 'marks';


--  SELECT [e].[ID], [e].[EventName], [e].[EventTime], [e].[Hall_ID], [e].[User_ID], [h].[HallID], [h].[Description], [h].[Hall_Name], [h].[Location]
--       FROM [Events] AS [e]
--       INNER JOIN [Halls] AS [h] ON [e].[Hall_ID] = [h].[HallID]

-- var x = _context.Users
-- .Join(_context.Events,
--             user => user.Username,
--             events => events.Users.Username,
--             (user, events) => new
--             {
--                   User = user.Username,
--                   EventName = events.EventName,
--                   EventTime = events.EventTime,
--                   HallID = events.Hall_ID
--             }).ToListAsync();

-- var y = from u in _context.Users
--       join e in _context.Events on u.Username equals e.User_ID
--       join h in _context.Halls on e.Hall_ID equals h.HallID
--       select new
--       {
--             User = u.Username,
--             EventName = e.EventName,
--             EventTime = e.EventTime,
--             HallID = e.Hall_ID,
--             HallName = h.Hall_Name,
--             Location = h.Location,
--             Description = h.Description
--       } into a
--       select a;
-- return Ok(x);