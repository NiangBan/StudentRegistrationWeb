using Newtonsoft.Json;
using StudentRegistrationWeb.Extension;
using StudentRegistrationWeb.Helper;
using StudentRegistrationWeb.Models;
using StudentRegistrationWeb.Utils;
using StudentRegistrationWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StudentRegistrationWeb.Controllers
{
    public class StudentController : BaseController
    {
        // GET: Student
        string sessionId = string.Empty;
        string userId = string.Empty;        
        public ActionResult StudentList()
        {            
            try
            {
                ViewBag.IsSuccess = TempData["RespCode"];
                ViewBag.Message = TempData["RespDescription"];
                var response = new StudentListModel();
                #region BindRequestData

                ApiRequestModel request = new ApiRequestModel();
                                
                var apirequest = JsonConvert.DeserializeObject<StudentDTO>(JsonConvert.SerializeObject(request));
                request.JsonStringRequest = JsonConvert.SerializeObject(apirequest);

                ////Need to delete
                //Session[CommonSessionID] = sessionId;
                //Session[CommonUserID] = userId;
                ////

                request.SessionID = Session[CommonSessionID].ToString();
                request.UserId = Session[CommonUserID].ToString();
                #endregion

                #region Post Api

                var dataReturn = this.PostAPI(request, APIRoute.API_Student_List).Result;

                #endregion
                if (dataReturn.RespCode == "000")
                {                    
                    response = JsonConvert.DeserializeObject<StudentListModel>(dataReturn.JsonStringResponse);
                    ViewBag.StudentList = response.studentList;
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        
        public ActionResult StudentRegister()
        {
            var UniversityList =new UniversityListModel();
            ViewBag.IsSuccess = TempData["RespCode"];
            ViewBag.Message = TempData["RespDescription"];
            
            List<GenderModel> GenderList = new List<GenderModel>
            {
                new GenderModel{Id="Male" , Name = "Male"},
                new GenderModel{Id="Female" , Name = "Female"}
            };

            ViewBag.GenderList = GenderList;

            UniversityList = SelectUniversity();
            var universityId = string.Empty;
            var marjorId = string.Empty;
            if (UniversityList.UniversityList.Count > 0)
            {
                var uniInfo = new SelectList(UniversityList.UniversityList,
                    "Id",
                    "Name").ToList();
                ViewBag.UniversityList = uniInfo;
                universityId = uniInfo[0].Value;
            }
            else
            {
                IEnumerable<SelectListItem> uniInfo = new List<SelectListItem>();
                ViewBag.UniversityList = uniInfo;
            }

            MajorListModel majorList = new MajorListModel();
            majorList = SelectMajor(universityId.ToString());
            if (majorList.MajorList.Count > 0)
            {
                var majorInfo = new SelectList(majorList.MajorList,
                    "Id",
                    "Name").ToList();
                ViewBag.MajorList = majorInfo;
                marjorId = majorInfo[0].Value;
            }
            else
            {
                IEnumerable<SelectListItem> majorInfo = new List<SelectListItem>();
                ViewBag.MajorList = majorInfo;
            }

            AcademicYearListModel academicYearList = new AcademicYearListModel();
            academicYearList = SelectAcademic(marjorId);
            if (academicYearList.YearList.Count > 0)
            {
                var academicInfo = new SelectList(academicYearList.YearList,
                    "Id",
                    "Name").ToList();
                ViewBag.AcademicList = academicInfo;
            }
            else
            {
                IEnumerable<SelectListItem> academicInfo = new List<SelectListItem>();
                ViewBag.AcademicList = academicInfo;
            }
            return View();
        }

        [HttpPost]
        public RedirectResult StudentRegister(StudentViewModel model)
        {            
            try
            {
                #region BindRequestData

                ApiRequestModel request = new ApiRequestModel();

                StudentDTO studentReq = new StudentDTO();
                studentReq.Name = model.Name;
                studentReq.FatherName = model.FatherName;
                studentReq.StudentNo = model.StudentNo;
                studentReq.NRC = model.NRC;
                studentReq.Phone = model.Phone;
                studentReq.Email = model.Email;
                studentReq.Address = model.Address;
                studentReq.Gender = model.Gender;
                studentReq.DateOfBirth = model.DateOfBirth;
                studentReq.UniversityId = model.University;
                studentReq.MajorId = model.Major;
                studentReq.AcademicyearId = model.AcademicYear;
                studentReq.Gender = model.Gender;
                studentReq.CreatedDate = DateTime.Now.ToString();
                studentReq.UpdatedDate = DateTime.Now.ToString();
                studentReq.CreatedUserId = "1";
                studentReq.UpdatedUserId = "1";
                var apirequest = JsonConvert.DeserializeObject<StudentDTO>(JsonConvert.SerializeObject(studentReq));
                
                request.JsonStringRequest = JsonConvert.SerializeObject(apirequest);
                
                ////Need to delete
                //Session[CommonSessionID] = sessionId;
                //Session[CommonUserID] = userId;
                ////
                request.SessionID = Session[CommonSessionID].ToString();
                request.UserId = Session[CommonUserID].ToString();

                #endregion

                #region Post Api

                var dataReturn = this.PostAPI(request, APIRoute.API_Student_Register).Result;

                #endregion
                string redirectLink = "";
                if (dataReturn.RespCode == "000")
                {
                    StudentList();
                    TempData["RespCode"] = "success";
                    TempData["RespDescription"] = "Student Registration "+ dataReturn.RespDescription ;
                    redirectLink = HtmlExtension.GetEncryptLinkForRedirect("StudentList", "Student");
                    return Redirect(redirectLink);
                }                
                else
                {
                    ViewBag.IsSuccess = "fail";
                    ViewBag.Message = dataReturn.RespDescription;
                    TempData["RespCode"]="fail";
                    TempData["RespDescription"]= "Student Registration : " + dataReturn.RespDescription; 
                    redirectLink = HtmlExtension.GetEncryptLinkForRedirect("StudentRegister", "Student");
                    return Redirect(redirectLink);
                }
            }
            catch (Exception ex)
            {
                return Redirect("");
            }
        }

        public ActionResult StudentUpdateByStudentId(StudentModel model)
        {
            var response = new StudentDTO();
            StudentViewModel studentViewModel = new StudentViewModel();
            var UniversityList = new UniversityListModel();
            try
            {
                ViewBag.IsSuccess = TempData["RespCode"];
                ViewBag.Message = TempData["RespDescription"];
                var studentId = TempData["StudentId"];

                List<GenderModel> GenderList = new List<GenderModel>
                {
                    new GenderModel{Id="Male" , Name = "Male"},
                    new GenderModel{Id="Female" , Name = "Female"}
                };

                ViewBag.GenderList = GenderList;

                #region BindRequestData

                ApiRequestModel request = new ApiRequestModel();
                StudentDTO studentModel = new StudentDTO();
                if(studentId != null)
                {
                    studentModel.Id = studentId.ToString();
                }
                else
                {
                    studentModel.Id = model.idString;
                }                
                var apirequest = JsonConvert.DeserializeObject<StudentDTO>(JsonConvert.SerializeObject(studentModel));
                request.JsonStringRequest = JsonConvert.SerializeObject(apirequest);

                ////Need to delete
                //Session[CommonSessionID] = sessionId;
                //Session[CommonUserID] = userId;
                ////

                request.SessionID = Session[CommonSessionID].ToString();
                request.UserId = Session[CommonUserID].ToString();
                #endregion

                #region Post Api

                var dataReturn = this.PostAPI(request, APIRoute.API_Get_StudentById).Result;

                #endregion
                if (dataReturn.RespCode == "000")
                {
                    response = JsonConvert.DeserializeObject<StudentDTO>(dataReturn.JsonStringResponse);
                    studentViewModel.Id =Int32.Parse(response.Id);
                    studentViewModel.StudentNo = response.StudentNo;
                    studentViewModel.Name = response.Name;
                    studentViewModel.NRC = response.NRC;
                    studentViewModel.Address = response.Address;
                    studentViewModel.Phone = response.Phone;
                    studentViewModel.Email = response.Email;
                    studentViewModel.FatherName = response.FatherName;
                    studentViewModel.DateOfBirth = response.DateOfBirth;
                    studentViewModel.Gender = response.Gender;

                    studentViewModel.University = response.UniversityName;
                    studentViewModel.UniversityID = Int32.Parse(response.UniversityId);
                    studentViewModel.Major = response.MajorName;
                    studentViewModel.MajorID = Int32.Parse(response.MajorId);
                    studentViewModel.AcademicYear = response.AcademicyearName;
                    studentViewModel.AcademicYearID = Int32.Parse(response.AcademicyearId);

                    UniversityList = SelectUniversity();

                    if (UniversityList.UniversityList.Count > 0)
                    {
                        var uniInfo = new SelectList(UniversityList.UniversityList.Where(x => x.Name != studentViewModel.University),
                            "Id",
                            "Name").ToList();
                        uniInfo.Insert(0, new SelectListItem { Value = studentViewModel.UniversityID.ToString(), Text = studentViewModel.University });
                        ViewBag.UniversityList = uniInfo;
                    }
                    else
                    {
                        IEnumerable<SelectListItem> uniInfo = new List<SelectListItem>();
                        ViewBag.UniversityList = uniInfo;
                    }

                    MajorListModel majorList = new MajorListModel();
                    majorList = SelectMajor(studentViewModel.UniversityID.ToString());
                    if (majorList.MajorList.Count > 0)
                    {
                        var majorInfo = new SelectList(majorList.MajorList.Where(x => x.Name != studentViewModel.Major),
                            "Id",
                            "Name").ToList();
                        majorInfo.Insert(0, new SelectListItem { Value = studentViewModel.MajorID.ToString(), Text = studentViewModel.Major });
                        ViewBag.MajorList = majorInfo;
                    }
                    else
                    {
                        IEnumerable<SelectListItem> majorInfo = new List<SelectListItem>();
                        ViewBag.MajorList = majorInfo;
                    }

                    AcademicYearListModel academicYearList = new AcademicYearListModel();
                    academicYearList = SelectAcademic(studentViewModel.MajorID.ToString());
                    if (academicYearList.YearList.Count > 0)
                    {
                        var academicInfo = new SelectList(academicYearList.YearList.Where(x => x.Name != studentViewModel.AcademicYear),
                            "Id",
                            "Name").ToList();
                        academicInfo.Insert(0, new SelectListItem { Value = studentViewModel.AcademicYearID.ToString(), Text = studentViewModel.AcademicYear });
                        ViewBag.AcademicList = academicInfo;
                    }
                    else
                    {
                        IEnumerable<SelectListItem> academicInfo = new List<SelectListItem>();
                        ViewBag.AcademicList = academicInfo;
                    }
                }
                return View("StudentUpdate", studentViewModel);

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public RedirectResult StudentUpdate(StudentViewModel model)
        {
            try
            {
                #region BindRequestData

                ApiRequestModel request = new ApiRequestModel();

                StudentDTO studentReq = new StudentDTO();
                studentReq.Id = model.Id.ToString();
                studentReq.Name = model.Name;
                studentReq.FatherName = model.FatherName;
                studentReq.StudentNo = model.StudentNo;
                studentReq.NRC = model.NRC;
                studentReq.Phone = model.Phone;
                studentReq.Email = model.Email;
                studentReq.Address = model.Address;
                studentReq.Gender = model.Gender;
                studentReq.DateOfBirth = model.DateOfBirth;
                studentReq.UniversityId = model.University;
                studentReq.MajorId = model.Major;
                studentReq.AcademicyearId = model.AcademicYear;
                studentReq.Gender = model.Gender;
                studentReq.CreatedDate = DateTime.Now.ToString();
                studentReq.UpdatedDate = DateTime.Now.ToString();
                studentReq.CreatedUserId = "1";
                studentReq.UpdatedUserId = "1";
                var apirequest = JsonConvert.DeserializeObject<StudentDTO>(JsonConvert.SerializeObject(studentReq));

                request.JsonStringRequest = JsonConvert.SerializeObject(apirequest);
                
                ////Need to delete
                //Session[CommonSessionID] = sessionId;
                //Session[CommonUserID] = userId;
                ////

                request.SessionID = Session[CommonSessionID].ToString();
                request.UserId = Session[CommonUserID].ToString();

                #endregion

                #region Post Api

                var dataReturn = this.PostAPI(request, APIRoute.API_Student_Update).Result;

                #endregion

                string redirectLink = "";
                if (dataReturn.RespCode == "000")
                {
                    StudentList();
                    TempData["RespCode"] = "success";
                    TempData["RespDescription"] = "Student Update " + dataReturn.RespDescription ;
                    redirectLink = HtmlExtension.GetEncryptLinkForRedirect("StudentList", "Student");
                    return Redirect(redirectLink);
                }
                else
                {
                    ViewBag.IsSuccess = "fail";
                    ViewBag.Message = dataReturn.RespDescription;
                    TempData["RespCode"] = "fail";
                    TempData["RespDescription"] = "Student Update : " + dataReturn.RespDescription;
                    redirectLink = HtmlExtension.GetEncryptLinkForRedirect("StudentUpdateByStudentId", "Student");
                    return Redirect(redirectLink);
                }
            }
            catch (Exception ex)
            {
                return Redirect("");
            }
        }

        [HttpPost]
        public ActionResult StudentDeleteByStudentId(StudentModel model)
        {
            try
            {
                #region BindRequestData

                ApiRequestModel request = new ApiRequestModel();

                StudentDTO studentReq = new StudentDTO();
                studentReq.Id = model.idString;
                var apirequest = JsonConvert.DeserializeObject<StudentDTO>(JsonConvert.SerializeObject(studentReq));

                request.JsonStringRequest = JsonConvert.SerializeObject(apirequest);

                ////Need to delete
                //Session[CommonSessionID] = sessionId;
                //Session[CommonUserID] = userId;
                ////

                request.SessionID = Session[CommonSessionID].ToString();
                request.UserId = Session[CommonUserID].ToString();

                #endregion

                #region Post Api

                var dataReturn = this.PostAPI(request, APIRoute.API_Student_Delete).Result;

                #endregion

                if (dataReturn.RespCode == "000")
                {
                    StudentList();
                    return View("StudentList");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult StudentDetail(StudentModel model)
        {
            var response = new StudentDTO();
            StudentViewModel studentViewModel = new StudentViewModel();
            var UniversityList = new UniversityListModel();
            try
            {
                List<GenderModel> GenderList = new List<GenderModel>
                {
                    new GenderModel{Id="Male" , Name = "Male"},
                    new GenderModel{Id="Female" , Name = "Female"}
                };

                ViewBag.GenderList = GenderList;
                #region BindRequestData

                ApiRequestModel request = new ApiRequestModel();
                StudentDTO studentModel = new StudentDTO();
                studentModel.Id = model.idString;
                var apirequest = JsonConvert.DeserializeObject<StudentDTO>(JsonConvert.SerializeObject(studentModel));
                request.JsonStringRequest = JsonConvert.SerializeObject(apirequest);

                ////Need to delete
                //Session[CommonSessionID] = sessionId;
                //Session[CommonUserID] = userId;
                ////

                request.SessionID = Session[CommonSessionID].ToString();
                request.UserId = Session[CommonUserID].ToString();
                #endregion

                #region Post Api

                var dataReturn = this.PostAPI(request, APIRoute.API_Get_StudentById).Result;

                #endregion
                if (dataReturn.RespCode == "000")
                {
                    response = JsonConvert.DeserializeObject<StudentDTO>(dataReturn.JsonStringResponse);

                    studentViewModel.StudentNo = response.StudentNo;
                    studentViewModel.Name = response.Name;
                    studentViewModel.NRC = response.NRC;
                    studentViewModel.Address = response.Address;
                    studentViewModel.Phone = response.Phone;
                    studentViewModel.Email = response.Email;
                    studentViewModel.FatherName = response.FatherName;
                    studentViewModel.DateOfBirth = response.DateOfBirth;
                    studentViewModel.Gender = response.Gender;

                    studentViewModel.University = response.UniversityName;
                    studentViewModel.UniversityID = Int32.Parse(response.UniversityId);
                    studentViewModel.Major = response.MajorName;
                    studentViewModel.MajorID = Int32.Parse(response.MajorId);
                    studentViewModel.AcademicYear = response.AcademicyearName;
                    studentViewModel.AcademicYearID = Int32.Parse(response.AcademicyearId);

                    UniversityList = SelectUniversity();

                    if (UniversityList.UniversityList.Count > 0)
                    {
                        var uniInfo = new SelectList(UniversityList.UniversityList.Where(x => x.Name != studentViewModel.University),
                            "Id",
                            "Name").ToList();
                        uniInfo.Insert(0, new SelectListItem { Value = studentViewModel.UniversityID.ToString(), Text = studentViewModel.University });
                        ViewBag.UniversityList = uniInfo;
                    }
                    else
                    {
                        IEnumerable<SelectListItem> uniInfo = new List<SelectListItem>();
                        ViewBag.UniversityList = uniInfo;
                    }

                    MajorListModel majorList = new MajorListModel();
                    majorList = SelectMajor(studentViewModel.UniversityID.ToString());
                    if (majorList.MajorList.Count > 0)
                    {
                        var majorInfo = new SelectList(majorList.MajorList.Where(x => x.Name != studentViewModel.Major),
                            "Id",
                            "Name").ToList();
                        majorInfo.Insert(0, new SelectListItem { Value = studentViewModel.MajorID.ToString(), Text = studentViewModel.Major });
                        ViewBag.MajorList = majorInfo;
                    }
                    else
                    {
                        IEnumerable<SelectListItem> majorInfo = new List<SelectListItem>();
                        ViewBag.MajorList = majorInfo;
                    }

                    AcademicYearListModel academicYearList = new AcademicYearListModel();
                    academicYearList = SelectAcademic(studentViewModel.MajorID.ToString());
                    if (academicYearList.YearList.Count > 0)
                    {
                        var academicInfo = new SelectList(academicYearList.YearList.Where(x => x.Name != studentViewModel.AcademicYear),
                            "Id",
                            "Name").ToList();
                        academicInfo.Insert(0, new SelectListItem { Value = studentViewModel.AcademicYearID.ToString(), Text = studentViewModel.AcademicYear });
                        ViewBag.AcademicList = academicInfo;
                    }
                    else
                    {
                        IEnumerable<SelectListItem> academicInfo = new List<SelectListItem>();
                        ViewBag.AcademicList = academicInfo;
                    }                    
                }
                return View("StudentDetail", studentViewModel);

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [AllowAnonymous]
        public UniversityListModel SelectUniversity()
        {
            var response = new UniversityListModel();
            try
            {                
                #region BindRequestData

                ApiRequestModel request = new ApiRequestModel();

                var apirequest = JsonConvert.DeserializeObject<StudentDTO>(JsonConvert.SerializeObject(request));
                request.JsonStringRequest = JsonConvert.SerializeObject(apirequest);

                ////Need to delete
                //Session[CommonSessionID] = sessionId;
                //Session[CommonUserID] = userId;
                ////

                request.SessionID = Session[CommonSessionID].ToString();
                request.UserId = Session[CommonUserID].ToString();
                #endregion

                #region Post Api

                var dataReturn = this.PostAPI(request, APIRoute.API_Get_UniversityList).Result;

                #endregion
                if (dataReturn.RespCode == "000")
                {
                    response = JsonConvert.DeserializeObject<UniversityListModel>(dataReturn.JsonStringResponse);                    
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        [AllowAnonymous]
        public ActionResult SelectMajorByUniversityId(string universityid)
        {
            IEnumerable<SelectListItem> infoList = new List<SelectListItem>();
            var response = SelectMajor(universityid);
            if (response.MajorList != null)
            {
                infoList = new SelectList(response.MajorList,
                    "Id",
                    "Name").ToList();
                return Json(new SelectList(infoList, "Value", "Text"));
            }
            else
            {
                return Json(new SelectList(infoList, "Value", "Text"));
            }
        }

        [AllowAnonymous]
        public ActionResult SelectAcademicByMajorId(string majorid)
        {
            IEnumerable<SelectListItem> infoList = new List<SelectListItem>();
            var response = SelectAcademic(majorid);
            if (response.YearList != null)
            {
                infoList = new SelectList(response.YearList,
                    "Id",
                    "Name").ToList();
                return Json(new SelectList(infoList, "Value", "Text"));
            }
            else
            {
                return Json(new SelectList(infoList, "Value", "Text"));
            }
        }

        public MajorListModel SelectMajor(string universityid)
        {   
            var response = new MajorListModel();
            try
            {
                #region BindRequestData

                ApiRequestModel request = new ApiRequestModel();
                MajorModel majorModel = new MajorModel();
                majorModel.UniversityId = universityid;
                var apirequest = JsonConvert.DeserializeObject<MajorModel>(JsonConvert.SerializeObject(majorModel));
                request.JsonStringRequest = JsonConvert.SerializeObject(apirequest);

                ////Need to delete
                //Session[CommonSessionID] = sessionId;
                //Session[CommonUserID] = userId;
                ////

                request.SessionID = Session[CommonSessionID].ToString();
                request.UserId = Session[CommonUserID].ToString();
                #endregion

                #region Post Api

                var dataReturn = this.PostAPI(request, APIRoute.API_Get_MajorList_UniId).Result;

                #endregion
                if (dataReturn.RespCode == "000")
                {
                    response = JsonConvert.DeserializeObject<MajorListModel>(dataReturn.JsonStringResponse);
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public AcademicYearListModel SelectAcademic(string majorid)
        {            
            var response = new AcademicYearListModel();
            try
            {
                #region BindRequestData

                ApiRequestModel request = new ApiRequestModel();
                AcademicYearModel academicYearModel = new AcademicYearModel();
                academicYearModel.MajorId = majorid;
                var apirequest = JsonConvert.DeserializeObject<AcademicYearModel>(JsonConvert.SerializeObject(academicYearModel));
                request.JsonStringRequest = JsonConvert.SerializeObject(apirequest);

                ////Need to delete
                //Session[CommonSessionID] = sessionId;
                //Session[CommonUserID] = userId;
                ////

                request.SessionID = Session[CommonSessionID].ToString();
                request.UserId = Session[CommonUserID].ToString();
                #endregion

                #region Post Api

                var dataReturn = this.PostAPI(request, APIRoute.API_Get_AcademicList_MajorId).Result;

                #endregion
                if (dataReturn.RespCode == "000")
                {
                    response = JsonConvert.DeserializeObject<AcademicYearListModel>(dataReturn.JsonStringResponse);
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
    }
}