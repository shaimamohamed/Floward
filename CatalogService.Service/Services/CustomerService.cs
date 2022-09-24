using CatalogService.Core.Entities;
using CatalogService.Core.Interfaces.Repositories;
using CatalogService.Core.Interfaces.Services;
using CatalogService.Core.Models;
using CatalogService.Data.Helpers;
using CatalogService.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<GeneralResponse<LoginResponse>> Login(LoginRequest loginRequest)
        {
            var respnose = new GeneralResponse<LoginResponse>();

            if (string.IsNullOrWhiteSpace(loginRequest.Username) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            var customer = _customerRepository.GetCustomerByUsername(loginRequest.Username);

            if (customer == null)
            {
                respnose.Message = "Invalid Username or Pasword";
                return respnose;
            }

            var isVerified = HashingHelper.VerifyPasswordHashAndSalt(loginRequest.Password, customer.PasswordHash, customer.PasswordSalt);

            if (!isVerified)
            {
                respnose.Message = "Invalid Username or Pasword";
                return respnose;
            }

            respnose = new GeneralResponse<LoginResponse>()
            {
                Success = true,
                Data = new LoginResponse { UserId = customer.Id, Username = customer.Username, FirstName = customer.FirstName, LastName = customer.LastName }
            };

            return respnose;
        }

        public async Task<GeneralResponse<ProfileResponse>> GetProfile(string username)
        {
            var respnose = new GeneralResponse<ProfileResponse>();

            if (string.IsNullOrWhiteSpace(username))
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            var customer = _customerRepository.GetCustomerByUsername(username);

            if (customer == null)
            {
                respnose.Message = "Customer Not Found";
                return respnose;
            }

            respnose = new GeneralResponse<ProfileResponse>()
            {
                Success = true,
                Data = new ProfileResponse { UserId = customer.Id, Username = customer.Username, FirstName = customer.FirstName, LastName = customer.LastName }
            };

            return respnose;
        }
    }
}
