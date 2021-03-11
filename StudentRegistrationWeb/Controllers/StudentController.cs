using StudentRegistrationWeb.Models;
using StudentRegistrationWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationWeb.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult StudentList()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult GetStudentListSearch()
        {
            return View("StudentList");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetStudentListSearch(StudentModel student)
        {
            return View("StudentList");
        }
        public ActionResult StudentRegister()
        {
            TestModel t = new TestModel();
            t.Name = "CU";
            TestModel t1 = new TestModel();
            t1.Name = "TU";
            List<TestModel> l = new List<TestModel>();
            l.Add(t);
            l.Add(t1);
            ViewBag.UniversityList = l;
            return View();
        }
        public ActionResult StudentUpdateByStudentId(StudentModel model)
        {
            List<TestModel> UniversityList = new List<TestModel>();
            List<TestModel> MajorList = new List<TestModel>();
            List<TestModel> AcademicYearList = new List<TestModel>();
            TestModel university = new TestModel();
            TestModel major = new TestModel();
            TestModel academicYear = new TestModel();

            StudentViewModel studentViewModel = new StudentViewModel();
            studentViewModel.Name = "Ban Kim";
            studentViewModel.University = "TU";
            studentViewModel.UniversityID = 2;
            studentViewModel.Major = "EC";
            studentViewModel.MajorID = 4;
            studentViewModel.AcademicYear = "3rd Year";
            studentViewModel.AcademicYearID = 3;

            university.Name = "CU";
            university.Id = 1;
            UniversityList.Add(university);

            university = new TestModel();
            university.Name = "TU";
            university.Id = 2;
            UniversityList.Add(university);

            //major.Name = "CS";
            //major.Id = 1;
            //MajorList.Add(major);

            //major = new TestModel();
            //major.Name = "CT";
            //major.Id = 2;
            //MajorList.Add(major);

            //major = new TestModel();
            //major.Name = "Civil";
            //major.Id = 3;
            //MajorList.Add(major);

            //major = new TestModel();
            //major.Name = "EC";
            //major.Id = 4;
            //MajorList.Add(major);

            //major = new TestModel();
            //major.Name = "EP";
            //major.Id = 5;
            //MajorList.Add(major);

            //academicYear.Name = "1st Year";
            //academicYear.Id = 1;
            //AcademicYearList.Add(academicYear);

            //academicYear = new TestModel();
            //academicYear.Name = "2nd Year";
            //academicYear.Id = 2;
            //AcademicYearList.Add(academicYear);

            //academicYear = new TestModel();
            //academicYear.Name = "3rd Year";
            //academicYear.Id = 3;
            //AcademicYearList.Add(academicYear);

            //academicYear = new TestModel();
            //academicYear.Name = "4th Year";
            //academicYear.Id = 4;
            //AcademicYearList.Add(academicYear);

            //academicYear = new TestModel();
            //academicYear.Name = "Final Year";
            //academicYear.Id = 5;
            //AcademicYearList.Add(academicYear);

            var uniInfo = new SelectList(UniversityList.Where(x => x.Name != studentViewModel.University),
                    "ID",
                    "Name").ToList();
            uniInfo.Insert(0, new SelectListItem { Value = studentViewModel.UniversityID.ToString(), Text = studentViewModel.University });
            ViewBag.UniversityList = uniInfo;

            MajorListModel majorList = new MajorListModel();
            majorList = SelectMajor(studentViewModel.UniversityID.ToString());
            if (majorList.lstMajor.Count > 0)
            {
                var majorInfo = new SelectList(majorList.lstMajor.Where(x => x.Name != studentViewModel.Major),
                    "MajorID",
                    "Name").ToList();
                majorInfo.Insert(0, new SelectListItem { Value = studentViewModel.MajorID.ToString(), Text = studentViewModel.Major });
                ViewBag.MajorList = majorInfo;
            }

            AcademicYearListModel academicYearList = new AcademicYearListModel();
            academicYearList = SelectAcademic(studentViewModel.MajorID.ToString());
            if (academicYearList.lstAcademicYear.Count > 0)
            {
                var academicInfo = new SelectList(academicYearList.lstAcademicYear.Where(x => x.Name != studentViewModel.AcademicYear),
                    "ID",
                    "Name").ToList();
                academicInfo.Insert(0, new SelectListItem { Value = studentViewModel.AcademicYearID.ToString(), Text = studentViewModel.AcademicYear });
                ViewBag.AcademicList = academicInfo;
            }
            return View("StudentUpdate", studentViewModel);
        }
        public ActionResult StudentUpdate()
        {
            return View();
        }
        
        public ActionResult StudentDetail(StudentModel model)
        {
            List<TestModel> UniversityList = new List<TestModel>();
            List<TestModel> MajorList = new List<TestModel>();
            List<TestModel> AcademicYearList = new List<TestModel>();
            TestModel university = new TestModel();
            TestModel major = new TestModel();
            TestModel academicYear = new TestModel();

            StudentViewModel studentViewModel = new StudentViewModel();
            studentViewModel.Name = "Ban Kim";
            studentViewModel.University = "CU";
            studentViewModel.UniversityID = 1;
            studentViewModel.Major = "CT";
            studentViewModel.MajorID = 2;
            studentViewModel.AcademicYear = "Final Year";
            studentViewModel.AcademicYearID = 5;

            university.Name = "CU";
            university.Id = 1;
            UniversityList.Add(university);

            university = new TestModel();
            university.Name = "TU";
            university.Id = 2;
            UniversityList.Add(university);

            major.Name = "CS";
            major.Id = 1;
            MajorList.Add(major);

            major = new TestModel();
            major.Name = "CT";
            major.Id = 2;
            MajorList.Add(major);

            major = new TestModel();
            major.Name = "Civil";
            major.Id = 3;
            MajorList.Add(major);

            major = new TestModel();
            major.Name = "EC";
            major.Id = 4;
            MajorList.Add(major);

            major = new TestModel();
            major.Name = "EP";
            major.Id = 5;
            MajorList.Add(major);

            academicYear.Name = "1st Year";
            academicYear.Id = 1;
            AcademicYearList.Add(academicYear);

            academicYear = new TestModel();
            academicYear.Name = "2nd Year";
            academicYear.Id = 2;
            AcademicYearList.Add(academicYear);

            academicYear = new TestModel();
            academicYear.Name = "3rd Year";
            academicYear.Id = 3;
            AcademicYearList.Add(academicYear);

            academicYear = new TestModel();
            academicYear.Name = "4th Year";
            academicYear.Id = 4;
            AcademicYearList.Add(academicYear);

            academicYear = new TestModel();
            academicYear.Name = "Final Year";
            academicYear.Id = 5;
            AcademicYearList.Add(academicYear);


            var uniInfo = new SelectList(UniversityList.Where(x => x.Name != studentViewModel.University),
                    "ID",
                    "Name").ToList();
            uniInfo.Insert(0, new SelectListItem { Value = studentViewModel.UniversityID.ToString(), Text = studentViewModel.University });
            ViewBag.UniversityList = uniInfo;

            var majorInfo = new SelectList(MajorList.Where(x => x.Name != studentViewModel.Major),
                    "ID",
                    "Name").ToList();
            majorInfo.Insert(0, new SelectListItem { Value = studentViewModel.MajorID.ToString(), Text = studentViewModel.Major });
            ViewBag.MajorList = majorInfo;

            var academicInfo = new SelectList(AcademicYearList.Where(x => x.Name != studentViewModel.AcademicYear),
                    "ID",
                    "Name").ToList();
            academicInfo.Insert(0, new SelectListItem { Value = studentViewModel.AcademicYearID.ToString(), Text = studentViewModel.AcademicYear });
            ViewBag.AcademicList = academicInfo;

            return View("StudentDetail", studentViewModel);
        }

        [AllowAnonymous]
        public ActionResult SelectMajorByUniversityId(string universityid)
        {
            IEnumerable<SelectListItem> infoList = new List<SelectListItem>();
            var response = SelectMajor(universityid);
            if (response.lstMajor != null)
            {

                infoList = new SelectList(response.lstMajor,
                    "MajorID",
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
            if (response.lstAcademicYear != null)
            {

                infoList = new SelectList(response.lstAcademicYear,
                    "AcademicYearID",
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
            MajorListModel response = new MajorListModel();

            MajorModel major = new MajorModel();
            List<MajorModel> lstMajor = new List<MajorModel>();

            if (universityid=="1")
            {
                major.Name = "CS";
                major.MajorID = "1";
                lstMajor.Add(major);

                major = new MajorModel();
                major.Name = "CT";
                major.MajorID = "2";
                lstMajor.Add(major);
            }
            else
            {
                major = new MajorModel();
                major.Name = "Civil";
                major.MajorID = "3";
                lstMajor.Add(major);

                major = new MajorModel();
                major.Name = "EC";
                major.MajorID = "4";
                lstMajor.Add(major);

                major = new MajorModel();
                major.Name = "EP";
                major.MajorID = "5";
                lstMajor.Add(major);
            }

            response.lstMajor = lstMajor;
            response.RespCode = "0000";
            response.RespDescription = "Sucess";
            return response;
            //#region BindRequestData

            //string requestFilePath = APIRoute.API_SelectOtherBranch;
            //string requestData = string.Empty;
            //string errormsg = string.Empty;
            //var apimodel = APIRequest(requestFilePath,
            //    Session[CommonDynamicKey].ToString(),
            //    Session[CommonSessionID].ToString(),
            //    Session[CommonUserID].ToString());
            //var apirequest = JsonConvert.DeserializeObject<OtherBranchSelectRequestModel>(JsonConvert.SerializeObject(apimodel));
            //apirequest.OtherBankID = universityid;
            //ABankRequestModel request = new ABankRequestModel();
            //request.UserId = apimodel.UserID;
            //request.SessionID = apimodel.SessionID;
            //request.DeviceID = CommonUtils.DeviceID;
            //request.UserType = CommonUtils.UserType;
            //request.IV = apimodel.IV;
            //try
            //{
            //    var bindreq = JsonConvert.SerializeObject(apirequest);
            //    request.JsonStringRequest = bindreq;
            //    requestData = JsonConvert.SerializeObject(request);
            //    request.JsonStringRequest = RijndaelCrypt.EncryptAES(bindreq, apimodel.dynamicKey, apimodel.hardCodeIV);
            //    #endregion

            //    var dataReturn = this.PostMobileAPI(request, apimodel.requestFilePath).Result;

            //    var bindData = RijndaelCrypt.DecryptAES(dataReturn.JsonStringResponse, apimodel.dynamicKey, apimodel.hardCodeIV);
            //    response = JsonConvert.DeserializeObject<OtherBrnchListModel>(bindData);
            //    return response;

            //}
            //catch (Exception ex)
            //{
            //    errormsg = ex.Message;
            //    return response;
            //}
            //finally
            //{
            //    if (!string.IsNullOrEmpty(errormsg))
            //    {
            //        Log.Error(requestFilePath + " Error: " + errormsg);
            //        Log.Error("Request:" + requestData);
            //        Log.Error("Response:" + JsonConvert.SerializeObject(response));
            //    }
            //    else
            //    {
            //        Log.Info(requestFilePath + " Successful");
            //        Log.Info("Request:" + requestData);
            //        Log.Info("Response:" + JsonConvert.SerializeObject(response));
            //    }
            //}
        }

        public AcademicYearListModel SelectAcademic(string majorid)
        {
            AcademicYearListModel response = new AcademicYearListModel();

            AcademicYearModel academicYear = new AcademicYearModel();
            List<AcademicYearModel> AcademicYearList = new List<AcademicYearModel>();

            if (majorid == "1" || majorid == "2")
            {
                academicYear.Name = "1st Year";
                academicYear.AcademicYearID = "1";
                AcademicYearList.Add(academicYear);

                academicYear = new AcademicYearModel();
                academicYear.Name = "2nd Year";
                academicYear.AcademicYearID = "2";
                AcademicYearList.Add(academicYear);

                academicYear = new AcademicYearModel();
                academicYear.Name = "3rd Year";
                academicYear.AcademicYearID = "3";
                AcademicYearList.Add(academicYear);

                academicYear = new AcademicYearModel();
                academicYear.Name = "4th Year";
                academicYear.AcademicYearID = "4";
                AcademicYearList.Add(academicYear);

                academicYear = new AcademicYearModel();
                academicYear.Name = "Final Year";
                academicYear.AcademicYearID = "5";
                AcademicYearList.Add(academicYear);
            }
            else if (majorid == "2")
            {
                academicYear.Name = "1st Year";
                academicYear.AcademicYearID = "6";
                AcademicYearList.Add(academicYear);

                academicYear = new AcademicYearModel();
                academicYear.Name = "2nd Year";
                academicYear.AcademicYearID = "7";
                AcademicYearList.Add(academicYear);

                academicYear = new AcademicYearModel();
                academicYear.Name = "3rd Year";
                academicYear.AcademicYearID = "8";
                AcademicYearList.Add(academicYear);

                academicYear = new AcademicYearModel();
                academicYear.Name = "4th Year";
                academicYear.AcademicYearID = "9";
                AcademicYearList.Add(academicYear);

                academicYear = new AcademicYearModel();
                academicYear.Name = "Final Year";
                academicYear.AcademicYearID = "10";
                AcademicYearList.Add(academicYear);
            }
            else
            {
                academicYear.Name = "1st Year";
                academicYear.AcademicYearID = "11";
                AcademicYearList.Add(academicYear);

                academicYear = new AcademicYearModel();
                academicYear.Name = "2nd Year";
                academicYear.AcademicYearID = "12";
                AcademicYearList.Add(academicYear);

                academicYear = new AcademicYearModel();
                academicYear.Name = "3rd Year";
                academicYear.AcademicYearID = "13";
                AcademicYearList.Add(academicYear);

                academicYear = new AcademicYearModel();
                academicYear.Name = "4th Year";
                academicYear.AcademicYearID = "14";
                AcademicYearList.Add(academicYear);

                academicYear = new AcademicYearModel();
                academicYear.Name = "5th Year";
                academicYear.AcademicYearID = "15";
                AcademicYearList.Add(academicYear);

                academicYear = new AcademicYearModel();
                academicYear.Name = "Final Year";
                academicYear.AcademicYearID = "16";
                AcademicYearList.Add(academicYear);
            }

            response.lstAcademicYear = AcademicYearList;
            response.RespCode = "0000";
            response.RespDescription = "Sucess";
            return response;
            //OtherBrnchListModel response = new OtherBrnchListModel();

            //#region BindRequestData

            //string requestFilePath = APIRoute.API_SelectOtherBranch;
            //string requestData = string.Empty;
            //string errormsg = string.Empty;
            //var apimodel = APIRequest(requestFilePath,
            //    Session[CommonDynamicKey].ToString(),
            //    Session[CommonSessionID].ToString(),
            //    Session[CommonUserID].ToString());
            //var apirequest = JsonConvert.DeserializeObject<OtherBranchSelectRequestModel>(JsonConvert.SerializeObject(apimodel));
            //apirequest.OtherBankID = majorid;
            //ABankRequestModel request = new ABankRequestModel();
            //request.UserId = apimodel.UserID;
            //request.SessionID = apimodel.SessionID;
            //request.DeviceID = CommonUtils.DeviceID;
            //request.UserType = CommonUtils.UserType;
            //request.IV = apimodel.IV;
            //try
            //{
            //    var bindreq = JsonConvert.SerializeObject(apirequest);
            //    request.JsonStringRequest = bindreq;
            //    requestData = JsonConvert.SerializeObject(request);
            //    request.JsonStringRequest = RijndaelCrypt.EncryptAES(bindreq, apimodel.dynamicKey, apimodel.hardCodeIV);
            //    #endregion

            //    var dataReturn = this.PostMobileAPI(request, apimodel.requestFilePath).Result;

            //    var bindData = RijndaelCrypt.DecryptAES(dataReturn.JsonStringResponse, apimodel.dynamicKey, apimodel.hardCodeIV);
            //    response = JsonConvert.DeserializeObject<OtherBrnchListModel>(bindData);
            //    return response;

            //}
            //catch (Exception ex)
            //{
            //    errormsg = ex.Message;
            //    return response;
            //}
            //finally
            //{
            //    if (!string.IsNullOrEmpty(errormsg))
            //    {
            //        Log.Error(requestFilePath + " Error: " + errormsg);
            //        Log.Error("Request:" + requestData);
            //        Log.Error("Response:" + JsonConvert.SerializeObject(response));
            //    }
            //    else
            //    {
            //        Log.Info(requestFilePath + " Successful");
            //        Log.Info("Request:" + requestData);
            //        Log.Info("Response:" + JsonConvert.SerializeObject(response));
            //    }
            //}
        }
    }
}