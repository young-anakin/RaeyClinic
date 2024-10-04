﻿using DentalClinic.DTOs.RoleDTO;
using DentalClinic.DTOs.SMSSettingDTO;
using DentalClinic.Models;
using DentalClinic.Services.RoleService;
using DentalClinic.Services.SMSSettingService;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSSettingController : Controller
    {
        private readonly ISMSSettingService _SMSSettingService;
        public SMSSettingController(ISMSSettingService SMSSettingService)
        {
            _SMSSettingService = SMSSettingService;
        }
        [HttpPost]
        public async Task<ActionResult> AddSMSSetting(SMSSettingAddDTO DTO)
        {
            try
            {
                return Ok(await _SMSSettingService.AddSMSSetting(DTO));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ErrorResponse { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Internal Server Error" });
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateSMSSetting(SMSSettingUpdateDTO DTO)
        {
            try
            {

                return Ok(await _SMSSettingService.UpdateSMSSetting(DTO));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ErrorResponse { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Internal Server Error" });
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetSMSSetting()
        {
            try
            {
                return Ok(await _SMSSettingService.GetSMSSettings());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new ErrorResponse { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Internal Server Error" });
            }
        }

    }
}
