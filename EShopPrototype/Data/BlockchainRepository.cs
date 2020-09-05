using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Nethereum.Web3;

namespace EShopPrototype.Data
{
    public class BlockchainRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string infuraUrl;
        private readonly string contratAddress;

        public BlockchainRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            infuraUrl = _configuration.GetSection("Config:InfuraURL").Value;
            contratAddress = _configuration.GetSection("Config:ContractAddress").Value;
        }

        public double GetProductPrice(int productId)
        {
            return 50.98;
        }

        public void GetProductPriceHistory()
        {

        }

    }
}
