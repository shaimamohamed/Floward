using CatalogService.Core.Entities;
using CatalogService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Interfaces.Services
{
    public interface IRabbitMQService
    {
        void SendToQueue(string message);
    }
}
