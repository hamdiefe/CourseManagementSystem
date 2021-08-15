using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using CourseManagementSystem.Authorization;
using CourseManagementSystem.Controllers;
using CourseManagementSystem.Services.Documents;
using CourseManagementSystem.Services.Documents.Dtos;
using CourseManagementSystem.Web.Models.Documents;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CourseManagementSystem.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Documents)]
    public class DocumentsController : CourseManagementSystemControllerBase
    {
        private readonly IDocumentsAppService _documentsAppService;

        public DocumentsController(IDocumentsAppService documentsAppService)
        {
            _documentsAppService = documentsAppService;
        }

        public ActionResult Index()
        {
            var model = new DocumentsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        public async Task<PartialViewResult> CreateOrEditModal(long? id)
        {
            GetDocumentForEditOutput getDocumentForEditOutput;

            if (id.HasValue)
            {
                getDocumentForEditOutput = await _documentsAppService.GetDocumentForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getDocumentForEditOutput = new GetDocumentForEditOutput
                {
                    Document = new CreateOrEditDocumentDto()
                };
                getDocumentForEditOutput.Document.Date = DateTime.Now;
            }

            var viewModel = new CreateOrEditDocumentModalViewModel()
            {
                Document = getDocumentForEditOutput.Document,
                DocumentStudentList = await _documentsAppService.GetStudentsForTableDropdown()
            };

            return PartialView("_CreateOrEditModal", viewModel);
        }   

        public async Task<PartialViewResult> ViewModal(int id)
        {
            var getDocumentForViewDto = await _documentsAppService.GetDocumentForView(id);

            var model = new DocumentViewModel()
            {
                Document = getDocumentForViewDto.Document
            };

            return PartialView("_ViewModal", model);
        }
    }
}
