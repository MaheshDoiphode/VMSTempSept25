using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorManagement.Application.DTOs;
using VisitorManagement.Application.Services;
using VisitorManagement.Domain.Common;
using VisitorManagement.Domain.Exceptions;

namespace VisitorManagement.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        private readonly IHostVisitorRequestService _hostVisitorRequestService;
        private readonly IAdminApprovalStatusService _adminApprovalRequestService;
        private readonly IVisitorEntityService _visitorService;

        public VisitController(IHostVisitorRequestService hostvisitorrequestService, IAdminApprovalStatusService adminApprovalRequestService, IVisitorEntityService visitorService)
        {
            _adminApprovalRequestService = adminApprovalRequestService;
            _hostVisitorRequestService = hostvisitorrequestService;
            _visitorService = visitorService;
        }
        
        // Host EndPoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HostVisitorRequestDTO>>> GetAllRequestsAsync()
        {
            try
            {
                var requests = await _hostVisitorRequestService.GetAllRequestsAsync();
                return Ok(requests);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while retrieving requests: {ex.Message}");
            }
        }

        [HttpGet("{requestId}")]
        public async Task<ActionResult<HostVisitorRequestDTO>> GetRequestByIdAsync(int requestId)
        {
            try
            {
                var request = await _hostVisitorRequestService.GetRequestByIdAsync(requestId);

                if (request == null)
                {
                    return NotFound($"Host Visitor Request with ID {requestId} not found.");
                }

                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while retrieving the request: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<HostVisitorRequestDTO>> CreateRequestAsync(HostVisitorRequestDTO requestDTO)
        {
            try
            {
                var createdRequestDTO = await _hostVisitorRequestService.CreateRequestAsync(requestDTO);
                return Ok(createdRequestDTO);
            }
            catch (HostVisitorRequestServiceException ex)
            {
                return BadRequest($"Failed to create request: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while creating the request: {ex.Message}");
            }
        }

        [HttpPut("{requestId}")]
        public async Task<ActionResult> UpdateRequestAsync(int requestId, HostVisitorRequestDTO requestDTO)
        {
            try
            {
                var success = await _hostVisitorRequestService.UpdateRequestAsync(requestId, requestDTO);

                if (success)
                {
                    return NoContent();
                }

                return BadRequest($"Failed to update request with ID {requestId}.");
            }
            catch (HostVisitorRequestNotFoundException ex)
            {
                return NotFound($"Host Visitor Request with ID {requestId} not found: {ex.Message}");
            }
            catch (HostVisitorRequestServiceException ex)
            {
                return BadRequest($"Failed to update request: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while updating the request: {ex.Message}");
            }
        }

        [HttpDelete("{requestId}")]
        public async Task<ActionResult> DeleteRequestAsync(int requestId)
        {
            try
            {
                var success = await _hostVisitorRequestService.DeleteRequestAsync(requestId);

                if (success)
                {
                    return NoContent();
                }

                return BadRequest($"Failed to delete request with ID {requestId}.");
            }
            catch (HostVisitorRequestNotFoundException ex)
            {
                return NotFound($"Host Visitor Request with ID {requestId} not found: {ex.Message}");
            }
            catch (HostVisitorRequestServiceException ex)
            {
                return BadRequest($"Failed to delete request: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while deleting the request: {ex.Message}");
            }
        }


        [HttpGet("host-visitor-request/{requestId}")]
        public async Task<ActionResult<HostVisitorRequestDTO>> GetHostVisitorRequestByIdAsync(int requestId)
        {
            try
            {
                var request = await _adminApprovalRequestService.GetHostVisitorRequestByIdAsync(requestId);

                if (request == null)
                {
                    return NotFound($"Host Visitor Request with ID {requestId} was not found.");
                }

                return Ok(request);
            }
            catch (HostVisitorRequestNotFoundException ex)
            {
                return NotFound($"Host Visitor Request with ID {requestId} was not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
            }
        }

        // AdminApprovalStatus EndPoints

        [HttpGet("admin-approval-statuses")]
        public async Task<ActionResult<IEnumerable<AdminApprovalStatusDTO>>> GetAllAdminApprovalStatusesAsync()
        {
            try
            {
                var adminApprovalStatuses = await _adminApprovalRequestService.GetAllAdminApprovalStatusesAsync();

                if (adminApprovalStatuses.Any())
                {
                    return Ok(adminApprovalStatuses);
                }
                else
                {
                    return NotFound("No admin approval statuses found.");
                }
            }
            catch (AdminApprovalStatusNotFoundException ex)
            {
                return NotFound($"Admin Approval Status not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
            }
        }

        
        [HttpGet("admin-approval-status/{hostVisitorRequestId}")]
        public async Task<ActionResult<AdminApprovalStatusDTO>> GetAdminApprovalStatusAsync(int hostVisitorRequestId)
        {
            try
            {
                var adminApprovalStatus = await _adminApprovalRequestService.GetAdminApprovalStatusAsync(hostVisitorRequestId);

                if (adminApprovalStatus == null)
                {
                    return NotFound($"Admin Approval Status for Request with ID {hostVisitorRequestId} was not found.");
                }

                return Ok(adminApprovalStatus);
            }
            catch (AdminApprovalStatusNotFoundException ex)
            {
                return NotFound($"Admin Approval Status for Request with ID {hostVisitorRequestId} was not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpPut("admin-approval-status/approve-request/{requestId}")]
        public async Task<IActionResult> UpdateAdminApprovalStatusToApproved(int requestId)
        {
            try
            {
                var result = await _adminApprovalRequestService.UpdateAdminApprovalStatusToApprovedAsync(requestId);

                if (result)
                {
                    return Ok($"Admin Approval Status for Request with ID {requestId} was updated successfully.");
                }
                else
                {
                    return NotFound($"Admin Approval Status for Request with ID {requestId} was not found.");
                }
            }
            catch (AdminApprovalStatusNotFoundException ex)
            {
                return NotFound($"Admin Approval Status for Request with ID {requestId} was not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
            }
        }
        [HttpPut("admin-approval-status/deny-request/{requestId}")]
        public async Task<IActionResult> UpdateAdminApprovalStatusToDenied(int requestId, string reason)
        {
           
            try
            {
                Console.WriteLine("-----------------------------------------", reason);
                var result = await _adminApprovalRequestService.UpdateAdminApprovalStatusToDeniedAsync(requestId, reason);
                

                if (result)
                {
                    return Ok(new { message = $"Admin Approval Status for Request with ID {requestId} was updated successfully." });
                }
                else
                {
                    return NotFound(new { message = $"Admin Approval Status for Request with ID {requestId} was not found." });
                }
            }
            catch (AdminApprovalStatusNotFoundException ex)
            {
                return NotFound(new { message = $"Admin Approval Status for Request with ID {requestId} was not found: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An unexpected error occurred: {ex.Message}" });
            }
        }


        [HttpPut("admin-approval-status/mark-complete/{requestId}")]
        public async Task<IActionResult> UpdateAdminApprovalStatusToVisitCompleted(int requestId)
        {
            try
            {
                var result = await _adminApprovalRequestService.UpdateAdminApprovalStatusToVisitCompleted(requestId);

                if (result)
                {
                    return Ok($"Admin Approval Status for Request with ID {requestId} was updated successfully.");
                }
                else
                {
                    return NotFound($"Admin Approval Status for Request with ID {requestId} was not found.");
                }
            }
            catch (AdminApprovalStatusNotFoundException ex)
            {
                return NotFound($"Admin Approval Status for Request with ID {requestId} was not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
            }
        }


        [HttpGet("admin-approval-status/pending-not-approved")]
        public async Task<ActionResult<IEnumerable<AdminApprovalStatusDTO>>> GetAllPendingNotApprovedVisits()
        {
            try
            {
                var adminApprovalStatuses = await _adminApprovalRequestService.GetAllPendingNotApprovedVisits();

                if (adminApprovalStatuses.Any())
                {
                    return Ok(adminApprovalStatuses);
                }
                else
                {
                    return NotFound("No pending, not approved visits found.");
                }
            }
            catch (AdminApprovalStatusNotFoundException ex)
            {
                return NotFound($"Admin Approval Status not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("admin-approval-status/not-approved-tomorrow")]
        public async Task<ActionResult<IEnumerable<AdminApprovalStatusDTO>>> GetNotApprovedTomorrowVisitsAsync()
        {
            try
            {
                var adminApprovalStatuses = await _adminApprovalRequestService.GetNotApprovedTomorrowVisitsAsync();

                if (adminApprovalStatuses.Any())
                {
                    return Ok(adminApprovalStatuses);
                }
                else
                {
                    return NotFound("No not approved visits found for tomorrow.");
                }
            }
            catch (AdminApprovalStatusNotFoundException ex)
            {
                return NotFound($"Admin Approval Status not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("admin-approval-status/denied")]
        public async Task<ActionResult<IEnumerable<AdminApprovalStatusDTO>>> GetAllDeniedVisitsAsync()
        {
            try
            {
                var adminApprovalStatuses = await _adminApprovalRequestService.GetAllDeniedVisitsAsync();

                if (adminApprovalStatuses.Any())
                {
                    return Ok(adminApprovalStatuses);
                }
                else
                {
                    return NotFound("No denied visits found.");
                }
            }
            catch (AdminApprovalStatusNotFoundException ex)
            {
                return NotFound($"Admin Approval Status not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("admin-approval-status/approved")]
        public async Task<ActionResult<IEnumerable<AdminApprovalStatusDTO>>> GetAllApprovedVisitsAsync()
        {
            try
            {
                var adminApprovalStatuses = await _adminApprovalRequestService.GetAllApprovedVisitsAsync();

                if (adminApprovalStatuses.Any())
                {
                    return Ok(adminApprovalStatuses);
                }
                else
                {
                    return NotFound("No approved visits found.");
                }
            }
            catch (AdminApprovalStatusNotFoundException ex)
            {
                return NotFound($"Admin Approval Status not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("admin-approval-status/completed")]
        public async Task<ActionResult<IEnumerable<AdminApprovalStatusDTO>>> GetAllCompletedVisitsAsync()
        {
            try
            {
                var adminApprovalStatuses = await _adminApprovalRequestService.GetAllCompletedVisitsAsync();

                if (adminApprovalStatuses.Any())
                {
                    return Ok(adminApprovalStatuses);
                }
                else
                {
                    return NotFound("No completed visits found.");
                }
            }
            catch (AdminApprovalStatusNotFoundException ex)
            {
                return NotFound($"Admin Approval Status not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
            }
        }
        // Visitor End Points

        [HttpGet("getVisitorsByNameAndContact/{visitorName}/{visitorContact}")]
        public async Task<ActionResult<IEnumerable<VisitorEntityDTO>>> GetVisitorsByVisitorNameAndContactAsync(string visitorName, string visitorContact)
        {
            try
            {
                var visitors = await _visitorService.GetVisitorsByVisitorNameAndContactAsync(visitorName, visitorContact);
                return Ok(visitors);
            }
            catch (VisitorEntityNotFoundException ex)
            {
                return NotFound($"Visitors with name {visitorName} and contact number {visitorContact} not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while retrieving visitors: {ex.Message}");
            }
        }

        [HttpGet("getAllVisitors")]
        public async Task<ActionResult<IEnumerable<VisitorEntityDTO>>> GetAllVisitorsAsync()
        {
            try
            {
                var visitors = await _visitorService.GetAllVisitorsAsync();
                return Ok(visitors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while retrieving visitors: {ex.Message}");
            }
        }

        [HttpPost("createVisitor")]
        public async Task<ActionResult<VisitorEntityDTO>> CreateVisitorAsync([FromBody] VisitorEntityDTO visitorEntityDTO)
        {
            try
            {
                var createdVisitorDTO = await _visitorService.CreateVisitorAsync(visitorEntityDTO);
                return Ok(createdVisitorDTO);
            }
            catch (VisitorEntityServiceException ex)
            {
                return BadRequest($"Failed to create visitor: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while creating the visitor: {ex.Message}");
            }
        }

        [HttpPut("update-visitor")]
        public async Task<ActionResult> UpdateVisitorAsync(string visitorName, string visitorContactNumber, [FromBody] VisitorEntity updatedVisitor)
        {
            try
            {
                var success = await _visitorService.UpdateVisitorAsync(visitorName, visitorContactNumber, updatedVisitor);

                if (success)
                {
                    return NoContent();
                }

                return NotFound($"Visitor with name {visitorName} and contact number {visitorContactNumber} not found.");
            }
            catch (VisitorEntityServiceException ex)
            {
                return BadRequest($"Failed to update visitor: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while updating the visitor: {ex.Message}");
            }
        }


        [HttpDelete("visitor-entity")]
        public async Task<ActionResult> DeleteVisitorAsync(string name, string contactNumber)
        {
            try
            {
                var success = await _visitorService.DeleteVisitorAsync(name, contactNumber);

                if (success)
                {
                    return NoContent();
                }

                return BadRequest($"Failed to delete visitor with name: {name} and contact number: {contactNumber}.");
            }
            catch (VisitorEntityNotFoundException ex)
            {
                return NotFound($"Visitor with name {name} and contact number {contactNumber} not found: {ex.Message}");
            }
            catch (VisitorEntityServiceException ex)
            {
                return BadRequest($"Failed to delete visitor: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while deleting the visitor: {ex.Message}");
            }
        }

    }
}
