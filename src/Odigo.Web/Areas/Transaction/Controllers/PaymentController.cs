using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Odigo.Model.Model;
using Odigo.Business.Interfaces;
using Odigo.Web.Controllers;
using Odigo.Utility.Interfaces;
using Odigo.Web.Areas.Transaction.Models;
using Odigo.Entities;

namespace Odigo.Web.Areas.Transaction.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IRepository _da;
        private readonly ILogger _logger;
        private readonly IService _serviceChargeEngine;

        private PaymentViewModel _viewModel;

        public PaymentController(IRepository da, ILogger logger, IService serviceChargeEngine)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            if(serviceChargeEngine == null)
            {
                throw new ArgumentNullException("serviceChargeEngine");
            }

            _da = da;
            _logger = logger;
            _serviceChargeEngine = serviceChargeEngine;
        }

        public ActionResult Slip()
        {
            try
            {
                _viewModel = (PaymentViewModel)TempData["PaymentViewModel"];

                if (_viewModel == null)
                {
                    _viewModel = new PaymentViewModel();
                    PaymentSlip slip = (PaymentSlip)TempData["PaymentSlip"];

                    if (slip == null || slip.Payment == null)
                    {
                        throw new Exception("Payment or its Slip cannot be null! Please contact your system administrator");
                    }
                 
                    _viewModel.PaymentSlip = slip;
                }

                if (_viewModel.PaymentSlip == null || _viewModel.PaymentSlip.Payment == null || _viewModel.PaymentSlip.Payment.ServiceCharge == null || _viewModel.PaymentSlip.Payment.ServiceCharge.Id <= 0)
                {
                    throw new Exception("Error occurred! Slip details display operation failed due to empty input variables! Please contact your system administrator");
                }

                //if (_viewModel.PaymentSlip == null || _viewModel.PaymentSlip.Payment == null || _viewModel.PaymentSlip.Payment.Id <= 0 || _viewModel.PaymentSlip.Payment.ServiceCharge == null || _viewModel.PaymentSlip.Payment.ServiceCharge.Id <= 0)
                //{
                //    throw new Exception("Error occurred! Slip details display operation failed due to empty input variables! Please contact your system administrator");
                //}
            }
            catch(Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            TempData["PaymentViewModel"] = _viewModel;
            return View(_viewModel);
        }

        //[HttpPost]
        public ActionResult SubmitSlip(PaymentViewModel viewModel)
        {
            JsonResult json = null;

            try
            {
                if (ModelState.IsValid)
                {
                    //if (viewModel == null || viewModel.PaymentSlip == null || viewModel.PaymentSlip.Payment == null || viewModel.PaymentSlip.Payment.Id <= 0)

                    if (viewModel == null || viewModel.PaymentSlip == null || viewModel.PaymentSlip.Payment == null)
                    {
                        json = Json(new { isValid = false, message = "Error occurred! Slip details display operation failed due to empty input variables! Please contact your system administrator" }, "text/html", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        json = Json(new { isValid = true, message = "Form initialization was successful" }, "text/html", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    json = Json(new { isValid = false, message = "Form validation failed! Make sure that all required fields are correctly entered." }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                json = Json(new { isValid = false, message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }

            TempData["PaymentViewModel"] = viewModel;

            return json;
        }

        public ActionResult WebPay()
        {
            PaymentViewModel viewModel = (PaymentViewModel)TempData["PaymentViewModel"];

            try
            {
                if (viewModel != null)
                {
                    //_viewModel.ServiceCharge = viewModel.ServiceCharge;
                    //_viewModel.Person = viewModel.Person;
                }
                else
                {
                    throw new Exception("Error occurred! Interface display operation failed! Please contact your system administrator");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                SetMessage(ex.Message, ApplicationMessage.Category.Error);
            }

            //SetTempData(_viewModel);

            TempData["PaymentViewModel"] = viewModel;
            return View(viewModel);
        }

        private void SetPaymentViewModelFromTemData()
        {
            //_viewModel.School = (School)TempData["School"];
            //_viewModel.Student = (Student)TempData["Student"];
            //_viewModel.Class = (StudentLevel)TempData["Class"];
            //_viewModel.Payment = (Payment)TempData["Payment"];
            //_viewModel.SubjectCategory = (SubjectCategory)TempData["SubjectCategory"];
        }

        private void SetTempData(PaymentViewModel viewModel)
        {
            //TempData["Student"] = paymentViewModel.Student;
            //TempData["School"] = paymentViewModel.School;
            //TempData["Class"] = paymentViewModel.Class;
            //TempData["Payment"] = paymentViewModel.Payment;
            //TempData["SubjectCategory"] = paymentViewModel.SubjectCategory;
            //TempData["PaymentViewModel"] = paymentViewModel;
        }

        //[HttpPost]
        //public ActionResult WebPay(PaymentViewModel vm)
        //{
        //    PaymentViewModel paymentViewModel = (PaymentViewModel)TempData["PaymentViewModel"];

        //    //try
        //    //{
        //    //    if (ModelState.IsValid)
        //    //    {
        //    //        if (paymentViewModel != null && vm != null)
        //    //        {
        //    //            paymentViewModel.Payment.Channel = vm.Payment.Channel;
        //    //            SetTempData(paymentViewModel);

        //    //            return RedirectToAction("WebPayProcessor", "Payment");
        //    //        }
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    SetMessage(ex.Message, Message.Category.Error);
        //    //}

        //    //PopulateAllDropDowns();
        //    //SetTempData(paymentViewModel);
        //    return View(paymentViewModel);
        //}


        public ActionResult WebPayProcessor()
        {
            PaymentViewModel paymentViewModel = (PaymentViewModel)TempData["PaymentViewModel"];

            //try
            //{
               
            //    _viewModel = paymentViewModel;
            //    SetPaymentViewModelFromTemData();
            //}
            //catch (Exception ex)
            //{
            //    SetMessage(ex.Message, Message.Category.Error);
            //}

            //SetTempData(_viewModel);
            return View(_viewModel);
        }

        [HttpPost]
        public ActionResult WebPayProcessor(PaymentViewModel viewModel)
        {
            PaymentViewModel paymentViewModel = (PaymentViewModel)TempData["PaymentViewModel"];

            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        if (paymentViewModel != null)
            //        {
            //            paymentViewModel.Payment.Person = paymentViewModel.Student;
            //            paymentViewModel.Payment.School = paymentViewModel.School;
            //            paymentViewModel.Payment.DateEntered = DateTime.Now;
            //            paymentViewModel.Payment.DatePaid = DateTime.Now;
            //            paymentViewModel.Payment.Paid = true;

            //            PaymentEngine paymentEngine = _paymentEngineFactory.Create(Payment.Modes.Web);
            //            Invoice invoice = paymentEngine.Pay(paymentViewModel.Payment);
            //            if (invoice != null)
            //            {
            //                paymentViewModel.Invoice = invoice;
            //                SetTempData(paymentViewModel);

            //                TempData["Invoice"] = invoice;
            //                return RedirectToAction("WebPayResponse", "Payment");
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    SetMessage(ex.Message, Message.Category.Error);
            //}

            //SetTempData(paymentViewModel);
            return View(paymentViewModel);
        }

        public ActionResult WebPayResponse()
        {
            //Invoice invoice = (Invoice)TempData["Invoice"];

            //TempData["Invoice"] = invoice;
            //return View(invoice);

            return View();
        }

        private void PopulateAllDropDowns()
        {
            //PaymentViewModel viewModel = (PaymentViewModel)TempData["PaymentViewModel"];

            //try
            //{
            //    if (viewModel == null)
            //    {
            //        _viewModel = new PaymentViewModel();
            //        _viewModel.PaymentChannelSelectList = DropdownUtility.PopulateModelSelectList<PaymentChannel, PAYMENT_CHANNEL>(_da, false);

            //        ViewBag.PaymentChannels = _viewModel.PaymentChannelSelectList;
            //    }
            //    else
            //    {
            //        _viewModel = viewModel;
            //        if (_viewModel.Payment == null && _viewModel.Payment.Id > 0 && _viewModel.Payment.Channel == null && _paymentViewModel.Payment.Channel.Id > 0)
            //        {
            //            ViewBag.PaymentChannels = new SelectList(_viewModel.PaymentChannelSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT, _viewModel.Payment.Channel.Id);
            //        }
            //        else
            //        {
            //            ViewBag.PaymentChannels = new SelectList(_viewModel.PaymentChannelSelectList, DropdownUtility.VALUE, DropdownUtility.TEXT);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    SetMessage(ex.Message, Message.Category.Error);
            //}
        }










    }
}