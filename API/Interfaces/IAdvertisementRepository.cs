using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomefinderAPI.Models;

namespace API.Interfaces
{
    public interface IAdvertisementRepository
    {
        public Task<List<Advertisement>> ListAllAdvertisementsAsync();
    }
}