using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
      public class BiddingController : ControllerBase
      {
            private readonly Context _context;
            public BiddingController(Context context)
            {
                  _context = context;
            }

            /*
            Scenerio : 
                  The Event Contains Null Value for PhotoGrapherID && CateringId
                  is Considered For Event Bidding 

            TestCase : 
                  63a20efa-c938-4f67-9200-9025628dc113
                  45c44bba-c548-40af-9b65-24581d2be5fb
            */

            #region User_Acceptance_Page
            [HttpGet("Get_Catering_Bidders/{EveId}")]
            public async Task<IActionResult> GetCateringBids(string EveId)
            {
                  try
                  {
                        var g = new Guid(EveId);
                        var x = await (from e in _context.Event_Bidding
                                       join u in _context.Users on e.BidderId equals u.Username
                                       where e.EventId == g && e.Role == "catering"
                                       select new { e, u })
                                    .Select(s => new
                                    {
                                          BidId = s.e.BidId,
                                          EventId = s.e.EventId,
                                          BidDescription = s.e.BidDescription,
                                          CatererId = s.e.BidderId,
                                          Name = s.u.Name,
                                          CatererTeamName = s.u.CateringIds.CatererTeamName,
                                          ProfileImg = s.u.ProfileImg,
                                          DisplayImg = s.u.CateringIds.DisplayImg,
                                          Phone = s.u.Phone,
                                          Email = s.u.Email,
                                          Location = s.u.Location,
                                          Experience = s.u.CateringIds.Experience,
                                          OverAllRating = _context.Events.Where(w => w.CateringId == s.u.CateringIds.CatererUserId).Any()
                                          ? Math.Round((from r in _context.Reviews
                                                        join e in _context.Events on r.ReviewID equals e.EventID
                                                        where e.CateringId == s.u.CateringIds.CatererUserId
                                                        select r.CateringRating).Average(), 1) : 0.0
                                    })
                                    .ToListAsync();
                        return Ok(x);
                  }
                  catch (Exception e)
                  {
                        return BadRequest(e.StackTrace);

                  }
            }

            [HttpGet("Get_PhotoGrapher_Bidders/{EveId}")]
            public async Task<IActionResult> GetPhotoGrapherBids(string EveId)
            {
                  try
                  {
                        var g = new Guid(EveId);
                        var x = await (from e in _context.Event_Bidding
                                       join u in _context.Users on e.BidderId equals u.Username
                                       where e.EventId == g && e.Role == "photographer"
                                       select new { e, u })
                                    .Select(s => new
                                    {
                                          BidId = s.e.BidId,
                                          EventId = s.e.EventId,
                                          BidDescription = s.e.BidDescription,
                                          CatererId = s.e.BidderId,
                                          Name = s.u.Name,
                                          PhotoGrapherTeamName = s.u.PhotoGrapherIds.PhotographerUserId,
                                          ProfileImg = s.u.ProfileImg,
                                          DisplayImg = s.u.PhotoGrapherIds.DisplayImg,
                                          Phone = s.u.Phone,
                                          Email = s.u.Email,
                                          Location = s.u.Location,
                                          Experience = s.u.PhotoGrapherIds.Experience,
                                          OverAllRating = _context.Events.Where(w => w.PhotoGrapherID == s.u.PhotoGrapherIds.PhotographerUserId).Any()
                                          ? Math.Round((from r in _context.Reviews
                                                        join e in _context.Events on r.ReviewID equals e.EventID
                                                        where e.PhotoGrapherID == s.u.PhotoGrapherIds.PhotographerUserId
                                                        select r.PhotoRating).Average(), 1) : 0.0
                                    })
                                    .ToListAsync();
                        return Ok(x);
                  }
                  catch (Exception e)
                  {
                        return BadRequest(e.StackTrace);

                  }
            }
            #endregion

            #region Business_User_Show_Events 
            [HttpGet("Show_Photography_Events_For_Bidding")]
            public async Task<IActionResult> Show_PhotoGraphy_Events_For_Bidding()
            {
                  try
                  {
                        var x = await (_context.Events.Where(e => e.PhotoGrapherID == null))
                              .Select(s => new
                              {
                                    EventId = s.EventID,
                                    EventName = s.EventName,
                                    EventTime = s.EventTime,
                                    Hall = s.Halls.Hall_Name,
                                    Location = s.Halls.Location,
                                    HallDescription = s.Halls.Description
                              })
                              .ToListAsync();
                        return Ok(x);
                  }
                  catch (Exception e)
                  {
                        return BadRequest(e.StackTrace);

                  }
            }
            [HttpGet("Show_Catering_Events_For_Bidding")]
            public async Task<IActionResult> Show_Catering_Events_For_Bidding()
            {
                  try
                  {
                        var x = await (_context.Events.Where(e => e.CateringId == null))
                              .Select(s => new
                              {
                                    EventId = s.EventID,
                                    EventName = s.EventName,
                                    EventTime = s.EventTime,
                                    Hall = s.Halls.Hall_Name,
                                    Location = s.Halls.Location,
                                    HallDescription = s.Halls.Description
                              }).ToListAsync();
                        return Ok(x);
                  }
                  catch (Exception e)
                  {
                        return BadRequest(e.StackTrace);
                  }
            }


            #endregion

            #region User_Region
            [HttpPut("Accept_The_Caterer_Best_Bidder")]
            public async Task<IActionResult> Accept_the_Best_Caterer_Bidder([FromBody] BidAccept bid)
            {
                  try
                  {
                        if (bid != null)
                        {
                              var e = await _context.Events.AsNoTracking()
                              .FirstOrDefaultAsync(e => e.EventID == bid.EventId);


                              if (e != null)
                              {
                                    e.CateringId = bid.BidderId;
                                    _context.Events.Update(e);
                                    _context.SaveChanges();
                                    return Ok("Catering Bidder Accepted");
                              }
                              return BadRequest("Invalid Request 2");
                        }
                        return BadRequest("Invalid Request 1");

                  }
                  catch (Exception e)
                  {
                        return BadRequest(e.StackTrace);
                  }
            }

            [HttpPut("Accept_The_Best_Photographer_Bidder")]
            public async Task<IActionResult> Accept_the_Best_Photographer_Bidder([FromBody] BidAccept bid)
            {
                  try
                  {
                        if (bid != null)
                        {
                              var e = await _context.Events.AsNoTracking()
                              .FirstOrDefaultAsync(e => e.EventID == bid.EventId);

                              if (e != null)
                              {

                                    e.PhotoGrapherID = bid.BidderId;
                                    _context.Events.Update(e);
                                    _context.SaveChanges();
                                    return Ok("PhotoGrapher Bidder Accecpted");
                              }
                              return BadRequest("Invalid Request 2");
                        }
                        return BadRequest("Invalid Request 1");

                  }
                  catch (Exception e)
                  {
                        return BadRequest(e.StackTrace);
                  }
            }

            #endregion
      }
}