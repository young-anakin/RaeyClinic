using AutoMapper;
using DentalClinic.Context;
using DentalClinic.DTOs;
using DentalClinic.DTOs.MedicalCertificateDTO;
using DentalClinic.Models;
using DentalClinic.Services.Tools;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Services.MedicalCertificates
{
    public class MedicalCertificateService : IMedicalCertificateService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IToolsService _toolsService;
        public MedicalCertificateService(DataContext context, IMapper mapper, IToolsService toolsService)
        {
            _context = context;
            _mapper = mapper;
            _toolsService = toolsService;
        }

        public async Task<MedicalCertificate> CreateMedicalCertificate(AddMedicalCertificateDTO DTO)
        {
            Patient? patient = await _context.Patients.FindAsync(DTO.PatientId);

            Employee? employee = await _context.Employees.FindAsync(DTO.EmployeeId);

            MedicalCertificate MC = new MedicalCertificate
            {
                //Patient = patient,
                Diagnosis = DTO.Diagnosis,
                SickLeave = DTO.SickLeave,
                AttendedFrom = DTO.AttendedFrom,
                UpTo = DTO.UpTo,
                EmployeeId = DTO.EmployeeId,
                PatientId = DTO.PatientId,

            };

            MC.Patient = patient;
            MC.Employee = employee;


            _context.MedicalCertificates.Add(MC);
            await _context.SaveChangesAsync();
            return MC;

        }


        public async Task<MedicalCertificate> UpdateMedicalCertificate(UpdateMedicalCertificateDTO DTO)
        {

            MedicalCertificate? medicalCertificate = await _context.MedicalCertificates.FindAsync(DTO.CardNumber);

            //Patient? patient = await _context.Patients.FindAsync(DTO.PatientId);

            //Employee? employee = await _context.Employees.FindAsync(DTO.EmployeeId);

            if (medicalCertificate == null)
            {
                return null; // Prescription not found
            }

            medicalCertificate.UpTo = DTO.UpTo;
            medicalCertificate.AttendedFrom = DTO.AttendedFrom;
            medicalCertificate.Diagnosis = DTO.Diagnosis;
            medicalCertificate.SickLeave = DTO.SickLeave;




            _context.MedicalCertificates.Update(medicalCertificate);
            await _context.SaveChangesAsync();
            return medicalCertificate;

        }

        public async Task<MedicalCertificate> GetSpecificCertificate(int id)
        {
            return await _context.MedicalCertificates
                .Include(p => p.Patient)
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(p => p.CardNumber == id);

        }

        public async Task<List<MedicalCertificate>> GetAllCertificates()
        {
            return await _context.MedicalCertificates
                .Include(p => p.Patient)
                .Include(p => p.Employee)
                .ToListAsync();
        }

        public async Task<List<MedicalCertificate>> GetMedicalCertificatesByEmployee(int employeeId)
        {
            var medicalCertificates = await _context.MedicalCertificates
                .Include(mc => mc.Employee)
                .Where(mc => mc.Employee != null && mc.Employee.EmployeeId == employeeId)
                .ToListAsync();

            return medicalCertificates;
        }

        public async Task<List<MedicalCertificate>> GetMedicalCertificatesByPatient(int patientId)
        {
            var medicalCertificates = await _context.MedicalCertificates
                .Include(mc => mc.Patient)
                .Where(mc => mc.Patient != null && mc.Patient.PatientId == patientId)  // Handle null Patient references
                .ToListAsync();

            return medicalCertificates;
        }


        public async Task<bool> DeleteMedicalCertificate(int id)
        {
            var med = await _context.MedicalCertificates.FindAsync(id);
            if (med == null)
            {
                return false; // Prescription not found
            }

            _context.MedicalCertificates.Remove(med);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
