using AutoMapper;
using AutoMapper.Mappers;
using RescueRabbit.Services.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Web.ViewModels.Registration
{
    public class RegisterViewModel
    {

        private static MappingEngine __mappingEngine;

        static RegisterViewModel()
        {
            var configuration = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            var mappingEngine = new MappingEngine(configuration);
            configuration.CreateMap<RegisterViewModel, RegistrationRequest>();
            __mappingEngine = mappingEngine;
        }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Care Facility")]
        public string CareFacility { get; set; }

        public RegistrationRequest ToRegistrationRequest()
        {
            return __mappingEngine.Map<RegisterViewModel, RegistrationRequest>(this);
        }
    }
}
