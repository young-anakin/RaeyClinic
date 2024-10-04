using DentalClinic.DTOs.LaboratoryRequestsDTO;
using DentalClinic.Models;

namespace DentalClinic.Services.LaboratoryService
{
    public interface ILaboratoryService
    {
        Task<LaboratoryRequests> CreateLaboratoryRequests(AddLaboratoryRequestsDTO DTO);
        Task<bool> DeleteLabRequest(int id);
        Task<List<LaboratoryRequests>> GetLaboratoryReportedBy(int employeeId);
        Task<List<LaboratoryRequests>> GetLaboratoryRequestedBy(int employeeId);
        Task<List<LaboratoryRequests>> GetLaboratoryRequests();
        Task<List<LaboratoryRequests>> GetPatientLabReports(int patientID);
        Task<LaboratoryRequests> GetSpecificLabRequest(int id);
        Task<LaboratoryRequests?> UpdateLabReport(UpdateLaboratoryRequestDTO DTO);
    }
}