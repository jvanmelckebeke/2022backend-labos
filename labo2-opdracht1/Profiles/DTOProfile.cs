using AutoMapper;
using labo2_opdracht1.DTO;
using labo2_opdracht1.Models;

namespace labo2_opdracht1.Profiles;

public class VaccinLocationResolver : IValueResolver<VaccineRegistration, VaccineRegistrationDTO, string>
{
    public string Resolve(VaccineRegistration source, VaccineRegistrationDTO destination, string dest,
        ResolutionContext context)
    {
        List<VaccinationLocation> locations = context.Items["locations"] as List<VaccinationLocation>;
        return locations.Single(l => l.VaccinationLocationId == source.VaccinationLocationId).Name;
    }
}

public class VaccinResolver : IValueResolver<VaccineRegistration, VaccineRegistrationDTO, string>
{
    public string Resolve(VaccineRegistration source, VaccineRegistrationDTO destination, string dest,
        ResolutionContext context)
    {
        List<VaccineType> vaccins = context.Items["vaccins"] as List<VaccineType>;
        return vaccins.Single(l => l.VaccineTypeId == source.VaccineTypeId).Name;
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