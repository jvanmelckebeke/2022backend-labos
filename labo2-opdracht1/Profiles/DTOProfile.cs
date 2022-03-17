using AutoMapper;
using labo2_opdracht1.DTO;
using labo2_opdracht1.Models;
using labo2_opdracht1.Services;

namespace labo2_opdracht1.Profiles;

public class VaccinLocationResolver : IValueResolver<VaccineRegistration, VaccineRegistrationDTO, string>
{
    private IVaccinationService _vaccinationService;
    public VaccinLocationResolver(IVaccinationService vaccinationService)
    {
        _vaccinationService = vaccinationService;
    }
    
    public string Resolve(VaccineRegistration source, VaccineRegistrationDTO destination, string dest,
        ResolutionContext context)
    {
        return _vaccinationService.GetVaccineById(source.VaccinationLocationId).Name;
    }
}

public class VaccinResolver : IValueResolver<VaccineRegistration, VaccineRegistrationDTO, string>
{
    private IVaccinationService _vaccinationService;
    
    public VaccinResolver(IVaccinationService vaccinationService)
    {
        _vaccinationService = vaccinationService;
    }
    public string Resolve(VaccineRegistration source, VaccineRegistrationDTO destination, string dest,
        ResolutionContext context)
    {
        return _vaccinationService.GetVaccineById(source.VaccineTypeId).Name;
    }
}

public class DTOProfile : Profile
{
    public DTOProfile()
    {
        CreateMap<VaccineRegistration, VaccineRegistrationDTO>()
            .ForMember(dest => dest.VaccineName, opt => opt.MapFrom<VaccinResolver>())
            .ForMember(dest => dest.Location, opt => opt.MapFrom<VaccinLocationResolver>());
    }
}