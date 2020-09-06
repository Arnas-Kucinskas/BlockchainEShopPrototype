using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace EShopPrototype.Data
{
    public class BlockchainRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string contractAddress;
        private readonly string getPriceFunctionName;
		private readonly string getLogsFunctionName;
		private readonly string setPriceFunctionName;
		private readonly string abi;
        private readonly string infuraUrl;
		private readonly string senderAddress;
		private readonly Web3 web3;

		public BlockchainRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            contractAddress = _configuration.GetSection("Blockchain:ContractAddress").Value;
            getPriceFunctionName = _configuration.GetSection("Blockchain:GetPriceFunction").Value;
			setPriceFunctionName = _configuration.GetSection("Blockchain:SetPriceFunction").Value;
			senderAddress = _configuration.GetSection("Blockchain:SenderAddress").Value;
			getLogsFunctionName = _configuration.GetSection("Blockchain:GetLogsFunction").Value;

			//abi = _configuration.GetSection("Blockchain:ContractABI").Value;
			/*TODO - Abi should be in appsettings.json*/
			abi = @"[
	{
		'inputs': [
			{
				'internalType': 'uint256',
				'name': 'productId',
				'type': 'uint256'
			},
			{
				'internalType': 'uint256',
				'name': 'price',
				'type': 'uint256'
			}
		],
		'name': 'AddOrUpdateProduct',
		'outputs': [],
		'stateMutability': 'nonpayable',
		'type': 'function'
	},
	{
		'anonymous': false,
		'inputs': [
			{
				'indexed': false,
				'internalType': 'uint256',
				'name': 'productId',
				'type': 'uint256'
			},
			{
				'indexed': false,
				'internalType': 'uint256',
				'name': 'price',
				'type': 'uint256'
			}
		],
		'name': 'LogPricechange',
		'type': 'event'
	},
	{
		'inputs': [
			{
				'internalType': 'uint256',
				'name': '',
				'type': 'uint256'
			}
		],
		'name': 'products',
		'outputs': [
			{
				'internalType': 'uint256',
				'name': '',
				'type': 'uint256'
			}
		],
		'stateMutability': 'view',
		'type': 'function'
	}
]";

            infuraUrl = _configuration.GetSection("Blockchain:InfuraURL").Value;
			web3 = new Web3(infuraUrl);
		}

        public async Task<bool> SetProductPrice(int productId, double price)
        {
            string privateKey = _configuration.GetSection("Blockchain:PrivateAddressKey").Value;
			Account account = new Account(privateKey);
			Web3 web3 = new Web3(account, infuraUrl);
			/* TODO  - calculate has based on transaction*/
			web3.TransactionManager.DefaultGas = 1000000;

			Contract contract = web3.Eth.GetContract(abi, contractAddress);
			Function setProductPrice = contract.GetFunction(setPriceFunctionName);
            try
            {
				var result = await setProductPrice.SendTransactionAsync(senderAddress, productId, price);
                if (result != null)
                {
					return true;
                }
			}
			catch (Exception)
            {
				return false;
			}
			return false;
        }
        public async Task<double> GetProductPrice(int productId)
        {
			var contract = web3.Eth.GetContract(abi, contractAddress);
            var getPrice = contract.GetFunction(getPriceFunctionName);
            int priceInInteger = await getPrice.CallAsync<int>(productId);
			double productPrice = FromIntPriceToDouble(priceInInteger);

			return productPrice;
        }


		public async Task GetProductPriceHistory(int id)
		{//List<Tuple<DateTime, double>>
			List <Tuple<DateTime, double>> priceHistoryList  = new List<Tuple<DateTime, double>>();
			BlockParameter firstBlock = BlockParameter.CreateEarliest();
			BlockParameter lastBlock = BlockParameter.CreateLatest();

			Contract contract = web3.Eth.GetContract(abi, contractAddress);
			Event eventLogs = contract.GetEvent(getLogsFunctionName);

			var filterInput = eventLogs.CreateFilterInput(firstBlock, lastBlock);
			var logs = await eventLogs.GetAllChanges<LogPricechangeEventDTOBase>(filterInput);
            foreach (var log in logs)
            {
                if (log.Event.ProductId == id)
                {
					var blockWithTransactions = await web3.Eth.Blocks.GetBlockWithTransactionsByHash.SendRequestAsync(log.Log.BlockHash);
					DateTime blockDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
					blockDateTime = blockDateTime.AddSeconds((long)blockWithTransactions.Timestamp.Value).ToLocalTime();
					priceHistoryList.Add(
							new Tuple<DateTime, double>(blockDateTime, FromIntPriceToDouble((int)log.Event.Price))
						);
				}
				
            }
			

			/*return new List<Tuple<DateTime, double>>() { 
                new Tuple<DateTime, double>(DateTime.Now, 50.95),
                new Tuple<DateTime, double>(DateTime.Now.AddDays(1), 60.95)
            };*/
        }

		private double FromIntPriceToDouble(int price)
        {
			return (double)price / (double)100;
		}

		[Event("LogPricechange")]
		public class LogPricechangeEventDTOBase : IEventDTO
		{
			[Parameter("uint256", "productId", 1, false)]
			public virtual BigInteger ProductId { get; set; }
			[Parameter("uint256", "price", 2, false)]
			public virtual BigInteger Price { get; set; }
		}

	}
}
