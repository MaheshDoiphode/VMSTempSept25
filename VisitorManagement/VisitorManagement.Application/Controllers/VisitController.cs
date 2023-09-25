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


        public VisitController(IHostVisitorRequestService hostvisitorrequestService, IAdminApprovalStatusService adminApprovalRequestService)
        {
            _adminApprovalRequestService = adminApprovalRequestService;
            _hostVisitorRequestService = hostvisitorrequestService;
        }

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

        // AdminApprovalStatus

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

        [HttpPut("AdminApproveRequest/{requestId}")]
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

        [HttpPut("AdminDenyRequest/{requestId}")]
        public async Task<IActionResult> UpdateAdminApprovalStatusToDenied(int requestId)
        {
            try
            {
                var result = await _adminApprovalRequestService.UpdateAdminApprovalStatusToDeniedAsync(requestId);

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

        [HttpPut("AdminVisitCompleted/{requestId}")]
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

        // AdminApprovalStatus

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
                z
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


    }
}
