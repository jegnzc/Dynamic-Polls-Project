using DevelTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DevelTest.Controllers
{
    [Authorize]
    public class PollController : Controller
    {
        public ActionResult Index()
        {
            List<PollViewModel> pollList = new List<PollViewModel>();
            using (var context = new ApplicationDbContext())
            {
                pollList = context.Polls.Select(x => new PollViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PollFields = x.PollFields.Select(z => new PollFieldViewModel
                    {
                        Id = z.Id,
                        Name = z.Name,
                        DisplayName = z.DisplayName,
                        PollFieldTypeDesc = z.PollFieldType.Name,
                        Required = z.Required
                    }).ToList(),
                    Url = x.Url

                }).ToList();
            }
            return View(pollList);
        }

        public ActionResult Results(int id)
        {
            PollViewModel pollList = new PollViewModel();
            using (var context = new ApplicationDbContext())
            {
                pollList = context.Polls.Where(y => y.Id == id).Select(x => new PollViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PollFields = x.PollFields.Select(z => new PollFieldViewModel
                    {
                        Id = z.Id,
                        Name = z.Name,
                        DisplayName = z.DisplayName,
                        PollFieldTypeDesc = z.PollFieldType.Name,
                        Required = z.Required,
                        PollAnswers = z.PollAnswers.Select(m => new PollAnswerViewModel
                        {
                            Id = m.Id,
                            Answer = m.Answer,
                            PollFieldId = m.PollFieldId
                        }).ToList()
                    }).ToList(),
                    Url = x.Url

                }).FirstOrDefault();
            }
            return View(pollList);
        }

        [AllowAnonymous]
        public ActionResult Answer(string id)
        {
            PollViewModel pollList = new PollViewModel();
            using (var context = new ApplicationDbContext())
            {
                pollList = context.Polls.Where(z => z.Url.Contains(id)).Select(x => new PollViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PollFields = x.PollFields.Select(z => new PollFieldViewModel
                    {
                        Id = z.Id,
                        Name = z.Name,
                        DisplayName = z.DisplayName,
                        PollFieldTypeDesc = z.PollFieldType.Name,
                        PollFieldTypeId = z.PollFieldTypeId,
                        Required = z.Required
                    }).ToList(),
                    Url = x.Url

                }).FirstOrDefault();
            }
            return View(pollList);
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Answer(PollViewModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                foreach (var field in model.PollFields)
                {
                    string Answer = "No respondió";
                    if (field.Numeric != null)
                    {
                        Answer = field.Numeric.ToString();
                    }
                    else if (field.Text != "" && field.Text != null)
                    {
                        Answer = field.Text;
                    }
                    else if (field.Date != null)
                    {
                        Answer = field.Date.ToString();
                    }
                    var realModel = new PollAnswer
                    {
                        PollFieldId = field.Id,
                        Answer = Answer
                    };
                    context.PollAnswers.Add(realModel);
                }
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id)
        {
            PollViewModel pollList = new PollViewModel();
            using (var context = new ApplicationDbContext())
            {
                pollList = context.Polls.Where(p => p.Id == id).Select(x => new PollViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    PollFields = x.PollFields.Select(z => new PollFieldViewModel
                    {
                        Id = z.Id,
                        Name = z.Name,
                        DisplayName = z.DisplayName,
                        PollFieldTypeId = z.PollFieldTypeId,
                        PollFieldTypeDesc = z.PollFieldType.Name,
                        Required = z.Required
                    }).ToList(),
                    Url = x.Url

                }).FirstOrDefault();
            }

            ViewBag.PollFieldTypes = GetPollFieldTypesDropDown();
            return View(pollList);
        }

        public ActionResult Delete(int id)
        {
            ViewBag.PollFieldTypes = GetPollFieldTypesDropDown();
            if (ModelState.IsValid)
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = context.Polls.SingleOrDefault(b => b.Id == id);
                    if (result != null)
                    {
                        context.Entry(result).State = EntityState.Deleted;
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(PollViewModel model)
        {
            ViewBag.PollFieldTypes = GetPollFieldTypesDropDown();
            if (ModelState.IsValid)
            {
                var realModel = new Poll
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    PollFields = model.PollFields.Select(x => new PollField
                    {
                        Name = x.Name,
                        DisplayName = x.DisplayName,
                        PollFieldTypeId = x.PollFieldTypeId,
                        Required = x.Required
                    }).ToList(),
                    Url = Url.Action("Answer", "Poll", null, Request.Url.Scheme) + "/" + GenerateUniqueRandomToken()
                };
                using (var context = new ApplicationDbContext())
                {
                    var result = context.Polls.SingleOrDefault(b => b.Id == model.Id);
                    if (result != null)
                    {
                        result.Name = realModel.Name;
                        result.Description = realModel.Description;
                        result.PollFields.ToList().ForEach(s => context.Entry(s).State = EntityState.Deleted);
                        result.PollFields = realModel.PollFields;

                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.PollFieldTypes = GetPollFieldTypesDropDown();
            return View(new PollViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(PollViewModel model)
        {
            ViewBag.PollFieldTypes = GetPollFieldTypesDropDown();
            if (ModelState.IsValid)
            {
                var realModel = new Poll
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    PollFields = model.PollFields.Select(x => new PollField
                    {
                        Name = x.Name,
                        DisplayName = x.DisplayName,
                        PollFieldTypeId = x.PollFieldTypeId,
                        Required = x.Required
                    }).ToList(),
                    Url = Url.Action("Answer", "Poll", null, Request.Url.Scheme) + "/" + GenerateUniqueRandomToken()
                };
                using (var context = new ApplicationDbContext())
                {
                    context.Polls.Add(realModel);
                    context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddPollField(PollViewModel viewModel)
        {
            viewModel.PollFields.Add(viewModel.PollField);
            ViewBag.PollFieldTypes = GetPollFieldTypesDropDown();
            return View(nameof(Create), viewModel);
        }

        public SelectList GetPollFieldTypesDropDown()
        {
            SelectList PollFieldTypesSL = new SelectList("");
            using (var context = new ApplicationDbContext())
            {
                var PollFieldTypes = context.PollFieldTypes.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();
                PollFieldTypesSL = new SelectList(PollFieldTypes, "Value", "Text");
            }
            return PollFieldTypesSL;
        }

        public static string GenerateUniqueRandomToken()
        {
            const string availableChars =
                "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            using (var generator = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[16];
                generator.GetBytes(bytes);
                var chars = bytes
                    .Select(b => availableChars[b % availableChars.Length]);
                var token = new string(chars.ToArray());
                return token;
            }
        }

    }
}