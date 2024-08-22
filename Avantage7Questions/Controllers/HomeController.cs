using Avantage7Questions.Models;
using Avantage7Questions.Models.Tables;
using Avantage7Questions.Models.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using Avantage7Questions.Services;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Avantage7Questions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuestionsDbContext _db;
        private readonly TelegramBotService _bot;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, QuestionsDbContext db, TelegramBotService bot, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _db = db;
            _bot = bot;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IndexVM indexVM = new IndexVM()
            {
                Question = new Question(),
                Images = new List<Image>(),
            };
            indexVM.Question.Items = new Items();
            //indexVM.Question.QS = new List<QS>();
            //indexVM.Question.QS.Add(new QS());

            return View(indexVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Question question, List<IFormFile> files)
        {
            
            if (files != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                foreach (var file in files)
                {
                    var uploads = Path.Combine(wwwRootPath, "images");
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }
                    
                    var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);

                    var filePath = Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    //var urlFriendlyPath = filePath.Replace("\\", "/");
                    //urlFriendlyPath = ReplaceFirst(urlFriendlyPath, "/", "//");
                    //urlFriendlyPath = @"file:///" + @urlFriendlyPath;


                    Image image = new Image()
                    {
                        FilePath = @"\images\" + fileName,
                        QuestionId = question.Id,
                        Question = question
                    };
                    _db.Images.Add(image);


                    //using (var memoryStream = new MemoryStream())
                    //{
                    //    file.CopyTo(memoryStream);
                    //    var Data = memoryStream.ToArray();

                    //    Image image = new Image()
                    //    {
                    //        ImageInBytes = memoryStream.ToArray(),
                    //        QuestionId = question.Id,
                    //        Question = question
                    //    };
                    //    _db.Images.Add(image);
                    //}
                }
            }

            question.QS = new List<QS>();

            question.QS.Add(addNewStatus("New", question.Id));
            //question.QS.FirstOrDefault().Id = question.Id;
            //question.QS.FirstOrDefault().Status = _db.Statuses.FirstOrDefault(a => a.Name == "New");
            //question.QS.FirstOrDefault().Date = currentTime;

            _db.Itemses.Add(question.Items);
            //_db.QSes.Add(question.QS.FirstOrDefault());
            //_db.QSes.Add(qS);
            _db.Questions.Add(question);
            _db.SaveChanges();

            SendToBot(question.Name + " " + question.LastName, question.PhoneNumber, question.Data, getItemsString(question.Items), question.IsPhotos);


            return RedirectToAction("SuccessPost", new { number = question.PhoneNumber });
        }

        public string ReplaceFirst(string text, string searchChar, string replaceChar)
        {
            if (text == null) return null;

            var index = text.IndexOf(searchChar);
            if (index < 0) return text;

            return text.Substring(0, index) + replaceChar + text.Substring(index + 1);
        }
        public async Task SendToBot(string name, string phoneNumber, string description, string itemsString, bool isImages)
        {
            string img;
            if (isImages)
            {
                img = "так";
            }
            else
            {
                img = "ні";
            }
            string message = $"Ім'я: {name}\nНомер телефону: {phoneNumber}\nХоче замовити: {itemsString}\nОпис замовлення: {description}\nМістить картинки: {img}";
            long id = -1002157997326;
            await _bot.SendMessageAsync(id, message);
        }
        public IActionResult SuccessPost(string number)
        {
            SuccessPostVM successPostVM = new SuccessPostVM
            {
                Number = number
            };
            return View(successPostVM);
        }

        [Authorize]
        public IActionResult Storage()
        {
            return View();
        }

        public async Task<string> getAllQuestions()
        {
            //var questions = _db.Questions.Include("QS").Include("Items").OrderBy(e => e.Name).ToList();
            //var dbSetQuestions = _db.Set<Question>();

            var questions = await _db.Questions
                .Include(q => q.QS)
                    .ThenInclude(qs => qs.Status)
                .Include(q => q.Items)
                .OrderByDescending(q => q.QS.Min(qs => qs.Date))
                .ToListAsync();

            for (int i = 0; i < questions.Count; i++)
            {
                questions[i].ItemsString = getItemsString(questions[i].Items);
                questions[i].CurrentStatus = questions[i].QS.OrderByDescending(f => f.Date).FirstOrDefault().Status.Description;
                questions[i].LastUpdateDate = questions[i].QS.OrderByDescending(f => f.Date).FirstOrDefault().Date.ToString();
                questions[i].CreateQuestionDate = questions[i].QS.OrderBy(f => f.Date).FirstOrDefault().Date.ToString();
                questions[i].StatusBackgroundColor = questions[i].QS.OrderByDescending(f => f.Date).FirstOrDefault().Status.BackgroundColor;

                var img = _db.Images.Where(e => e.QuestionId == questions[i].Id);
                if(!img.Any())
                {
                    questions[i].IsPhotos = false;
                }
                else
                {
                    questions[i].IsPhotos = true;
                }
            };

            

            return JsonSerializer.Serialize(questions);
        }


        public async Task<string> getAllQuestionsForCalendar()
        {

            var questions = await _db.Questions
                .Include(q => q.QS)
                    .ThenInclude(qs => qs.Status)
                .Include(q => q.Items)
                .OrderByDescending(q => q.QS.Min(qs => qs.Date))
                .ToListAsync();

            questions.Take(50);

            for (int i = 0; i < questions.Count; i++)
            {
                questions[i].ItemsString = getItemsString(questions[i].Items);
                questions[i].StatusBackgroundColor = questions[i].QS.OrderByDescending(f => f.Date).FirstOrDefault().Status.BackgroundColor;
            };

            List<CalendarEvent> calendarEvents = new List<CalendarEvent>();

            foreach (Question question in questions)
            {
                CalendarEvent calendarEvent = new CalendarEvent();

                calendarEvent.title = question.ItemsString;
                calendarEvent.start = question.QS.OrderBy(f => f.Date).FirstOrDefault().Date.ToString("o");
                calendarEvent.backgroundColor = question.StatusBackgroundColor;
                calendarEvent.url = $"/Home/Info?id={question.Id}";
                calendarEvent.allDay = true;

                calendarEvents.Add(calendarEvent);
            }

            return JsonSerializer.Serialize(calendarEvents);
        }


        public QS addNewStatus(string statusName, int questionId)
        {
            DateTime currentTime = DateTime.Now;

            QS qS = new QS()
            {
                Status = _db.Statuses.FirstOrDefault(a => a.Name == statusName),
                QuestionId = questionId,
                Date = currentTime
            };

            return qS;
        }
        public string getItemsString(Items items)
        {
            string itemsStack = "";
            if (items.Computer != "false")
            {
                itemsStack += items.Computer;
                itemsStack += " ";
            }
            if (items.Laptop != "false")
            {
                itemsStack += items.Laptop;
                itemsStack += " ";
            }
            if (items.Monitor != "false")
            {
                itemsStack += items.Monitor;
                itemsStack += " ";
            }
            if (items.Keyboard != "false")
            {
                itemsStack += items.Keyboard;
                itemsStack += " ";
            }
            if (items.Mouse != "false")
            {
                itemsStack += items.Mouse;
                itemsStack += " ";
            }
            if (items.Phone != "false")
            {
                itemsStack += items.Phone;
                itemsStack += " ";
            }
            if (items.AnotherText != "")
            {
                itemsStack += items.AnotherText;
                itemsStack += " ";
            }
            return itemsStack;
        }

        [Authorize]
        public async Task<IActionResult> Info(int id)
        {
            //var item = await _db.Questions.Include(nameof(QS))
            //    .ThenInclude(nameof(Status))
            //    .Include(nameof(Items))
            //    .FirstOrDefaultAsync(s => s.Id == id);

            var item = await _db.Questions
                .Include(q => q.QS)
                    .ThenInclude(qs => qs.Status)
                .Include(q => q.Items)
                .FirstOrDefaultAsync(q => q.Id == id);

            item.ItemsString = getItemsString(item.Items);
            item.CurrentStatus = item.QS.OrderByDescending(f => f.Date).FirstOrDefault().Status.Description;
            item.StatusBackgroundColor = item.QS.OrderByDescending(f => f.Date).FirstOrDefault().Status.BackgroundColor;

            IndexVM indexVM = new IndexVM()
            {
                Question = item,
                Images = _db.Images.Where(e => e.QuestionId == id).ToList()
            };

            if (item.QS.FirstOrDefault(q => q.Status.Name == "Seen") == null)
            {
                item.QS.Add(addNewStatus("Seen", item.Id));
                _db.Update(item);
                _db.SaveChanges();
            }


            return View(indexVM);
        }

        [HttpPost]
        public async Task<int> Info(string state, int id)
        {
            var item = await _db.Questions.Include("QS").FirstOrDefaultAsync(q => q.Id == id);

            item.QS.Add(addNewStatus(state, item.Id));
            _db.Update(item);
            _db.SaveChanges();

            return id;
        }
        

        public async Task<IActionResult> Search(string number)
        {
            var q = await _db.Questions
                .Include(q => q.QS)
                    .ThenInclude(qs => qs.Status)
                .Include(q => q.Items)
                .OrderByDescending(q => q.QS.Min(qs => qs.Date))
                .ToListAsync();

            var question = q.Where(q => q.PhoneNumber == number).ToList();

            for (int i = 0; i < question.Count; i++)
            {
                question[i].ItemsString = getItemsString(question[i].Items);
                question[i].CurrentStatus = question[i].QS.OrderByDescending(f => f.Date).FirstOrDefault().Status.Description;
                question[i].LastUpdateDate = question[i].QS.OrderByDescending(f => f.Date).FirstOrDefault().Date.ToString();
                question[i].CreateQuestionDate = question[i].QS.OrderBy(f => f.Date).FirstOrDefault().Date.ToString();
                question[i].StatusBackgroundColor = question[i].QS.OrderByDescending(f => f.Date).FirstOrDefault().Status.BackgroundColor;
            };

            return View(question);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public class CalendarEvent
    {
        public string title { get; set; }
        public string start { get; set; }
        public string backgroundColor { get; set; }
        public string url { get; set; }
        public bool allDay  { get; set; }

    }

}
